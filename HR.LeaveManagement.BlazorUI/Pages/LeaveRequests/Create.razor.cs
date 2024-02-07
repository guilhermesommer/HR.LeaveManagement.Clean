using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject]
        ILeaveTypeService _leaveTypeService { get; set; }

        [Inject]
        ILeaveRequestService _leaveRequestService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        
        LeaveRequestVM LeaveRequest { get; set; } = new LeaveRequestVM();

        List<LeaveTypeVM> leaveTypeVMs { get; set; } = new List<LeaveTypeVM>();

        protected override async Task OnInitializedAsync()
        {
            leaveTypeVMs = await _leaveTypeService.GetLeaveTypes();
        }

        private async Task HandleValidSubmit()
        {
            // Perform form submission here
            await _leaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}