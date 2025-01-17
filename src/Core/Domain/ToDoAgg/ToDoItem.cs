using ToDoItem.Domain.Contants;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.ToDoAgg.Enums;
using ToDoItem.Domain.ToDoAgg.Services;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Sahred.Configurations;

namespace ToDoItem.Domain.ToDoAgg
{
    [Audtiable]
    public sealed class ToDoItem
    {
        private ToDoItem() { }
        private ToDoItem(string title, DateTime finishedDate, Priority priority, int userCreated, IToDoItemService service)
        {
            CreateGuard(title, finishedDate, priority, userCreated, service);

            Title = title;
            FinishedDate = finishedDate;
            Priority = priority;
            UserCreated = userCreated;
            CreatedDate = DateTime.Now;
        }

        public int Id { get; private set; }
        public string? Title { get; private set; }
        public DateTime FinishedDate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Priority Priority { get; private set; }
        public int UserCreated { get; private set; }
        public User? User { get; private set; }


        public void Update(string title, DateTime finishedDate, Priority priority)
        {
            UpdateGaurd(title, finishedDate, priority);

            this.Title = this.Title != title ? title : this.Title;
            this.FinishedDate = this.FinishedDate != finishedDate ? finishedDate : this.FinishedDate;
            this.Priority = this.Priority != priority ? priority : this.Priority;
        }
        public void Update(Priority priority)
        {
            PriorityGuard(priority);
            this.Priority = this.Priority != priority ? priority : this.Priority;
        }
        public void Update(string title)
        {
            TitleGuard(title);
            this.Title = this.Title != title ? title : this.Title;
        }
        public void Update(DateTime finishedDate)
        {
            FinishedDateGuard(finishedDate);
            this.FinishedDate = this.FinishedDate != finishedDate ? finishedDate : this.FinishedDate;
        }


        public static ToDoItem CreateItem(string title, DateTime finishedDate, Priority priority, int userCreated, IToDoItemService service)
        {
            return new ToDoItem(title, finishedDate, priority, userCreated, service);
        }


        #region GuardMethods
        private void CreateGuard(string title, DateTime finishedDate, Priority priority, int userCreated, IToDoItemService service)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainRulesViolatedException(string.Format(Messages.CRATED_FAILED_INVALIDARGUEMNT, typeof(ToDoItem), nameof(title)));
            if (userCreated <= 0)
                throw new DomainRulesViolatedException(string.Format(Messages.CRATED_FAILED_INVALIDARGUEMNT, typeof(ToDoItem), nameof(userCreated)));
            if (finishedDate <= DateTime.Now)
                throw new DomainRulesViolatedException(string.Format(Messages.CREATED_FAILED_RULEVIOLATION, typeof(ToDoItem), "FinishedDate Must be Greater Than CurrentDate"));

            var validateUser = service.CheckUser<Exception>(userCreated);
            if (!validateUser.isValid)
                throw validateUser.exception;
        }

        private void UpdateGaurd(string title, DateTime finishedDate, Priority priority)
        {
            TitleGuard(title);
            FinishedDateGuard(finishedDate);
            PriorityGuard(priority);
        }

        public void PriorityGuard(Priority priority)
        {
            if (priority is 0)
                throw new DomainRulesViolatedException("Priority Can not Be 0 or default");
        }
        public void FinishedDateGuard(DateTime finishedDate)
        {
            if (finishedDate < CreatedDate)
                throw new DomainRulesViolatedException("FinishedDate Can Not Be Samller Than CreatedDate");
        }
        private void TitleGuard(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainRulesViolatedException("Title Can Not be Empty Or Null");
        }

        #endregion
    }
}
