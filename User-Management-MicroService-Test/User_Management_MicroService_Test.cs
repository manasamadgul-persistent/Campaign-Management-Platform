using CMPApiMicroservice.Controller;
using CMPApiMicroservice.Models;
using CMPApiMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace User_Management_MicroService_Test
{
    public class User_Management_MicroService_Test
    {
        public Mock<IUserRepository> mock = new Mock<IUserRepository>();
        [Fact]
        public void GetUser_WhenCalled_Get()
        {
            //arrange
            mock.Setup(p => p.GetUser());
            UserController user = new UserController(mock.Object);
            //Act
            IActionResult response = user.Get();
            //Assert
            var objectResponse = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public void GetUserById_WhenCalled_Get()
        {
            // arrange
            var userDTO = new User()
            {
                Id = 1,
                Name = "test1",
                PhoneNumber = "123456",
                Email = "user@test.com",
                Age = 30,
                Address = "",
                State = 0,
            };
            mock.Setup(p => p.GetUserById(1));
            UserController emp1 = new UserController(mock.Object);
            // Act
            var result = emp1.Get(1);
            // Assert
            //Assert.True(empDTO.Equals(result));
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RemoveUser_WhenCalled_DeleteUser()
        {
            // arrange
            mock.Setup(p => p.DeleteUser(1));
            UserController user = new UserController(mock.Object);
            // Act
            IActionResult response = user.Delete(1);
            // Assert
            var objectResponse = Assert.IsType<StatusCodeResult>(response);

            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            User testItem = new User()
            {
                Id = 1,
                Name = "test1",
                PhoneNumber = "123456",
                Email = "user@test.com",
                Age = 30,
                Address = "",
                State = 0,
            };
            mock.Setup(p => p.InsertUser(testItem));
            UserController user = new UserController(mock.Object);
            // Act
            IActionResult createdResponse = user.Post(testItem);
            // Assert
            var objectResponse = Assert.IsType<ObjectResult>(createdResponse);

            Assert.Equal(201, objectResponse.StatusCode);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnsUpdatedResponse()
        {
            // Arrange
            User testItem = new User()
            {
                Id = 1,
                Name = "test1",
                PhoneNumber = "123456",
                Email = "user@test.com",
                Age = 30,
                Address = "",
                State = 0,
            };
            mock.Setup(p => p.UpdateUser(testItem));
            UserController user = new UserController(mock.Object);
            // Act
            IActionResult createdResponse = user.Put(testItem);
            // Assert
            var objectResponse = Assert.IsType<ObjectResult>(createdResponse);

            Assert.Equal(200, objectResponse.StatusCode);
        }
    }
}
