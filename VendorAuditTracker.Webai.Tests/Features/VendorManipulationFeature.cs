﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using VendorAuditTracker.Webapi.Controllers;
using VendorAuditTracker.Webapi.DataTransferObjects.Response;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;
using VendorAuditTracker.Webapi.Tests.Utilities;

namespace VendorAuditTracker.Webapi.Tests.Features
{

    [TestClass]
    public class VendorManipulationFeature
    {
        private IDbContextFactory _mockDbContextFactory;
        private IVendorAuditDbContext _mockDbContext;
        private Action<ContainerBuilder> _environment;

        [TestInitialize]
        public void Initialise()
        {
            _mockDbContextFactory = Substitute.For<IDbContextFactory>();
            _mockDbContext = Substitute.For<IVendorAuditDbContext>();
            _environment = SetupEnvironment();
        }

        public Action<ContainerBuilder> SetupEnvironment()
        {
            _mockDbContext.Vendors = NSubstituteUtils.CreateMockDbSet(new[]
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

            _mockDbContextFactory.DbContext.Returns(_mockDbContext);

            //Action
            //Register all mocked objects
            Action<ContainerBuilder> autofacOverrides = builder =>
            {
                builder.RegisterInstance(_mockDbContextFactory).As<IDbContextFactory>();
            };

            return autofacOverrides;
        }

        /// <summary>
        /// @scenario: Successfully retrieved all Vendor details
        /// </summary>
        /// <remarks>
        /// GIVEN A Vendors details are stored within the system
        /// WHEN A business application requests to retrieve all vendor details
        /// THEN a list of vendors should be recieved by the caller.
        /// </remarks>
        [TestMethod, TestCategory("1.0")]
        public void GetAllVendorDetails_Test()
        {
            using (var lifetimeScope = AutofacResolver.GetLifeTimeScope(_environment))
            {
                //Retreive service from scope
                var vendorController = lifetimeScope.Resolve<VendorController>();
                var result = vendorController.Get().Result;
                var actualResponse = result as NegotiatedContentResult<VendorResponse>;

                // Assertions
                Assert.IsInstanceOfType(actualResponse.Content, typeof(VendorResponse));
                Assert.IsNotNull(actualResponse);
                Assert.IsNotNull(actualResponse.Content.Vendors);
                Assert.AreEqual(1, actualResponse.Content.Vendors.Count, "The number Vendors returned should be 1");
                Assert.IsTrue(actualResponse.Content.Vendors[0].Projects[0].Code == "PP123456", "Invalid Project Code");
                Assert.AreEqual(actualResponse.Content.Vendors.First().Id, 1,
                    "Wrong Id");
                _mockDbContext.Received(1);
            }
        }

        /// <summary>
        /// @scenario: Create new vendor
        /// </summary>
        /// <remarks>
        /// GIVEN A Vendors details are stored within the system
        /// WHEN A business application requests to create a new vendor into the system
        /// THEN a succesful response should be returned to the application.
        /// </remarks>
        [TestMethod, TestCategory("1.0")]
        public void CreateNewVendor_Test()
        {

        }
    }
}
