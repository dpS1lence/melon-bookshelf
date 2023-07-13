namespace MelonBookshelfBlazorApp.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;

        public bool ValidateRegistration()
        {
            if(IsNullOrDefaultOrEmmpty(new List<string>() { FirstName, LastName, Email, Password, ConfirmPassword }))
            {
                return false;
            }

            if(Password != ConfirmPassword) 
            {
                return false; 
            }

            return true;
        }

        private static bool IsNullOrDefaultOrEmmpty(List<string> strings)
        {
            foreach(string s in strings)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s) || s == default) return false;
            }

            return true;
        }
    }
}
