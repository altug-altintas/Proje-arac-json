using Proje_Dal.Context;
using Proje_Dal.Repositories.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_Dal.Repositories.Concrete
{
    public class FirmaSahisRepo : BaseRepo<FirmaSahis>, IFirmaSahisRepo
    {
        public FirmaSahisRepo(ProjectContext context) : base(context)
        {
        }
    }
}
