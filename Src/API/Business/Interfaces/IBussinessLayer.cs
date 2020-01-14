using API.Model;
using System.Collections.Generic;

namespace API.Business.Interfaces
{
    public interface IBussinessLayer
    {
        // Methos to be used by implementation layer.
        Capture Create(Capture computer);
        List<Capture> FindAll();
        Capture FindById(long id);
    }
}
