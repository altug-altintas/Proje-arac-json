using Proje_Dal.Context;
using Proje_Dal.Repositories.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proje_Dal.Repositories.Concrete
{
    public class IslemRepo : BaseRepo<islem>, IIslemRepo
    {
        public IslemRepo(ProjectContext context) : base(context)
        {
        }

        public void Create2(islem entity)
        {
           // entity.No = GetNextFisNo();
            _context.Islemler.Add(entity);
            _context.SaveChanges();
        }




        public string GetNextFisNo()
        {
            var islemler = _context.Islemler.ToList();           
            var maxFisNo = islemler
                .Select(x => int.TryParse(x.No, out int result) ? result : 0)
                .DefaultIfEmpty(0)
                .Max();

            return (maxFisNo + 1).ToString();
        }
    }
}
