using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace InteraCoop.Frontend.Shared
{
    public partial class Pagination
    {
        private List<PageModel> links = [];
        private List<OptionModel> options = [];
        private int selectedOptionValue = 10;
        [Parameter] public int CurrentPage { get; set; } = 1;
        [Parameter] public int TotalPages { get; set; }
        [Parameter] public int Radio { get; set; } = 10;
        [Parameter] public EventCallback<int> SelectedPage { get; set; }
        [Parameter] public EventCallback<int> RecordNumber { get; set; }

        protected override void OnParametersSet()
        {
            BuildPages();
            BuildOptions();
        }

        private void BuildOptions()
        {
            options =
                [
                new OptionModel { Value = 10, Name = "10" },
                new OptionModel { Value = 25, Name = "25" },
                new OptionModel { Value = 50, Name = "50" },
                new OptionModel { Value = int.MaxValue, Name = "Todos" },
                ];
        }

        private async Task InternalRecordsNumberSelected(ChangeEventArgs e)
        {
            if(e.Value != null)
            {
                selectedOptionValue = Convert.ToInt32(e.Value.ToString());
            }
            await RecordNumber.InvokeAsync(selectedOptionValue);
        }

        private async Task InternalSelectedPage(PageModel pageModel)
        {
            if (pageModel.Page == CurrentPage || pageModel.Page == 0)
            {
                return;
            }

            await SelectedPage.InvokeAsync(pageModel.Page);
        }

        private void BuildPages()
        {
            links = [];
            var previousLinkEnable = CurrentPage != 1;
            var previousLinkPage = CurrentPage - 1;

            links.Add(new PageModel
            {
                Icon = "bi bi-chevron-left",
                Page = previousLinkPage,
                Enable = previousLinkEnable
            });

            for (int i = 1; i <= TotalPages; i++)
            {
                if (TotalPages <= Radio)
                {
                    links.Add(new PageModel
                    {
                        Page = i,
                        Enable = CurrentPage == i,
                        Text = $"{i}"
                    });
                }

                if (TotalPages > Radio && i <= Radio && CurrentPage <= Radio)
                {
                    links.Add(new PageModel
                    {
                        Page = i,
                        Enable = CurrentPage == i,
                        Text = $"{i}"
                    });
                }

                if (CurrentPage > Radio && i > CurrentPage - Radio && i <= CurrentPage)
                {
                    links.Add(new PageModel
                    {
                        Page = i,
                        Enable = CurrentPage == i,
                        Text = $"{i}"
                    });
                }
            }

            var linkNextEnable = CurrentPage != TotalPages;
            var linkNextPage = CurrentPage != TotalPages ? CurrentPage + 1 : CurrentPage;
            links.Add(new PageModel
            {
                Icon = "bi bi-chevron-right",
                Page = linkNextPage,
                Enable = linkNextEnable
            });
        }

        private class OptionModel
        {
            public string Name { get; set; } = null!;
            public int Value { get; set; }
        }

        private class PageModel
        {
            public string Text { get; set; } = null!;
            public string? Icon { get; set; }
            public int Page { get; set; }
            public bool Enable { get; set; } = true;
            public bool Active { get; set; } = false;
        }
    }
}