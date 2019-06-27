using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UWay.Skynet.Cloud.Data.Test.TestModels
{
    public class User
    {
       

        public string UserName { get; set; }
        public int Age { get; set; }

        public double Wage { get; set; }
        public bool IsBitch { get; set; }
        public DateTime Birthday { get; set; }
    }
}
