using ToDoItem.Sahred.Primtives;
using ToDoItem.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace ToDoItem.Domain.UserAgg.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {

        #region Constractors
        public EmailAddress(string email)
        {
            Gaurd(email);
            this.Address = email;
        }
        private EmailAddress() { }

        #endregion

        public string? Address { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address!;
        }

        private void Gaurd(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainRulesViolatedException("email can not be empty");

            var emailValidation = new EmailAddressAttribute();

            if (!emailValidation.IsValid(email))
                throw new DomainRulesViolatedException("email is invalid");

        }
    }

    public sealed class Password : ValueObject
    {
        #region Constractors
        public Password(string value)
        {
            Gaurd(value);
            Value = value;
        }

        private Password() { }

        #endregion
        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private void Gaurd(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainRulesViolatedException("password Can not be empty");
            else if (value.Length < 8)
                throw new DomainRulesViolatedException("Password can not be less than 8 charctures");
        }
    }
}
