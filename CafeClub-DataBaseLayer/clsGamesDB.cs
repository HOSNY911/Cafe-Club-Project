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
    public static class clsGamesDB
    {

        public static int AddNewGame(string GameName, decimal PricePerHour,int Createby)
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
                    cmd.Parameters.Add("@GameID", SqlDbType.Int).Direction = ParameterDirection.Output;


                    try
                    {

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int Result = (int)cmd.Parameters["@ReturnValue"].Value;

                        if(Result == 1)
                        {
                            return (int)cmd.Parameters["@GameID"].Value;
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


        public static bool UpdateGamebyGameID(int GameID, string GameName, decimal PricePerHour, int Updatedby)
        {
            int IsUpdated = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateGamebyGameName", conn))
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


        public static GameDTO GetGameByGameName(string GameName)
        {

            GameDTO Game = null;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetGameByGameName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GameName", GameName);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                Game = new GameDTO();
                                Game.GameID = (int)Reader["GameID"];
                                Game.GameName = (string)Reader["GameName"];
                                Game.PricePerHour = (decimal)Reader["PricePerHour"];
                                Game.CreatedAt = (DateTime)Reader["CreatedAt"];
                                Game.CreatedBy = Reader["Createdby"] != DBNull.Value ? (string)Reader["Createdby"] : null;
                                Game.UpdatedAt = Reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Reader["UpdatedAt"] : null;
                                Game.Updatedby = Reader["Updatedby"] != DBNull.Value ? (string)Reader["Updatedby"] : null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        return null;
                    }
                }
            }

            return Game;
        }









    }
}
