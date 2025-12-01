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

    public static class clsPaymentsDB
    {
        public static bool AddPayment(decimal Amount, int Createdby,string PhoneNumber)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddPayment", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@Createdby", Createdby);
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

        public static DataTable GetAllPaymentsbyPhoneNumber(int PageNumber, int RowPerPage, ref int TotalCount,string PhoneNumber)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllPaymentsbyPhoneNumber", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                    cmd.Parameters.AddWithValue("@RowPerPage", RowPerPage);
                    cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);


                    cmd.Parameters.Add(new SqlParameter("@TotalCount", SqlDbType.Int)).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                dt.Load(Reader);
                            }
                        }

                        object totalCountVal = cmd.Parameters["@TotalCount"].Value;

                        if (totalCountVal != null && totalCountVal != DBNull.Value)
                        {
                            TotalCount = Convert.ToInt32(totalCountVal);
                        }
                        else
                        {
                            TotalCount = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                    }
                }
            }
            return dt;
        }




























    }
}
