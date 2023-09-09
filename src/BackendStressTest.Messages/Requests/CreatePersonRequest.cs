using System.Globalization;

namespace BackendStressTest.Messages.Requests
{
    public class CreatePersonRequest
    {
        public string? Name { get; set; }
        
        public string? Nickname { get; set; }
        
        public DateOnly Birthdate { get; set; }

        public string[]? Stack { get; set; }

        public bool Validate()
        {
            var basisValidation = ValidateName() && ValidateNickname() && ValidateBirthdate();

            if (basisValidation && Stack != null)
            {
                return ValidateStack();
            }

            return basisValidation;
        }

        private bool ValidateStack()
        {
            return !Stack.Any(x => string.IsNullOrWhiteSpace(x) || x.Length > 32);
        }

        private bool ValidateName()
        {
            return Name.Length <= 100;
        }

        private bool ValidateBirthdate()
        {
            DateOnly expectedDate;
            return DateOnly.TryParseExact(Birthdate.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out expectedDate);
        }

        private bool ValidateNickname()
        {
            return Nickname.Length <= 32;
        }
    }
}
