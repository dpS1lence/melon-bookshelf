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
            public static string ReturnPhysicalResource { get => "user-actions/{resourceId}/{userId}/return-physical-resource"; }
            public static string GetPhysicalResource { get => "user-actions/{resourceId}/{userId}/get-physical-resource"; }
            public static string UpvoteRequest { get => "user-actions/{requestId}/{userId}/upvote"; }
            public static string FollowRequest { get => "user-actions/{requestId}/{userId}/follow"; }
        }

        public static class HRActions
        {
            public static string ConfirmRequest { get => "hr-actions/requests/{requestId}/confirm"; }
            public static string RejectRequest { get => "hr-actions/requests/{requestId}/reject"; }
            public static string SetRequestInProgress { get => "hr-actions/requests/{requestId}/inprogress"; }
            public static string SetRequestInDelivery { get => "hr-actions/requests/{requestId}/delivery"; }
            public static string SetRequestDelivered { get => "hr-actions/requests/{requestId}/delivered"; }
        }

        public static class HRDashboard
        {
            public static string GetRequests { get => "hrdashboard/requests"; }
            public static string GetAllResourcesForHR { get => "hrdashboard/resources"; }
        }

        public static class RequestsData
        {
            public static string AddRequest { get => "requests/create"; }
            public static string GetRequestsByUserId { get => "requests/{userId}/get-by-userid"; }
            public static string GetRequestById { get => "requests/{requestId}"; }
        }

        public static class ResourcesData
        {
            public static string Resources { get => "resources"; }
            public static string SearchResources { get => "resources/search"; }
            public static string GetResourcesCategories { get => "resources/categories"; }
            public static string PhysicalResourceById { get => "resources/{physical-resource-id}"; }
            public static string ResourceById { get => "resources/{resource-id}"; }
            public static string PhysicalResourcesByUserId { get => "resources/{userId}/taken"; }
        }
    }
}
