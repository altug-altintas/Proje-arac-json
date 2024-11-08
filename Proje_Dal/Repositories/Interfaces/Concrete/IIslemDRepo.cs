using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_Dal.Repositories.Interfaces.Concrete
{
    public interface IIslemDRepo : IBaseRepo<islemD>
    {
        List<islemD> GetisLemDetWithID(int id);
        List<islemD> GetisLemDet();

    }
}
