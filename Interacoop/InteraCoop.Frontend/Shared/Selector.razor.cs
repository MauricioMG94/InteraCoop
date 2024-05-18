using InteraCoop.Frontend.Helpers;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Shared
{
    public partial class Selector
    {
        [Parameter]
        public List<MultipleSelectorModel> NonSelected { get; set; } = new();

        [Parameter]
        public List<MultipleSelectorModel> Selected { get; set; } = new();

        private void Select(MultipleSelectorModel item)
        {
            if (NonSelected.Contains(item))
            {
                if (Selected != null && Selected.Count > 0)
                {
                    var currentValue = Selected.First();
                    NonSelected.Add(currentValue);
                    Selected.Clear();
                }

                Selected.Add(item);
                NonSelected.Remove(item);
            }
        }

        private void SelectClear(MultipleSelectorModel item)
        {
            Selected.Clear();
            NonSelected.Add(item);
        }
    }
}
