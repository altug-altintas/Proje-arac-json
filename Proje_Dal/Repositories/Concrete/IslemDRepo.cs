using Proje_Dal.Context;
using Proje_Dal.Repositories.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proje_Dal.Repositories.Concrete
{
    public class IslemDRepo : BaseRepo<islemD>, IIslemDRepo
    {
        public IslemDRepo(ProjectContext context) : base(context)
        {
        }

        public List<islemD> GetisLemDet()
        {
            return _context.IslemDetaylar.ToList();
        }

        public List<islemD> GetisLemDetWithID(int id)
        {
          return _context.IslemDetaylar.Where(a=> a.IslemId==id).ToList();   
        }
    }
}
