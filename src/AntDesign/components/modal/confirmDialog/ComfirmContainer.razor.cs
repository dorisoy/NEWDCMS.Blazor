using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace AntDesign
{
    public partial class ComfirmContainer
    {
        [Inject]
        private ModalService ModalService { get; set; }

        [Inject]
        private ConfirmService ConfirmService { get; set; }

        /// <summary>
        /// 用于表示 ConfirmRef 集合
        /// </summary>
        private readonly List<ConfirmRef> _confirmRefs = new List<ConfirmRef>();

        #region override

        /// <summary>
        /// 注册打开关闭事件（AntContainer）
        /// </summary>
        protected override void OnInitialized()
        {
            ModalService.OnConfirmOpenEvent += OnConfirmOpen;
            ModalService.OnConfirmCloseEvent += OnConfirmClose;
            ModalService.OnConfirmCloseAllEvent += OnConfirmCloseAll;
            ModalService.OnConfirmUpdateEvent += OnConfirmUpdate;
            //注册Confirm OnConfirmOpen
            ConfirmService.OnOpenEvent += OnConfirmOpen;
        }

        #endregion

        /// <summary>
        ///创建并打开确认对话框
        /// </summary>
        private async Task OnConfirmOpen(ConfirmRef confirmRef)
        {
            confirmRef.Config.Visible = true;
            if (!_confirmRefs.Contains(confirmRef))
            {
                confirmRef.Config.BuildButtonsDefaultOptions();
                _confirmRefs.Add(confirmRef);
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 更新确认对话框
        /// </summary>
        /// <param name="confirmRef"></param>
        /// <returns></returns>
        private async Task OnConfirmUpdate(ConfirmRef confirmRef)
        {
            if (confirmRef.Config.Visible)
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        /// <summary>
        /// 释放指定确认对话框
        /// </summary>
        /// <param name="confirmRef"></param>
        /// <returns></returns>
        private async Task OnConfirmClose(ConfirmRef confirmRef)
        {
            if (confirmRef.Config.Visible)
            {
                confirmRef.Config.Visible = false;
                await InvokeAsync(StateHasChanged);
                if (confirmRef.OnClose != null)
                {
                    await confirmRef.OnClose.Invoke();
                }
            }
        }

        /// <summary>
        /// 确认对话框从DOM移除之后，从_confirmRefs 移除它
        /// </summary>
        /// <param name="confirmRef"></param>
        /// <returns></returns>
        private Task OnConfirmRemove(ConfirmRef confirmRef)
        {
            if (_confirmRefs.Contains(confirmRef))
            {
                _confirmRefs.Remove(confirmRef);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 释放全部确认对话框
        /// </summary>
        /// <returns></returns>
        private async Task OnConfirmCloseAll()
        {
            // avoid iterations the change of _confirmRefs affects the iterative process
            //避免迭代_confirmRefs的更改会影响迭代过程
            var confirmRefsTemp = new List<ConfirmRef>(_confirmRefs);
            foreach (var confirmRef in confirmRefsTemp)
            {
                await OnConfirmClose(confirmRef);
            }
        }

        /// <summary>
        /// 卸载事件
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            ModalService.OnConfirmOpenEvent -= OnConfirmOpen;
            ModalService.OnConfirmCloseEvent -= OnConfirmClose;
            ModalService.OnConfirmCloseAllEvent -= OnConfirmCloseAll;
            ModalService.OnConfirmUpdateEvent -= OnConfirmUpdate;
            //卸载Confirm OnConfirmOpen
            ConfirmService.OnOpenEvent -= OnConfirmOpen;

            base.Dispose(disposing);
        }
    }
}
