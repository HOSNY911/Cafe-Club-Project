using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.IO;

namespace CafeClub_Shared
{

    public static class clsDataAccessSettings
    {

        public static string ConnectionString()
        {
            string Connection = ConfigurationManager.ConnectionStrings["CafeClubDbConnection"].ConnectionString;

            return Connection;
        }

    }


    public static class Logger
    {
        public static void LogError(Exception ex)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog.txt";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertErrorLog", conn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Msg", ex.Message);
                    cmd.Parameters.AddWithValue("@Loc", ex.StackTrace);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                string errorDetails = $"Date: {DateTime.Now} \nError: {ex.Message} \nLocation: {ex.StackTrace} \n-----------------------\n";
                File.AppendAllText(FilePath, errorDetails);
            }
           
        }
    }

    public  class UserDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }
        public string CreatedBy { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Updatedby { get; set; }
    }




























}
