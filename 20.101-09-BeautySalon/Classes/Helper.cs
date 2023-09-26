using _20._101_09_BeautySalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20._101_09_BeautySalon.Classes
{
    internal class Helper
    {
        public static bool flag = false;
        private static Entities BeautySalonEntities;
        public static Entities GetContext()
        {
            if (BeautySalonEntities == null)
            {
                BeautySalonEntities = new Entities();
            }
            return BeautySalonEntities;
        }
    }
}
