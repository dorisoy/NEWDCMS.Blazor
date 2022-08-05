using DCMS.Client.Extensions;
using DCMS.Web.Infrastructure.Services.Identity.Roles;
using DCMS.Client.Shared.Dialogs;
using DCMS.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DCMS.Client.Shared
{
    public partial class MainBody
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback OnDarkModeToggle { get; set; }

        [Parameter]
        public EventCallback<bool> OnRightToLeftToggle { get; set; }

        private bool _drawerOpen = true;
        [Inject] private IRoleService RoleService { get; set; }

        private string CurrentUserId { get; set; }
        private string ImageDataUrl { get; set; }
        private string UserRealName { get; set; }
        private string SecondName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }
        private bool _rightToLeft = false;
        static Action OnInstallable;

        private DotNetObjectReference<MainBody> reference;
        protected DotNetObjectReference<MainBody> Reference
        {
            get
            {
                if (reference == null)
                {
                    reference = DotNetObjectReference.Create(this);
                }

                return reference;
            }
        }
        private async Task RightToLeftToggle()
        {
            var isRtl = await _clientPreferenceService.ToggleLayoutDirection();
            _rightToLeft = isRtl;

            await OnRightToLeftToggle.InvokeAsync(isRtl);
        }

        public async Task ToggleDarkMode()
        {
            await OnDarkModeToggle.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await _jsRuntime.InvokeAsync<object>("setRef", Reference);
            
            OnInstallable = async () =>
            {
                var parameters = new DialogParameters();
                var options = new DialogOptions() { CloseButton = false, MaxWidth = MaxWidth.Medium };
                var dialog = _dialogService.Show<InstallApp>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    await _jsRuntime.InvokeVoidAsync("BlazorPWA.installPWA");
                }
            };

            await LoadDataAsync();

            _rightToLeft = await _clientPreferenceService.IsRTL();
            _interceptor.RegisterEvent();

            hubConnection = hubConnection.TryInitialize(_NavigationManager, _localStorage);
            await hubConnection.StartAsync();
            hubConnection.On<string, string, string>(ApplicationConstants.SignalR.ReceiveChatNotification, (message, receiverUserId, senderUserId) =>
            {
                if (CurrentUserId == receiverUserId)
                {
                    _jsRuntime.InvokeAsync<string>("PlayAudio", "notification");
                    _snackBar.Add(message, Severity.Info, config =>
                    {
                        config.VisibleStateDuration = 10000;
                        config.HideTransitionDuration = 500;
                        config.ShowTransitionDuration = 500;
                        config.Action = _localizer["Chat?"];
                        config.ActionColor = Color.Primary;
                        config.Onclick = snackbar =>
                        {
                            _NavigationManager.NavigateTo($"chat/{senderUserId}");
                            return Task.CompletedTask;
                        };
                    });
                }
            });
            hubConnection.On(ApplicationConstants.SignalR.ReceiveRegenerateTokens, async () =>
            {
                try
                {
                    var token = await _authenticationService.TryForceRefreshToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        _snackBar.Add(_localizer["Refreshed Token."], Severity.Success);
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _snackBar.Add(_localizer["You are Logged Out."], Severity.Error);
                    await _authenticationService.Logout();
                    _NavigationManager.NavigateTo("/");
                }
            });
            hubConnection.On<string, string>(ApplicationConstants.SignalR.LogoutUsersByRole, async (userId, roleId) =>
            {
                if (CurrentUserId != userId)
                {
                    var rolesModel = await RoleService.GetRolesAsync();
                    if (rolesModel.Succeeded)
                    {
                        var role = rolesModel.Data.FirstOrDefault(x => x.Id == roleId);
                        if (role != null)
                        {
                            var currentUserRolesModel = await _userService.GetRolesAsync(CurrentUserId);
                            if (currentUserRolesModel.Succeeded && currentUserRolesModel.Data.UserRoles.Any(x => x.RoleName == role.Name))
                            {
                                _snackBar.Add(_localizer["You are logged out because the Permissions of one of your Roles have been updated."], Severity.Error);
                                await hubConnection.SendAsync(ApplicationConstants.SignalR.OnDisconnect, CurrentUserId);
                                await _authenticationService.Logout();
                                _NavigationManager.NavigateTo("/login");
                            }
                        }
                    }
                }
            });
            hubConnection.On<string>(ApplicationConstants.SignalR.PingRequest, async (userName) =>
            {
                await hubConnection.SendAsync(ApplicationConstants.SignalR.PingResponse, CurrentUserId, userName);
            });

            await hubConnection.SendAsync(ApplicationConstants.SignalR.OnConnect, CurrentUserId);

            _snackBar.Add(string.Format(_localizer["Welcome {0}"], UserRealName), Severity.Success);
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user == null) return;
            if (user.Identity?.IsAuthenticated == true)
            {
                CurrentUserId = user.GetUserId();
                UserRealName = user.GetUserRealName();
                if (UserRealName.Length > 0)
                {
                    FirstLetterOfName = UserRealName[0];
                }
                SecondName = user.GetUserRealName();
                Email = user.GetEmail();
                var imageModel = await _accountService.GetProfilePictureAsync(CurrentUserId);
                if (imageModel.Succeeded)
                {
                    ImageDataUrl = imageModel.Data;
                }

                var currentUserResult = await _userService.GetAsync(CurrentUserId);
                if (!currentUserResult.Succeeded || currentUserResult.Data == null)
                {
                    _snackBar.Add(_localizer["You are logged out because the user with your Token has been deleted."], Severity.Error);
                    await _authenticationService.Logout();
                }
            }
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), $"{_localizer["Logout Confirmation"]}"},
                {nameof(Dialogs.Logout.ButtonText), $"{_localizer["Logout"]}"},
                {nameof(Dialogs.Logout.Color), Color.Error},
                {nameof(Dialogs.Logout.CurrentUserId), CurrentUserId},
                {nameof(Dialogs.Logout.HubConnection), hubConnection}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Dialogs.Logout>(_localizer["Logout"], parameters, options);
        }
        
        [JSInvokable("PWAInstallable")]
        public static Task PWAInstallable()
        {
            OnInstallable.Invoke();
            return Task.CompletedTask;
        }

        [JSInvokable("ShowUpdateVersion")]
        public Task ShowUpdateVersion()
        {
            _snackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            var message = "New version available.";
            _snackBar.Add(message, Severity.Info, config =>
            {
                config.RequireInteraction = true;
                config.ShowCloseIcon = false;
                config.Action = _localizer["UPDATE?"];
                config.Onclick = async (snackbar) =>
                {
                    await _jsRuntime.InvokeVoidAsync("DCMS.onUserUpdate");
                };
            });
            return Task.CompletedTask;
        }

        private HubConnection hubConnection;
        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;
    }
}