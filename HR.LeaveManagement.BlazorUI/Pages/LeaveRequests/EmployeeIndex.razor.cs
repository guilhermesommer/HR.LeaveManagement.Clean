using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Runtime.InteropServices;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class EmployeeIndex
    {
        [Inject]
        ILeaveRequestService _leaveRequestService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        IJSRuntime _js { get; set; }

        public EmployeeLeaveRequestViewVM Model { get; set; } = new();

        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Model = await _leaveRequestService.GetUserLeaveRequests();
        }

        async Task CancelRequestAsync(int id)
        {
            var confirm = await _js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");

            if (confirm)
            {
                var response = await _leaveRequestService.CancelLeaveRequest(id);
                if (response.Success)
                {
                    StateHasChanged();
                }
                else
                {
                    Message = response.Message;
                }
            }
        }
    }
}