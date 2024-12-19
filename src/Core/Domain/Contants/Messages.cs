using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItem.Domain.Contants
{
    public static class Messages
    {
        public const string CRATED_FAILED_INVALIDARGUEMNT = "Can Not create Instance of {0} because Argument {1} is Not valid";
        public const string CREATED_FAILED_RULEVIOLATION = "Can Not create Instance of {0} because Rule : {1} was Violated.";
    }
}
