using System;
using System.Collections.Generic;
using System.Text;

namespace UWay.Skynet.Cloud.Nom.Entity
{

    public class EnumType
    {
        public long Id { get; set; }

        public string NameEn { get; set; }

        public string  NameCn { get; set; }

        public string TypeComment { get; set; }

        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public string Editor { get; set; }

        public string EditDate { get; set; }
    }
}
