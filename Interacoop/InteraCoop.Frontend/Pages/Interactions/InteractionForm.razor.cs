using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Helpers;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;

namespace InteraCoop.Frontend.Pages.Interactions
{
    [Authorize(Roles = "Employee")]
    public partial class InteractionForm
    {
        private EditContext editContext = null!;

        private List<MultipleSelectorModel> selected { get; set; } = new();
        private List<MultipleSelectorModel> nonSelected { get; set; } = new();
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter, EditorRequired] public InteractionDto Interaction { get; set; } = null!;
        [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
        [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
        [Parameter, EditorRequired] public List<Client> nonSelectedInteractions { get; set; } = new();
        [Parameter] public List<Client> selectedInteractions { get; set; } = new();
        public bool FormPostedSuccessfully { get; set; } = false;

        protected override void OnInitialized()
        {
            editContext = new(Interaction);

            Interaction.InteractionCreationDate = DateTime.Today;
            Interaction.StartDate = DateTime.Today;
            Interaction.EndDate = DateTime.Today;
            Interaction.AuditDate = DateTime.Today;
            Interaction.AuditUser = "Admin";

            selected = selectedInteractions.Select(x => new MultipleSelectorModel(x.Id.ToString(), x.Name)).ToList();
            nonSelected = nonSelectedInteractions.Select(x => new MultipleSelectorModel(x.Id.ToString(), x.Name)).ToList();
        }

        private async Task OnDataAnnotationsValidatedAsync()
        {
            Interaction.ClientsIds = selected.Select(x => int.Parse(x.Key)).ToList();
            await OnValidSubmit.InvokeAsync();
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited)
            {
                return;
            }

            if (FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = !string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

    }
}
