using System.Collections.Generic;
using System.Data.Entity.Migrations;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VendorAuditDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VendorAuditDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Vendors.AddOrUpdate(
                v => v.Name,
                new Vendor
                {
                    Name = "NGN",
                    PrimaryContact = "Philippe",
                    Projects = new List<Project>(),
                    SecondaryContact = "Andrew Morrison"
                }
            );

            context.Vendors.AddOrUpdate(
               v => v.Name,
               new Vendor
               {
                   Name = "LTX",
                   PrimaryContact = "Lou",
                   Projects = new List<Project>(),
                   SecondaryContact = string.Empty
               }
           );

            context.Vendors.AddOrUpdate(
               v => v.Name,
               new Vendor
               {
                   Name = "IBM",
                   PrimaryContact = "Someone",
                   Projects = new List<Project>(),
                   SecondaryContact = string.Empty
               }
           );
        }
    }
}