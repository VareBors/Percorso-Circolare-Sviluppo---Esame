using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.BL.Mapper;
using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Reti.PortalePercorsi.BL.Manager
{
    //MANAGER PER LA GESTIONE  DELLE RISORSE
    public class ResourcesManager
    {
        private readonly IUnitOfWork UnitOfWork;

        public ResourcesManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public List<DTOResource> GetAll()
        {
            List<DTOResource> ResourceList = new List<DTOResource>();

            foreach (Resource item in UnitOfWork.ResourceRepository.GetAll())
            {
                ResourceList.Add(ResourceMapper.GetDTOResources(item));
            }

            return ResourceList;
        } 
        
        public DTOResource GetById(int Id)
        {
            return ResourceMapper.GetDTOResources(UnitOfWork.ResourceRepository.GetByID(Id));
        }

        public int Add(DTOResource dtoResource)
        {
            Resource entityResource = ResourceMapper.GetEntityResource(dtoResource);

            entityResource.Id = UnitOfWork.GetResourceId();

            entityResource.Email = $"{entityResource.Name}.{entityResource.LastName}@reti.it";

            if (dtoResource.LastName.Replace(" ","").Length >= 5) 
            {
                entityResource.Username = dtoResource.LastName.Replace(" ", "").Substring(0, 5).ToLower();
            }
            else
            {
                entityResource.Username = dtoResource.LastName.Replace(" ", "").ToLower();
            }

            if(dtoResource.Name.Replace(" ", "").Length >= 2)
            {
                entityResource.Username += dtoResource.Name.Replace(" ", "").Substring(0, 2).ToLower();
            }
            else
            {
                entityResource.Username += dtoResource.Name.Replace(" ", "").ToLower();
            }

            int ResourceWithSameUsername = UnitOfWork.ResourceRepository.GetAll().Where(res => res.Username.Substring(0, res.Username.Length - 1) == entityResource.Username).Count();
            
            entityResource.Username += ResourceWithSameUsername+1;

            UnitOfWork.ResourceRepository.Add(entityResource);
            UnitOfWork.Commit();

            return entityResource.Id;
        }

        public void Dispose()
        {
            UnitOfWork.Rollback();
        }
    }
}
