namespace MelonBookshelfBlazorApp.ApiEndpoints
{
    public class HRActionsOptions
    {
        public string ConfirmRequest { get; set; } = default!;

        public string RejectRequest { get; set; } = default!;

        public string SetResourceInProgress { get; set; } = default!;

        public string SetResourceInDelivery { get; set; } = default!;

        public string SetResourceDelivered { get; set; } = default!;
    }
}
