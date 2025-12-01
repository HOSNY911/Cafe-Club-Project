using CafeClub_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClub_DataBaseLayer
{
    public static class clsDebitsDB
    {

        public static bool AddNewDebit(decimal AmountOwed, string PhoneNumber)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddDebit", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@AmountOwed", AmountOwed);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    

                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    try
                    {

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int Result = (int)cmd.Parameters["@ReturnValue"].Value;
                        return Result == 1;
                        

                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);

                        Console.WriteLine("عفواً حدث خطأ: " + ex.Message);
                        return false;
                    }

                }

            }
        }


    }
}
