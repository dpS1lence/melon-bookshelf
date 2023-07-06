namespace MelonBookshelfApi.ResponceModels
{
    public class PhysicalResourceTaken : PhysicalResource
    {
        public DateTime ExpectedReturnDate
        {
            get
            {
                return this.DateAdded.AddDays(20);
            }
        }
    }
}
