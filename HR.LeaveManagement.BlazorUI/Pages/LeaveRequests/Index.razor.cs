using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Index
    {
        [Inject]
        ILeaveRequestService _leaveRequestService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public AdminLeaveRequestViewVM Model { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Model = await _leaveRequestService.GetAdminLeaveRequestList();
        }

        private void GoToDetails(int id)
        {
            NavigationManager.NavigateTo($"/leaverequests/details/{id}");
        }

        private void CreateLeaveRequest()
        {
            NavigationManager.NavigateTo("/leaverequests/create/");
        }
    }
}