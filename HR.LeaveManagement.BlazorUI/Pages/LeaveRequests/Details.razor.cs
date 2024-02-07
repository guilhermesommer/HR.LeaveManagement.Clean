using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ILeaveRequestService _leaveRequestService { get; set; }

        [Parameter]
        public int id { get; set; }

        string ClassName;
        string HeadingText;

        public LeaveRequestVM Model { get; private set; } = new LeaveRequestVM();

        protected override async Task OnParametersSetAsync()
        {
            Model = await _leaveRequestService.GetLeaveRequest(id);
        }

        protected override async Task OnInitializedAsync()
        {
            if (Model.Approved == null)
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }
            else if (Model.Approved == true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }
            else
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }
        }

        private async Task ChangeApproval(bool approvalStatus)
        {
            await _leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}