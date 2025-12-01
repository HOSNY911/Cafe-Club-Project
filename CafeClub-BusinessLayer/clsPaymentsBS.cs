using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeClub_DataBaseLayer;
using CafeClub_Shared;

namespace CafeClub_BusinessLayer
{
    public static class clsPaymentsBS
    {

        public static bool AddPayment(decimal Amount, string PhoneNumber)
        {
            return clsPaymentsDB.AddPayment(Amount,CurrentUser.UserID, PhoneNumber);
        }

        public static DataTable GetAllPaymentsbyPhoneNumber(int PageNumber, int RowPerPage, ref int TotalCount, string PhoneNumber)
        {
            return clsPaymentsDB.GetAllPaymentsbyPhoneNumber(PageNumber, RowPerPage, ref TotalCount, PhoneNumber);
        }


    }
}
