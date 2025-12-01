using CafeClub_DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClub_BusinessLayer
{
    public static class clsDebitsBS
    {

        public static bool AddNewDebit(decimal AmountOwed, string PhoneNumber)
        {
            return clsDebitsDB.AddNewDebit(AmountOwed, PhoneNumber);
        }


    }
}
