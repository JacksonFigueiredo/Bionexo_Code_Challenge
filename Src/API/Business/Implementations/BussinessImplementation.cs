using System.Collections.Generic;
using API.Business.Interfaces;
using API.Model;
using API.Repository.Interfaces;

namespace API.Business
{
    // This class where made to make some things easier when manipulating information, validation and possibly new methods, we don't need to access directly the repository
    // Acessing through an bussiness layer we can make validations, and other thins without touch model and repository layers, this way, will be a little bit clean.
    // when using generic repository, as we are using.

    public class BussinessImplementation : IBussinessLayer
    {
        private readonly IGenericRepository<Capture> _GenericRepositoryLayer;

        public BussinessImplementation(IGenericRepository<Capture> repository)
        {
            _GenericRepositoryLayer = repository;
        }

        public Capture Create(Capture computer)
        {
            return _GenericRepositoryLayer.Create(computer);
        }

        public List<Capture> FindAll()
        {
            return _GenericRepositoryLayer.FindAll();
        }

        public Capture FindById(long id)
        {
            return _GenericRepositoryLayer.FindById(id);
        }
    }
}
