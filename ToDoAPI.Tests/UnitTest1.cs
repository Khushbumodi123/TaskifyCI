// using Moq;
// using NUnit.Framework;
// using AutoMapper;
// using Microsoft.EntityFrameworkCore;
// using ToDoAPI.Services;
// using ToDoAPI.AppDataContext;
// using ToDoAPI.Models;
// using ToDoAPI.Interfaces;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using System.Threading.Tasks;

// namespace ToDoAPI.Tests.Unit.Services
// {
//     public class ToDoServicesTests
//     {
//         private Mock<ILogger<ToDoServices>> _mockLogger;
//         private Mock<IMapper> _mockMapper;
//         private Mock<IOptions<DbSettings>> _mockDbSettings;
//         private TodoDbContext _context;
//         private ToDoServices _todoService;

//         [SetUp]
//         public void SetUp()
//         {
//             _mockLogger = new Mock<ILogger<ToDoServices>>();
//             _mockMapper = new Mock<IMapper>();
//             _mockDbSettings = new Mock<IOptions<DbSettings>>();

//             // Set up an in-memory database for testing
//             var options = new DbContextOptionsBuilder<TodoDbContext>()
//                 .UseInMemoryDatabase("TestDatabase") // Unique in-memory database name for isolation
//                 .Options;

//             // Now, pass the mocked IOptions<DbSettings> to the TodoDbContext constructor
//             _context = new TodoDbContext(options);

//             // Ensure database is created
//             _context.Database.EnsureCreated();

//             // Create the service with the mocked dependencies
//             _todoService = new ToDoServices(_context, _mockLogger.Object, _mockMapper.Object);
//         }

//         [TearDown]
//         public void TearDown()
//         {
//             // Dispose the context after each test (clean up)
//             _context?.Dispose();
//         }

//         [Test]
//         public async Task CreateTodoAsync_ShouldCreateTodo()
//         {
//             // Arrange
//             var request = new CreateTodoRequest
//             {
//                 Title = "Test Todo",
//                 Description = "Test Description"
//             };

//             // Set up the mock for AutoMapper to map CreateTodoRequest to Todo
//             _mockMapper.Setup(m => m.Map<Todo>(It.IsAny<CreateTodoRequest>()))
//                 .Returns(new Todo { Title = request.Title, Description = request.Description });

//             // Act
//             var result = await _todoService.CreateTodoAsync(request);

//             // Assert
//             Assert.IsNotNull(result);  // Ensure the result is not null
//             Assert.AreEqual(request.Title, result.Title);  // Check that the title matches
//             Assert.AreEqual(request.Description, result.Description);  // Check that the description matches

//             // Check if the Todo was actually inserted into the database
//             var todoFromDb = await _context.Todos.FindAsync(result.Id);
//             Assert.IsNotNull(todoFromDb);  // Ensure the Todo exists in the database
//             Assert.AreEqual(request.Title, todoFromDb.Title);  // Check the title in the DB
//             Assert.AreEqual(request.Description, todoFromDb.Description);  // Check the description in the DB
//         }
//     }
// }
