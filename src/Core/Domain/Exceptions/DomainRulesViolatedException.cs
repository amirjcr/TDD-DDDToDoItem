using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItem.Domain.Exceptions
{
    public sealed class DomainRulesViolatedException : Exception
    {
        public DomainRulesViolatedException(string message)
            : base(message) { }
    }

    public sealed class DomainEntityNotFoundException : Exception
    {
        public DomainEntityNotFoundException(string message)
            : base(message)
        { }
    }

    public sealed class DomainInvalidUserException : Exception
    {
        public DomainInvalidUserException(string message)
            : base(message)
        { }
    }
}
