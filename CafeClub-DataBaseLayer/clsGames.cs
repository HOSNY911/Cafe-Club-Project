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
    public static class clsGames
    {

        public static bool AddNewGame(string GameName, decimal PricePerHour,int Createby)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddNewGame", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GameName", GameName);
                    cmd.Parameters.AddWithValue("@PricePerHour", PricePerHour);
                    cmd.Parameters.AddWithValue("@Createby", Createby);


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

        public static DataTable GetAllGames()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllGames", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                   

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

                     
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);


                    }
                }
            }
            return dt;
        }


        public static bool UpdateGamebyGameID(int GameID, string GameName, string PricePerHour, int Updatedby)
        {
            int IsUpdated = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateGamebyGameID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GameID", GameID);

                    if (string.IsNullOrEmpty(GameName))
                        cmd.Parameters.AddWithValue("@GameName", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@GameName", GameName);

                    cmd.Parameters.AddWithValue("@PricePerHour", PricePerHour);
                    cmd.Parameters.AddWithValue("@Updatedby", Updatedby);


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


        public static bool DeleteGamebyGameID(int GameID)
        {
            int IsUpdated = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteGamebyGameID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GameID", GameID);


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












    }
}
