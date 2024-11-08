using Proje_model.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje_model.Models.Abstract
{
    public abstract class BaseEntity
    {

        public int ID { get; set; }

        private DateTime _createdDate = DateTime.Now;

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        private Statu _statu = Statu.Active;

        public Statu Statu
        {
            get { return _statu; }
            set { _statu = value; }
        }


    }
}
