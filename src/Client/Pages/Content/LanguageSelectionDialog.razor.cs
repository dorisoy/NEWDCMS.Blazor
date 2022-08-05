using DCMS.Shared.Constants.Localization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class LanguageSelectionDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private MudListItem SelectedItem { get; set; }
        private object currentLanguageCode = "";

        protected override async Task OnInitializedAsync()
        {
            currentLanguageCode = await _clientPreferenceService.LanguageCode();
        }

        private async Task Submit()
        {
            if (SelectedItem == null)
            {
                _snackBar.Add("Please choose language", Severity.Error);
                return;
            }

            var languageCode = (string)SelectedItem.Value;
            var result = await _clientPreferenceService.ChangeLanguageAsync(languageCode);
            if (result.Succeeded)
            {
                _snackBar.Add(result.Messages[0], Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                foreach (var error in result.Messages)
                {
                    _snackBar.Add(error, Severity.Error);
                }
            }
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
