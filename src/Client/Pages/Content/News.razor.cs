using DCMS.Application.Models;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCMS.Client.Pages.Content
{
    public partial class News
    {
        private bool _loaded;
        private List<PostSummary> _postList;
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
            _postList = new List<PostSummary>
            {
                new PostSummary
                {
                    Id = 1,
                    Image = "/images/pictures/11.jpg",
                    Title = "Very Berry Chocolate...",
                    ShortDesc = "Ok, no room for modesty on this blog.....",
                    PostedDate = "MAR 20, 2021"
                },
                new PostSummary
                {
                    Id = 2,
                    Image = "/images/pictures/12.jpg",
                    Title = "Beetroot Quinoa",
                    ShortDesc = "Last week I post a phjtoto on my blog....",
                    PostedDate = "MAR 20, 2021"
                },
                new PostSummary
                {
                    Id = 3,
                    Image = "/images/pictures/17.jpg",
                    Title = "Spicy Sarpine, Cherry...",
                    ShortDesc = "Lorem Ipsum Dolor Sit Amet.....",
                    PostedDate = "MAR 20, 2021"
                },
                new PostSummary
                {
                    Id = 4,
                    Image = "/images/pictures/14.jpg",
                    Title = "Very Berry Chocolate...",
                    ShortDesc = "Ok, no room for modesty on this blog.....",
                    PostedDate = "FEB 20, 2021"
                },
                new PostSummary
                {
                    Id = 5,
                    Image = "/images/pictures/15.jpg",
                    Title = "Lorem Ipsum Dolor...",
                    ShortDesc = "Ok, no room for modesty on this blog.....",
                    PostedDate = "JAN 20, 2021"
                }
            };

            _loaded = true;
        }

        private void OpenPostDetails(int id)
        {
            var parameters = new DialogParameters();
            var post = _postList.FirstOrDefault(x => x.Id == id);
            parameters.Add(nameof(PostDetailModal.Post), post);

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, FullScreen = true, DisableBackdropClick = true };
            _dialogService.Show<PostDetailModal>("", parameters, options);
        }
    }
}
