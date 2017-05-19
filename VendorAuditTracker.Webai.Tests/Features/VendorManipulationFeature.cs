using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using VendorAuditTracker.Webapi.DataTransferObjects.Response;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;
using VendorAuditTracker.Webapi.Services;
using VendorAuditTracker.Webapi.Tests.Utilities;

namespace VendorAuditTracker.Webapi.Tests.Features
{

    [TestClass]
    public class VendorManipulationFeature
    {
        private IDbContextFactory _mockDbContextFactory;

        [TestInitialize]
        public void Initialise()
        {
            _mockDbContextFactory = Substitute.For<IDbContextFactory>();
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

            Action<ContainerBuilder> autofacOverrides = builder =>
            {
                builder.RegisterInstance(_mockDbContextFactory).As<IDbContextFactory>();
            };

            VendorResponse result;

            using (var lifetimeScope = AutofacResolver.GetLifeTimeScope(autofacOverrides))
            {
                var memberDocumentService = lifetimeScope.Resolve<IVendorService>();

                result = memberDocumentService.GetAll().Result;

            }

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Vendors.Count, "The number Vendors returned should be 1");
            Assert.AreEqual(result.Vendors.First().Id, 1,
                "Wrong Id");
        }
    }
}
