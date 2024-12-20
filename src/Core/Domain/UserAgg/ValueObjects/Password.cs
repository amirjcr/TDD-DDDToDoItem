using ToDoItem.Sahred.Primtives;
using ToDoItem.Domain.Exceptions;

namespace ToDoItem.Domain.UserAgg.ValueObjects
{
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
        public string? Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
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
