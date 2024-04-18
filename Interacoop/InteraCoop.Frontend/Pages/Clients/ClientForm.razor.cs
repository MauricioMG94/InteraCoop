﻿using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace InteraCoop.Frontend.Pages.Clients
{
    public partial class ClientForm
    {
        private EditContext editContext = null!;

        [EditorRequired, Parameter] public Client Client { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; }

        protected override void OnInitialized()
        {
            editContext = new(Client);
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmacion",
                Text = "¿Deseas abandonar la pagina y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (!confirm)
            {
                return;
            }
            context.PreventNavigation();
        }
    }
}
