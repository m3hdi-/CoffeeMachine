using CoffeeMachine.Controllers;
using CoffeeMachine.EntityManagers.Interfaces;
using CoffeeMachine.Models.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace CoffeMachineTest
{
    public class CoffeeMachineControllerTest
    {
        private readonly CoffeeMachineController _ctrl;
        private readonly Mock<ICoffeeMachineManager> _coffeeMachineManager = new Mock<ICoffeeMachineManager>();

        public CoffeeMachineControllerTest()
        {
            _ctrl = new CoffeeMachineController(_coffeeMachineManager.Object);
        }

        private void setup()
        {
            IQueryable<Dose> doses = new Dose[]
            {
                new Dose
                {
                    Id = 1,
                    Type = "Milk",
                    IsBadge = true,
                    IsMug = true,
                    CreatedAt = Convert.ToDateTime("2021-04-09 11:04:09.940"),
                    User = "John"
                },
                new Dose
                {
                    Id = 1,
                    Type = "Tea",
                    IsBadge = true,
                    IsMug = true,
                    CreatedAt = Convert.ToDateTime("2021-04-09 12:04:09.940"),
                    User = "John"
                },
                new Dose
                {
                    Id = 1,
                    Type = "Milk",
                    IsBadge = true,
                    IsMug = true,
                    CreatedAt = Convert.ToDateTime("2021-04-09 13:04:09.940"),
                    User = "Marc"
                }
            }.AsQueryable();

        }

        [Fact]
        public void GetDoses_ReturnOk()
        {
            //Arrange

            IQueryable<Dose> doses = new Dose[]
            {
                            new Dose
                            {
                                Id = 1,
                                Type = "Milk",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 11:04:09.940"),
                                User = "John"
                            },
                            new Dose
                            {
                                Id = 1,
                                Type = "Tea",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 12:04:09.940"),
                                User = "John"
                            },
                            new Dose
                            {
                                Id = 1,
                                Type = "Milk",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 13:04:09.940"),
                                User = "Marc"
                            }
            }.AsQueryable();

            _coffeeMachineManager.Setup(x => x.GetDoses()).Returns(doses);

            //Act
            dynamic response = _ctrl.GetDoses();

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, response.Result.StatusCode);

        }


        [Fact]
        public async void GetLastDose_ReturnOK()
        {
            //Arrange
            bool isBadged = true;
            string userName = "John";

            IQueryable<Dose> doses = new Dose[]
            {
                            new Dose
                            {
                                Id = 1,
                                Type = "Milk",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 11:04:09.940"),
                                User = "John"
                            },
                            new Dose
                            {
                                Id = 1,
                                Type = "Tea",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 12:04:09.940"),
                                User = "John"
                            },
                            new Dose
                            {
                                Id = 1,
                                Type = "Milk",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 13:04:09.940"),
                                User = "Marc"
                            }
            }.AsQueryable();
            _coffeeMachineManager.Setup(x => x.GetLastDose(isBadged, userName)).Returns(doses.Where(x => x.User == userName).OrderByDescending(c => c.CreatedAt));

            //Act
            dynamic response = _ctrl.GetLastDose(isBadged, userName);


            //Assert
            Assert.Equal((int)HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Fact]
        public async void GetLastDose_ReturnNotFound()
        {
            //Arrange
            bool isBadged = false;
            string userName = "Yashin";

            IQueryable<Dose> doses = new Dose[]
            {
                            new Dose
                            {
                                Id = 1,
                                Type = "Milk",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 11:04:09.940"),
                                User = "John"
                            },
                            new Dose
                            {
                                Id = 1,
                                Type = "Tea",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 12:04:09.940"),
                                User = "John"
                            },
                            new Dose
                            {
                                Id = 1,
                                Type = "Milk",
                                IsBadge = true,
                                IsMug = true,
                                CreatedAt = Convert.ToDateTime("2021-04-09 13:04:09.940"),
                                User = "Marc"
                            }
            }.AsQueryable();
            _coffeeMachineManager.Setup(x => x.GetLastDose(isBadged, userName)).Returns(doses.Where(x => x.User == userName).OrderByDescending(c => c.CreatedAt));

            //Act
            dynamic response = _ctrl.GetLastDose(isBadged, userName);


            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, response.Result.StatusCode);
        }

        [Fact]
        public async void CreateCommand_ReturnOk()
        {
            //Arrange

            Dose newCmd = new Dose();
            newCmd.Type = "Milk";
            newCmd.IsMug = true;
            newCmd.IsBadge = true;
            newCmd.User = "Alex";
            

            _coffeeMachineManager.Setup(x => x.CreateDose(newCmd)).Returns(DoseAsync());

            //Act
            dynamic response = _ctrl.CreateCommand(newCmd.Type, newCmd.IsMug, newCmd.IsBadge, newCmd.User);

            //Assert
            Assert.Equal((int)HttpStatusCode.Created, response.Result.Result.StatusCode);
        }

        private async Task<Dose> DoseAsync()
        {
            await Task.Delay(10000);
            Dose newCmd = new Dose();
            newCmd.Type = "Milk";
            newCmd.IsMug = true;
            newCmd.IsBadge = true;
            newCmd.User = "Alex";

            return newCmd;
        }

    }
}