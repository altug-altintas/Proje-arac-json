using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Proje_Dal.Repositories.Interfaces.Concrete
{
    public interface IisLemNewRepo : IBaseRepo<islem>
    {
        Task<bool> islemSaveWithDetails(islem islem, List<islemD> islemDList);
        string GetNextFisNo();
    }
}
