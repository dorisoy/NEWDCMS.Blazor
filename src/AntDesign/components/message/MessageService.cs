using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OneOf;
using Microsoft.AspNetCore.Components.Routing;


namespace AntDesign
{
    /// <summary>
    /// 全局展示操作反馈信息
    /// </summary>
    public class MessageService : IMessage, IDisposable
    {
        internal event Action<MessageGlobalConfig> OnConfig;
        internal event Func<MessageConfig, Task> OnOpening;
        internal event Action OnDestroy;

        private NavigationManager _navigationManager;
        public AntDesignConfiguration Configuration { get; }


        public MessageService(NavigationManager navigationManager, AntDesignConfiguration configuration = null)
        {
            _navigationManager = navigationManager;
            configuration ??= new AntDesignConfiguration();

            Configuration = configuration;
            navigationManager.LocationChanged += NavigationManager_LocationChanged;

        }

        #region API

        public Task Open([NotNull] MessageConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (OnOpening != null)
            {
                return OnOpening.Invoke(config);
            }

            return Task.CompletedTask;
        }

        public Task Success(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            return PreOpen(MessageType.Success, content, duration, onClose);
        }

        public Task Error(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            return PreOpen(MessageType.Error, content, duration, onClose);
        }

        public Task Info(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            var task = PreOpen(MessageType.Info, content, duration, onClose);
            return task;
        }

        public Task Warning(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            return PreOpen(MessageType.Warning, content, duration, onClose);
        }

        public Task Warn(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            return Warning(content, duration, onClose);
        }

        public Task Loading(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            return PreOpen(MessageType.Loading, content, duration, onClose);
        }

        private Task PreOpen(MessageType type, OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null)
        {
            MessageConfig config;
            if (content.IsT2)
            {
                config = content.AsT2;
            }
            else
            {
                config = new MessageConfig() { };
                OneOf<string, RenderFragment> configContent;
                if (content.IsT1)
                {
                    configContent = content.AsT1;
                }
                else
                {
                    configContent = content.AsT0;
                }

                config.Content = configContent;
                config.Duration = duration;

                if (onClose != null)
                {
                    config.OnClose += onClose;
                }
            }

            config.Type = type;
            return Open(config);
        }

        #endregion





        private void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            Destroy();
        }

        public void Config(MessageGlobalConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            OnConfig?.Invoke(config);
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }

        public void Dispose()
        {
            OnDestroy?.Invoke();
            _navigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
