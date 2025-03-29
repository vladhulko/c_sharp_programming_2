namespace Lab02_03.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public class InvalidEmailException : ValidationException
    {
        public InvalidEmailException(string email) 
            : base($"The email '{email}' is invalid. Please provide a valid email address.") { }
    }

    public class InvalidFirstNameException : ValidationException
    {
        public InvalidFirstNameException(string firstName) 
            : base($"The first name '{firstName}' is invalid. Spaces are not allowed.") { }
    }

    public class InvalidLastNameException : ValidationException
    {
        public InvalidLastNameException(string lastName) 
            : base($"The last name '{lastName}' is invalid. Spaces are not allowed.") { }
    }

    public class InvalidBirthDateException : ValidationException
    {
        public InvalidBirthDateException(string message) : base(message) { }
    }
}