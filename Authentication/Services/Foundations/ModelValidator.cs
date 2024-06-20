using System.Net.Mail;
using System.Text.RegularExpressions;
using System;

namespace Authentication.Services.Foundations
{
    public static class ModelValidator
    {
        public static void Validate<TResponseException>(this TResponseException responseException, params (dynamic Rule, string Parameter)[] validations) where TResponseException : ConnException
        {
            for (int i = 0; i < validations.Length; i++) {
                dynamic val;
                string text;
                (val, text) = ((object, string))validations[i];
                if (val.Condition) {
                    responseException.UpsertDataList(key: text, value: val.Message);
                }
            }

            responseException.ThrowIfContainsErrors();
        }

        public static dynamic IsInvalid(string value, string fieldName)
        {
            return new
            {
                Condition = string.IsNullOrWhiteSpace(value),
                Message = fieldName + " is required"
            };
        }

        public static dynamic IsInvalid(Guid value, string fieldName)
        {
            return new
            {
                Condition = (value == Guid.Empty),
                Message = fieldName + " is required"
            };
        }

        public static dynamic IsInvalid(DateTimeOffset input, string fieldName)
        {
            return new
            {
                Condition = (input == default(DateTimeOffset)),
                Message = fieldName + " is required"
            };
        }

        public static dynamic IsInvalid(int value, string fieldName)
        {
            return new
            {
                Condition = (value <= 0),
                Message = fieldName + " is required"
            };
        }

        public static dynamic IsInvalid(decimal value, string fieldName)
        {
            return new
            {
                Condition = (value <= 0m),
                Message = fieldName + " is required"
            };
        }

        public static dynamic IsInvalid<T>(T value, string fieldName) where T : class
        {
            return new
            {
                Condition = (value == null),
                Message = fieldName + " is required"
            };
        }

        public static dynamic IsInvalidEmail(string email)
        {
            return new
            {
                Condition = InvalidEmail(email),
                Message = "Invalid email! Email should be in a format like 'email@abc.xyz'."
            };
        }

        private static bool InvalidEmail(string email)
        {
            try {
                if (string.IsNullOrWhiteSpace(email)) {
                    return true;
                }

                new MailAddress(email);
                return false;
            }
            catch (FormatException) {
                return true;
            }
        }

        public static dynamic IsInvalidPassword(string password)
        {
            return new
            {
                Condition = InvalidPassword(password),
                Message = "Invalid password! Password must be at least 12 characters long and contain at least one number, one lowercase, one uppercase and one special character."
            };
        }

        private static bool InvalidPassword(string password)
        {
            Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{12,}$");
            if (string.IsNullOrWhiteSpace(password) || !regex.IsMatch(password)) {
                return true;
            }

            return false;
        }
    }
}
