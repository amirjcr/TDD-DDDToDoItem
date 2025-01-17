using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ToDoItem.Application.Contexts;
using ToDoItem.Application.UserAgg.Commands.Create;
using ToDoItem.Application.UserAgg.Repository;
using ToDoItem.Application.UserAgg.Service;
using ToDoItem.Domain.UserAgg;
using ToDoItem.Domain.UserAgg.Repository;
using ToDoItem.Domain.UserAgg.Service;
using ToDoItem.Domain.UserAgg.ValueObjects;
using ToDoItem.Persistance.Contexts;

namespace ToDoItem.Test.UserAgg.Commands
{
    [TestClass]
    public class UserCreateCommandHandlerTests
    {
        private IUserRepository _userRepo = default!;
        private IDatabaseContext _context = default!;
        private IUserService _userService = default!;
        [TestInitialize]
        public void InitiateTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

            _context = new DatabaseContext(options);
            _userRepo = new UserRepository(_context);
            _userService = new UserService(_userRepo);
        }

        [TestMethod]
        public void Handler_InstanstiateWithDefaultDependencies_WillReturnInstanceOfHandler()
        {
            var handler = new UserCreateCommandHanlder(_userService, _userRepo);
            Assert.IsNotNull(handler);
        }

        [TestMethod]
        public async Task Handler_ExecuteHandlerMethod_ExecutionWillBeSuccessful()
        {
            var email = new EmailAddress("test@gmail.com");
            var password = new Password("testpassword34dk");
            var command = new UserCreateCommand("this is test name", email, password);
            var handler = new UserCreateCommandHanlder(_userService, _userRepo);
            await handler.Handle(command, default);
        }

        [TestMethod]
        public async Task Handler_InsertUserWithDefaultValues_WillInsertSuccessfuly()
        {
            var email = new EmailAddress("test@gmail.com");
            var password = new Password("testpassword34dk");
            var command = new UserCreateCommand("this is test name", email, password);
            var handler = new UserCreateCommandHanlder(_userService, _userRepo);
            await handler.Handle(command, default);

            Assert.IsTrue(_context.Users.Any());
        }


        [TestMethod]
        public async Task Handler_InsertUserWithDefaultValues_WillReturnIdOfUserCreated()
        {
            var email = new EmailAddress("test@gmail.com");
            var password = new Password("testpassword34dk");
            var command = new UserCreateCommand("this is test name", email, password);
            var handler = new UserCreateCommandHanlder(_userService, _userRepo);
            var result = await handler.Handle(command, default);
            var user = _userRepo.GetById(result.Data);
            Assert.IsNotNull(user);
        }


        [TestMethod]
        public async Task Handler_CancelUserInsertion_WillThrownOprationCancledException()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(400));
            var email = new EmailAddress("test@gmail.com");
            var password = new Password("testpassword34dk");
            var command = new UserCreateCommand("this is test name", email, password);
            var handler = new UserCreateCommandHanlder(_userService, _userRepo);
            await Task.Delay(500);
            var result = await handler.Handle(command, cts.Token);
            Assert.AreEqual(typeof(OperationCanceledException), result.Exception?.GetType());
            cts.Dispose();
        }
    }
}
