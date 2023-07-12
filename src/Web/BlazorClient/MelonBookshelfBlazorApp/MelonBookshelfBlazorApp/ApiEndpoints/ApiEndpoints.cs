namespace MelonBookshelfBlazorApp.ApiEndpoints
{
    public static class ApiEndpoints
    {
        public static class Authentication
        {
            public static string Register { get => "users/register"; }

            public static string Login { get => "users/login"; }
        }

        public static class BaseUserActions
        {
            public static string ReturnPhysicalResource { get => "user-actions/{0}/{1}/return-physical-resource"; }
            public static string GetPhysicalResource { get => "user-actions/{0}/{1}/get-physical-resource"; }
            public static string UpvoteRequest { get => "user-actions/{0}/{1}/upvote"; }
            public static string FollowRequest { get => "user-actions/{0}/{1}/follow"; }
        }

        public static class HRActions
        {
            public static string ConfirmRequest { get => "hr-actions/requests/{0}/confirm"; }
            public static string RejectRequest { get => "hr-actions/requests/{0}/reject"; }
            public static string SetRequestInProgress { get => "hr-actions/requests/{0}/inprogress"; }
            public static string SetRequestInDelivery { get => "hr-actions/requests/{0}/delivery"; }
            public static string SetRequestDelivered { get => "hr-actions/requests/{0}/delivered"; }
        }

        public static class HRDashboard
        {
            public static string GetRequests { get => "hrdashboard/requests"; }
            public static string GetAllResourcesForHR { get => "hrdashboard/resources"; }
        }

        public static class RequestsData
        {
            public static string AddRequest { get => "requests/create"; }
            public static string GetRequestsByUserId { get => "requests/{0}/get-by-userid"; }
            public static string GetRequestById { get => "requests/{0}"; }
        }

        public static class ResourcesData
        {
            public static string Resources { get => "resources"; }
            public static string SearchResources { get => "resources/search"; }
            public static string GetResourcesCategories { get => "resources/categories"; }
            public static string PhysicalResourceById { get => "resources/{0}"; }
            public static string ResourceById { get => "resources/{0}"; }
            public static string PhysicalResourcesByUserId { get => "resources/{0}/taken"; }
        }
    }
}
