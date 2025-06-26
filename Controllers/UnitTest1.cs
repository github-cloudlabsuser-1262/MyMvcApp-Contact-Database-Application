using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Reflection;

namespace MyMvcApp.Tests.Controllers
{
    public class UserControllerTest
    {
        private UserController GetControllerWithUsers(List<User> users = null)
        {
            // Reset the static userlist using reflection for isolation between tests
            var userlistField = typeof(UserController).GetField("userlist", BindingFlags.Static | BindingFlags.NonPublic);
            userlistField.SetValue(null, users ?? new List<User>());
            return new UserController();
        }

        [Fact]
        public void Index_ReturnsViewWithUserList()
        {
            var users = new List<User> { new User { Id = 1, Name = "Test", Email = "test@email.com" } };
            var controller = GetControllerWithUsers(users);

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(users, result.Model);
        }

        [Fact]
        public void Details_ReturnsView_WhenUserExists()
        {
            var users = new List<User> { new User { Id = 2, Name = "Alice", Email = "alice@email.com" } };
            var controller = GetControllerWithUsers(users);

            var result = controller.Details(2) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(users[0], result.Model);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var controller = GetControllerWithUsers();

            var result = controller.Details(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            var controller = GetControllerWithUsers();

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_AddsUserAndRedirects_WhenModelStateIsValid()
        {
            var controller = GetControllerWithUsers();
            var user = new User { Name = "Bob", Email = "bob@email.com" };

            var result = controller.Create(user);

            Assert.IsType<RedirectToActionResult>(result);
            var userlistField = typeof(UserController).GetField("userlist", BindingFlags.Static | BindingFlags.NonPublic);
            var userlist = (List<User>)userlistField.GetValue(null);
            Assert.Single(userlist);
            Assert.Equal("Bob", userlist[0].Name);
        }

        [Fact]
        public void Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var controller = GetControllerWithUsers();
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User();

            var result = controller.Create(user);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsView_WhenUserExists()
        {
            var users = new List<User> { new User { Id = 3, Name = "Charlie", Email = "charlie@email.com" } };
            var controller = GetControllerWithUsers(users);

            var result = controller.Edit(3) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(users[0], result.Model);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var controller = GetControllerWithUsers();

            var result = controller.Edit(42);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_UpdatesUserAndRedirects_WhenModelStateIsValid()
        {
            var users = new List<User> { new User { Id = 4, Name = "Old", Email = "old@email.com" } };
            var controller = GetControllerWithUsers(users);
            var updatedUser = new User { Id = 4, Name = "New", Email = "new@email.com" };

            var result = controller.Edit(4, updatedUser);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("New", users[0].Name);
            Assert.Equal("new@email.com", users[0].Email);
        }

        [Fact]
        public void Edit_Post_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var controller = GetControllerWithUsers();
            var user = new User { Id = 5, Name = "Ghost", Email = "ghost@email.com" };

            var result = controller.Edit(5, user);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var users = new List<User> { new User { Id = 6, Name = "Test", Email = "test@email.com" } };
            var controller = GetControllerWithUsers(users);
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User();

            var result = controller.Edit(6, user);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsView_WhenUserExists()
        {
            var users = new List<User> { new User { Id = 7, Name = "Del", Email = "del@email.com" } };
            var controller = GetControllerWithUsers(users);

            var result = controller.Delete(7) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(users[0], result.Model);
        }

        [Fact]
        public void Delete_Get_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var controller = GetControllerWithUsers();

            var result = controller.Delete(100);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteConfirmed_RemovesUserAndRedirects()
        {
            var users = new List<User> { new User { Id = 8, Name = "ToDelete", Email = "del@email.com" } };
            var controller = GetControllerWithUsers(users);

            var result = controller.DeleteConfirmed(8);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Empty(users);
        }

        [Fact]
        public void DeleteConfirmed_DoesNothing_WhenUserDoesNotExist()
        {
            var users = new List<User> { new User { Id = 9, Name = "Keep", Email = "keep@email.com" } };
            var controller = GetControllerWithUsers(users);

            var result = controller.DeleteConfirmed(999);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Single(users);
        }
    }
}