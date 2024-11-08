using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_Dal.Repositories.Interfaces.Concrete
{
    public interface IIslemRepo : IBaseRepo<islem>
    {

        void Create2(islem entity);
        string GetNextFisNo();

    }
}
