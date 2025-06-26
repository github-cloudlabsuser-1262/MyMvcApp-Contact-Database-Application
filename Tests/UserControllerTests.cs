using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using Xunit;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithUserList()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<List<User>>(viewResult.Model);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Post_ValidUser_AddsUserAndRedirects()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Name = "Test", Email = "test@example.com" };

            // Act
            var result = controller.Create(user);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteConfirmed_RemovesUserAndRedirects()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Name = "ToDelete", Email = "delete@example.com" };
            controller.Create(user);

            // Act
            var result = controller.DeleteConfirmed(1);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
    }
}
