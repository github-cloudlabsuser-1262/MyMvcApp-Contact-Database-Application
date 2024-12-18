using Xunit;
using MyMvcApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Tests
{
    public class UnitTest1Test
    {
        [Fact]
        public void TestGetUser()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void TestCreateUser()
        {
            // Arrange
            var controller = new UserController();
            var newUser = new User { Id = 2, Name = "John Doe" };

            // Act
            var result = controller.Create(newUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestUpdateUser()
        {
            // Arrange
            var controller = new UserController();
            var updatedUser = new User { Id = 1, Name = "Jane Doe" };

            // Act
            var result = controller.Edit(1, updatedUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void TestDeleteUser()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Delete(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}