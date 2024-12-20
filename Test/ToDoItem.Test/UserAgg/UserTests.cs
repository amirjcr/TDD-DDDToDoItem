using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItem.Domain.Exceptions;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Domain.UserAgg.ValueObjects;

namespace ToDoItem.Test.UserAgg
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Create_UserWitoutBusiness_ExceptUserCreated()
        {
            var user = User.Create("test user this test", new EmailAddress("sample@gamil.com"), new Password("testpass"));

            Assert.IsNotNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithOutName_ExceptError()
        {
            User.Create(string.Empty, new EmailAddress("test@gmail.com"), new Password("testpass"));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_NameWithLowerLenght_ExceptError()
        {
            User.Create("test", new EmailAddress("test@gmail.com"), new Password("testpass"));
        }


        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_NameWithHigerLenght_ExceptError()
        {
            var userName = @"sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                             sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                             sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                             sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss";

            User.Create(userName!, new EmailAddress("test@gmail.com"), new Password("testpass"));
        }


        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithOutEmail_ExceptError()
        {
            User.Create("test user", default!, new Password("testpasssssss"));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Creaet_WithEmailButEmptyValue_ThrownException()
        {
            User.Create("test user", new EmailAddress(string.Empty), new Password("testpassssss"));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void create_WithInvalidEmail_excepterror()
        {
            User.Create("test user", new EmailAddress("amir x gmail.com"), new Password("testpassssss"));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithoutPassword_ThrownException()
        {
            User.Create("test user", new EmailAddress("Test@gmail.com"), default!);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_WithEmptyPassword_ThrownException()
        {
            User.Create("test user", new EmailAddress("Test@gmail.com"), new Password(string.Empty));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainRulesViolatedException))]
        public void Create_PasswordWithLowLenght_ThrownException()
        {
            User.Create("test user", new EmailAddress("Test@gmail.com"), new Password("test"));
        }

    }
}
