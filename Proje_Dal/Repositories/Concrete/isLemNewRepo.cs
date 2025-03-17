using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Abstract;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Dal.Repositories.Concrete
{
    public class isLemNewRepo : BaseRepo<islem>, IisLemNewRepo
    {
        public isLemNewRepo(ProjectContext context) : base(context)
        {
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

        public async Task<bool> islemSaveWithDetails(islem islem, List<islemD> islemDList)
        {
            try
            {
                _context.Islemler.Add(islem);
                _context.SaveChanges();

                foreach (var item in islemDList)
                {
                    item.IslemId = islem.ID;
                }

                _context.IslemDetaylar.AddRange(islemDList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //throw;
                return false;
            }
            return true;

        }
     
    }
}
