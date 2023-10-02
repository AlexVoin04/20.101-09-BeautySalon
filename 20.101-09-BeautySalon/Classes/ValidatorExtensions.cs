using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _20._101_09_BeautySalon.Classes
{
    public static class ValidatorExtensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            //Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$");
            Regex regex = new Regex(@"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$");
            //Regex regex = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            return !regex.IsMatch(s);
        }

        public static bool IsValidPhone(this string s)
        {
            Regex regex = new Regex("[^0-9+() -]+");
            return !regex.IsMatch(s);
        }

        public static bool IsValidFIO(this string s)
        {
            Regex regex = new Regex("[^a-zа-яА-ЯA-Z -]+");
            return !regex.IsMatch(s);
        }

        public static bool IsValidColor(this string s)
        {
            Regex regex = new Regex("[^0-9A-Z]+");
            return !regex.IsMatch(s);
        }

        public static bool IsValidDiscount(this string s)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(s);
        }

        public static bool IsValidCost(this string s)
        {
            ////Regex regex = new Regex("^[0-9]+(\\.[0-9]{1,2})?$");
            //Regex regex = new Regex("[^0-9.]+");
            //return !regex.IsMatch(s);
            Regex regex = new Regex(@"^\d+(\.\d{0,2})?$");
            return regex.IsMatch(s);
        }

        public static bool IsValidTitle(this string s)
        {
            Regex regex = new Regex("[^a-zA-Zа-яА-Я0-9\\s]+");
            return !regex.IsMatch(s);
        }
    }
}
