namespace BackShopCore.Models
{
    public class Customer
    {
        public int CustomerId { get; private set; }

        //private properties
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateOnly _dateOfBirth;

        //public properties
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string Email => _email;
        public DateOnly DateOfBirth => _dateOfBirth;
        public bool IsValid { get; private set; }

        private Customer(){}
        private Customer(int customerId, string firstName, string lastName, string email, DateOnly dateOfBirth)
        {
            CustomerId = customerId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
        }

        //public methods
        public static Customer RegisterNew(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            var customer = new Customer();
            customer.SetFirstName(firstName: firstName);
            customer.SetLastName(lastName: lastName);
            customer.SetEmail(email: email);
            customer.SetDateOfBirth(dateOfBirth: dateOfBirth);
            customer.Validate();

            return customer;
        }

        public static Customer SetExistingInfo(int customerId, string firstName, string lastName, string email, DateOnly dateOfBirth)
        {
            var customer = new Customer(customerId: customerId, firstName: firstName, lastName: lastName, email: email, dateOfBirth: dateOfBirth);
            customer.Validate();

            return customer;
        }

        //private methods
        private void SetFirstName(string firstName)
        {
            if (firstName.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(firstName), "The firstName length cannot exceed 40 characters.");
            }

            _firstName = firstName;
        }

        private void SetLastName(string lastName)
        {
            if (lastName.Length > 40)
            {
                throw new ArgumentOutOfRangeException(nameof(lastName), "The lastName length cannot exceed 40 characters.");
            }

            _lastName = lastName;
        }

        private void SetEmail(string email)
        {
            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            _email = email;
        }

        private bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }


        private void SetDateOfBirth(DateTime dateOfBirth)
        {
            var dateNow = DateTime.Now;

            if (dateOfBirth.ToUniversalTime().Date > dateNow.Date)
            {
                throw new ArgumentOutOfRangeException(nameof(dateOfBirth));
            }

            _dateOfBirth = DateOnly.FromDateTime(dateTime: dateOfBirth);
        }

        private void Validate()
        {
            var dateNow = DateTime.Now;

            IsValid = _firstName.Length <= 40
            && _lastName.Length <= 40
            && _dateOfBirth.Day <= dateNow.Day
            && _dateOfBirth.Month <= dateNow.Month
            && _dateOfBirth.Year <= dateNow.Year;
        }
    }
}