using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using ToDoAPI.Models;
using ToDoAPI.AppDataContext;

namespace ToDoAPI.Tests
{
    [TestFixture]
    public class TodoDbContextTests
    {
        private DbContextOptions<TodoDbContext> _options;
        private TodoDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Use an in-memory database for testing
            _options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoTestDb")
                .Options;

            // Create a new instance of the DbContext
            _context = new TodoDbContext(Options.Create(new DbSettings { ConnectionString = "InMemoryDb" }));
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the context after each test
            _context.Dispose();
        }

        [Test]
        public void CanAddTodoItem()
        {
            var todo = new Todo { Id = Guid.NewGuid(), Title = "Test Todo", IsComplete = false };

            // Act
            _context.Todos.Add(todo);
            _context.SaveChanges();

            // Assert
            var savedTodo = _context.Todos.FirstOrDefault(t => t.Id == todo.Id); // Compare with Guid
            Assert.IsNotNull(savedTodo);
            Assert.AreEqual("Test Todo", savedTodo.Title);
            Assert.IsFalse(savedTodo.IsComplete);
        }

        [Test]
        public void CanUpdateTodoItem()
        {
            // Arrange
            var todo = new Todo { Id =  Guid.NewGuid(), Title = "Test Todo", IsComplete = false };
            _context.Todos.Add(todo);
            _context.SaveChanges();

            // Act
            var savedTodo = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
            savedTodo.Title = "Updated Todo";
            savedTodo.IsComplete = true;
            _context.SaveChanges();

            // Assert
            var updatedTodo = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
            Assert.IsNotNull(updatedTodo);
            Assert.AreEqual("Updated Todo", updatedTodo.Title);
            Assert.IsTrue(updatedTodo.IsComplete);
        }

        [Test]
        public void CanDeleteTodoItem()
        {
            // Arrange
            var todo = new Todo { Id =  Guid.NewGuid(), Title = "Test Todo", IsComplete = false };
            _context.Todos.Add(todo);
            _context.SaveChanges();

            // Act
            var savedTodo = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
            _context.Todos.Remove(savedTodo);
            _context.SaveChanges();

            // Assert
            var deletedTodo = _context.Todos.FirstOrDefault(t => t.Id == todo.Id);
            Assert.IsNull(deletedTodo);
        }
    }
}