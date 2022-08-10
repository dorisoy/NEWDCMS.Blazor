﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace AntDesign
{
    /// <summary>
    /// Confirm icon type
    /// </summary>
    public enum ConfirmIcon
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Success = 4,
        Question = 5,
    }

    public static class ConfirmIconRenderFragments
    {
        public static RenderFragment Info = (builder) =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Type", "info-circle");
            builder.AddAttribute(2, "Theme", "outline");
            builder.CloseComponent();
        };

        public static RenderFragment Warning = (builder) =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Type", "exclamation-circle");
            builder.AddAttribute(2, "Theme", "outline");
            builder.CloseComponent();
        };

        public static RenderFragment Error = (builder) =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Type", "close-circle");
            builder.AddAttribute(2, "Theme", "outline");
            builder.CloseComponent();
        };

        public static RenderFragment Success = (builder) =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Type", "check-circle");
            builder.AddAttribute(2, "Theme", "outline");
            builder.CloseComponent();
        };

        public static RenderFragment Question = (builder) =>
        {
            builder.OpenComponent<Icon>(0);
            builder.AddAttribute(1, "Type", "question-circle");
            builder.AddAttribute(2, "Theme", "outline");
            builder.CloseComponent();
        };


        public static RenderFragment GetByConfirmIcon(ConfirmIcon confirmIcon)
        {
            switch (confirmIcon)
            {
                case ConfirmIcon.Info: return Info;
                case ConfirmIcon.Warning: return Warning;
                case ConfirmIcon.Error: return Error;
                case ConfirmIcon.Success: return Success;
                case ConfirmIcon.Question: return Question;
                default: return null;
            }
        }
    }
}
