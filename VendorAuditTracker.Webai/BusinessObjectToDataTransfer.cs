
using System;
using System.Collections.Generic;
using VendorAuditTracker.Webapi.DataTransferObjects;
using VendorAuditTracker.Webapi.Models;

namespace VendorAuditTracker.Webapi
{
    public static class Mapper
    {

        public static VendorDto BusinessObjectToDto(Vendor businessObject)
        {
            if (businessObject == null || businessObject.Projects == null)
                throw new NullReferenceException();

            var projectDto = new List<ProjectDto>();
            businessObject.Projects.ForEach(i => projectDto.Add(BusinessObjectToDto(i)));

            return new VendorDto
            {
                Id = businessObject.Id,
                Name = businessObject.Name,
                PrimaryContact = businessObject.PrimaryContact,
                Projects = projectDto,
                SecondaryContact = businessObject.SecondaryContact
            };
        }

        public static ProjectDto BusinessObjectToDto(Project businessObject)
        {
            if (businessObject == null || businessObject.CodeDrops == null)
                throw new NullReferenceException();

            var codeDropDtoList = new List<CodeDropDto>();
            businessObject.CodeDrops.ForEach(i => codeDropDtoList.Add(BusinessObjectToDto(i)));

            var vendorDtoList = new List<VendorDto>();
            businessObject.Vendors.ForEach(i => vendorDtoList.Add(BusinessObjectToDto(i)));

            return new ProjectDto
            {
                Id = businessObject.Id,
                Name = businessObject.Name,
                Code = businessObject.Code,
                CodeDrops = codeDropDtoList,
                ExternalSystemId = businessObject.ExternalSystemId,
                InternalSystemId = businessObject.InternalSystemId,
                SoftwareRelease = BusinessObjectToDto(businessObject.SoftwareRelease),
                Vendors = vendorDtoList
            };
        }

        public static CodeDropDto BusinessObjectToDto(CodeDrop businessObject)
        {
            if (businessObject == null)
                throw new NullReferenceException();

            return new CodeDropDto
            {
                Id = businessObject.Id,
                ActualDate = businessObject.ActualDate,
                TargetedDate = businessObject.TargetedDate,
                VendorName = businessObject.VendorName
            };
        }

        public static SoftwareReleaseDto BusinessObjectToDto(SoftwareRelease businessObject)
        {
            if (businessObject == null)
                throw new NullReferenceException();

            var projectDto = new List<ProjectDto>();
            businessObject.Projects.ForEach(i => projectDto.Add(BusinessObjectToDto(i)));

            return new SoftwareReleaseDto
            {
                Id = businessObject.Id,
                ActualDate = businessObject.ActualDate,
                TargetedDate = businessObject.TargetedDate,
                Projects = projectDto,
                ReleaseName = businessObject.ReleaseName,
                Version = businessObject.Version
            };
        }
    }
}