using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Application.UserAgg.Repository;
using ToDoItem.Application.UserAgg.Service;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Domain.UserAgg.Service;
using ToDoItem.Domain.UserAgg.ValueObjects;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test.UserAgg
{
    [TestClass]
    public class UserTests
    {
        private IUserService _service = default!;
        private IUserRepository userRepository = default!;
        [TestInitialize]
        public void Initializeer()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("testuserdb")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var context = new DatabaseContext(options);
            userRepository = new UserRepository(context);
            _service = new UserService(userRepository);
        }

        [TestMethod]
        public void Create_UserWitoutBusiness_ExceptUserCreated()
        {
            var user = User.Create("test user this test", new EmailAddress("sample@gamil.com"), new Password("testpass"), _service);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithOutName_ExceptError()
        {
            User.Create(string.Empty, new EmailAddress("test@gmail.com"), new Password("testpass"), _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_NameWithLowerLenght_ExceptError()
        {
            User.Create("test", new EmailAddress("test@gmail.com"), new Password("testpass"), _service);
        }


        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_NameWithHigerLenght_ExceptError()
        {
            var userName = @"sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                             sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                             sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                             sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";

            User.Create(userName!, new EmailAddress("test@gmail.com"), new Password("testpass"), _service);
        }


        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithOutEmail_ExceptError()
        {
            User.Create("test user", default!, new Password("testpasssssss"), _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Creaet_WithEmailButEmptyValue_ThrownException()
        {
            User.Create("test user", new EmailAddress(string.Empty), new Password("testpassssss"), _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void create_WithInvalidEmail_excepterror()
        {
            User.Create("test user", new EmailAddress("amir x gmail.com"), new Password("testpassssss"), _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithoutPassword_ThrownException()
        {
            User.Create("test user", new EmailAddress("Test@gmail.com"), default!, _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithEmptyPassword_ThrownException()
        {
            User.Create("test user", new EmailAddress("Test@gmail.com"), new Password(string.Empty), _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_PasswordWithLowLenght_ThrownException()
        {
            User.Create("test user", new EmailAddress("Test@gmail.com"), new Password("test"), _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void UpdateEmail_WithEmptyEmail_ThrowException()
        {
            var user = User.Create("test user", new EmailAddress("Test@gmail.com"), new Password("test"), _service);
            user.Update(default!, _service);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException), "Email Can Not Duplicate")]
        public void UpdateEmail_WithReptededEmail_ThrownException()
        {
            var user = User.Create("test user 10 s", new EmailAddress("Test@gmail.com"), new Password("testpassword must"), _service);
            user.Update(new EmailAddress("test@gmail.com"), _service);
        }
    }
}
