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
}
