using System;
using Lab02_03.Exceptions;

namespace Lab02_03.Models
{
    internal class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public bool IsAdult { get; }
        public string SunSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }
        
        private bool IsValidName(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^[\p{L}]+$");
        }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(firstName) || !IsValidName(firstName))
                throw new InvalidFirstNameException(firstName);
            FirstName = firstName;

            if (string.IsNullOrWhiteSpace(lastName) || !IsValidName(lastName))
                throw new InvalidLastNameException(lastName);
            LastName = lastName;

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new InvalidEmailException(email);
            Email = email;

            if (birthDate > DateTime.Today)
                throw new InvalidBirthDateException("Birth date cannot be in the future.");
            int age = CalculateAge(birthDate);
            if (age > 135)
                throw new InvalidBirthDateException("Age cannot be higher than 135.");

            BirthDate = birthDate;

            IsAdult = age >= 18;
            SunSign = CalculateSunSign();
            ChineseSign = CalculateChineseSign();
            IsBirthday = BirthDate.Day == DateTime.Today.Day && BirthDate.Month == DateTime.Today.Month;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        private string CalculateSunSign()
        {
            int day = BirthDate.Day;
            int month = BirthDate.Month;
            if ((month == 3 && day >= 21) || (month == 4 && day <= 19)) return "Aries";
            if ((month == 4 && day >= 20) || (month == 5 && day <= 20)) return "Taurus";
            if ((month == 5 && day >= 21) || (month == 6 && day <= 20)) return "Gemini";
            if ((month == 6 && day >= 21) || (month == 7 && day <= 22)) return "Cancer";
            if ((month == 7 && day >= 23) || (month == 8 && day <= 22)) return "Leo";
            if ((month == 8 && day >= 23) || (month == 9 && day <= 22)) return "Virgo";
            if ((month == 9 && day >= 23) || (month == 10 && day <= 22)) return "Libra";
            if ((month == 10 && day >= 23) || (month == 11 && day <= 21)) return "Scorpio";
            if ((month == 11 && day >= 22) || (month == 12 && day <= 21)) return "Sagittarius";
            if ((month == 12 && day >= 22) || (month == 1 && day <= 19)) return "Capricorn";
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18)) return "Aquarius";
            return "Pisces";
        }

        private string CalculateChineseSign()
        {
            string[] signs = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return signs[BirthDate.Year % 12];
        }
    }
}