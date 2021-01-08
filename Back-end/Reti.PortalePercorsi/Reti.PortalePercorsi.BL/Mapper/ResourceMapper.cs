using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reti.PortalePercorsi.BL.Mapper
{
    public static class ResourceMapper
    {
        public static DTOResource GetDTOResources(Resource entityResource)
        {
            DTOResource dtoResource = new DTOResource()
            {
                Email = entityResource.Email,
                Id = entityResource.Id,
                LastName = entityResource.LastName,
                Name = entityResource.Name,
                Username = entityResource.Username
            };

            return dtoResource;
        }

        public static Resource GetEntityResource(DTOResource dtoResource)
        {
            Resource entityResource = new Resource()
            {
                Email = dtoResource.Email,
                Id = dtoResource.Id,
                LastName = dtoResource.LastName,
                Name = dtoResource.Name,
                Username = dtoResource.Username
            };

            return entityResource;
        }
    }
}
