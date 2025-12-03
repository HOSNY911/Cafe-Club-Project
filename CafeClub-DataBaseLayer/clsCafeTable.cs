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
    public static class clsCafeTable
    {

        public static int AddCafeTable(string TableName, int GameID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddCafeTable", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TableName", TableName);
                    cmd.Parameters.AddWithValue("@GameID", GameID);
                   


                    cmd.Parameters.Add("@TableID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    try
                    {

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int Result = (int)cmd.Parameters["@ReturnValue"].Value;
                        if (Result == 1)
                        {
                            return (int)cmd.Parameters["@TableID"].Value;
                        }
                        else
                        {
                            return -1;
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);

                        Console.WriteLine("عفواً حدث خطأ: " + ex.Message);
                        return -1;
                    }

                }

            }
        }


        public static bool DeleteCafeTable(int TableID)
        {
            int IsUpdated = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteCafeTable", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TableID", TableID);


                    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int)).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        conn.Open();

                        cmd.ExecuteNonQuery();

                        IsUpdated = (int)cmd.Parameters["@ReturnValue"].Value;

                        return IsUpdated == 1;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        return false;
                    }
                }
            }
        }



        // SP_GetAllCafeTables هتهاها عن طريق ال CafeTableDTO لانك هنا برتجع table فاهم 














    }



}
