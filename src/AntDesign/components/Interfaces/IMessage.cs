using System;

using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace AntDesign
{
    public interface IMessage : IDisposable
    {
        Task Open([NotNull] MessageConfig config);
        Task Success(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null);
        Task Error(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null);
        Task Info(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null);
        Task Warning(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null);
        Task Warn(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null);
        Task Loading(OneOf<string, RenderFragment, MessageConfig> content, double? duration = null, Action onClose = null);

        void Config(MessageGlobalConfig config);

        void Destroy();
    }
}
