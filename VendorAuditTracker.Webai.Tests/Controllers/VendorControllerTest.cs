using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using VendorAuditTracker.Webapi.Controllers;
using VendorAuditTracker.Webapi.DataTransferObjects.Response;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;
using VendorAuditTracker.Webapi.Tests.Utilities;

namespace VendorAuditTracker.Webapi.Tests.Controllers
{
    [TestClass]
    public class VendorControllerTest
    {
        private IDbContextFactory _mockDbContextFactory;

        [TestInitialize]
        public void Initialise()
        {
            _mockDbContextFactory = Substitute.For<IDbContextFactory>();
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            //VendorController controller = new VendorController();

            // Act
            //var result = controller.Get();

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        /// <summary>
        /// @scenario: Successfully retrieved all Vendor details
        /// </summary>
        /// <remarks>
        /// GIVEN A Vendors details are stored within the system
        /// WHEN A business application requests to retrieve all vendor details
        /// THEN a list of vendors should be recieved by the caller.
        /// </remarks>
        [TestMethod, TestCategory("2.4")]
        public void GetAllVendorDetails_Test()
        {
            // Setup
            //Mock the database as we only want to test the method
            var mockDbContext = Substitute.For<IVendorAuditDbContext>();
            mockDbContext.Vendors = NSubstituteUtils.CreateMockDbSet(new[]
            {
                new  Vendor
                {
                    Id = 1,
                    Name = "NGN",
                    PrimaryContact = "primary",
                    Projects = new List<Project>
                    {
                        new Project
                        {
                            Code = "PP123456",
                            CodeDrops = 
                            new List<CodeDrop>
                            {
                                new CodeDrop
                                {
                                    ActualDate = DateTime.Now.AddDays(5),
                                    Id = 1,
                                    TargetedDate = DateTime.Now,
                                    VendorName = "NGN"
                                }
                            },SoftwareRelease = new SoftwareRelease()
                        }},
                    SecondaryContact = "secondary"
                }
            });

            _mockDbContextFactory.DbContext.Returns(mockDbContext);

            //Action
            //Register the Mocked context factory via IOC
            Action<ContainerBuilder> autofacOverrides = builder =>
            {
                builder.RegisterInstance(_mockDbContextFactory).As<IDbContextFactory>();
            };

            VendorResponse result;

            using (var lifetimeScope = AutofacResolver.GetLifeTimeScope(autofacOverrides))
            {
                //Retreive service from scope
                var memberDocumentService = lifetimeScope.Resolve<VendorController>();

                var a = memberDocumentService.Get();

            }

            //Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(1, result.Vendors.Count, "The number Vendors returned should be 1");
            //Assert.IsTrue(result.Vendors[0].Projects[0].Code == "PP123456", "Invalid Project Code");
            //Assert.AreEqual(result.Vendors.First().Id, 1,
            //    "Wrong Id");
        }
    }
}
