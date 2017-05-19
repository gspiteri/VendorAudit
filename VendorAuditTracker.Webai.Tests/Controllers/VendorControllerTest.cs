using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendorAuditTracker.Webapi.Controllers;
using VendorAuditTracker.Webapi.Interfaces;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Tests.Controllers
{
    [TestClass]
    public class VendorControllerTest
    {
        private IVendorAuditDbContext _vendorAuditDbContext;
        private IDbContextFactory _mockedDbContextFactory;

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
    }
}
