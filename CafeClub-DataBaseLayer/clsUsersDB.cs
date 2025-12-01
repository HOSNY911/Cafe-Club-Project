using CafeClub_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CafeClub_DataBaseLayer
{
    public static class clsUsersDB
    {

        public static int AddNewUser(string UserName,string Password,int Permissions, string Createdby,string Phone,string FullName,bool IsActive= true)
        {

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddNewUserByUserName", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Permissions", Permissions);
                    cmd.Parameters.AddWithValue("@Createdby", Createdby);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@IsActive", IsActive);

                    cmd.Parameters.Add("@UserID",SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ReturnValue",SqlDbType.Int).Direction= ParameterDirection.ReturnValue;
                  
                    try
                    {
                       
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int Result = (int)cmd.Parameters ["@ReturnValue"].Value;
                        if (Result == 1)
                        {
                            return (int)cmd.Parameters["@UserID"].Value;
                        }
                        else
                        {
                            return -1;
                        }

                    }
                    catch(Exception ex)
                    {
                        Logger.LogError(ex);
                        return -1;
                    }
                   
                }

            }

        }

        public static DataTable GetAllUsers(int PageNumber,int RowPerPage, ref int TotalCount)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using(SqlCommand cmd = new SqlCommand("SP_GetUsers",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                    cmd.Parameters.AddWithValue("@RowPerPage", RowPerPage);
                    

                    cmd.Parameters.Add(new SqlParameter("@TotalCount", SqlDbType.Int)).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader Reader=cmd.ExecuteReader())
                        {
                            if(Reader.HasRows)
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
                    catch(Exception ex)
                    {
                        Logger.LogError(ex);
                    }
                }
            }
            return dt;
        }

        public static UserDTO GetUserByUsersName(string UserName)
        {
            
            UserDTO User = null;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetUserByUsersName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        conn.Open();
                        using(SqlDataReader Reader= cmd.ExecuteReader())
                        {
                            if(Reader.Read())
                            {
                                User = new UserDTO();
                                User.UserID = (int)Reader["UserID"];
                                User.FullName = (string)Reader["FullName"];
                                User.UserName = (string)Reader["UserName"];
                                User.Permissions = (int)Reader["Permissions"];
                                User.IsActive = (bool)Reader["IsActive"];
                                User.Phone = (string)Reader["Phone"];
                                User.CreatedAt = (DateTime)Reader["CreatedAt"];
                                User.CreatedBy = Reader["Createdby"] != DBNull.Value ? (string)Reader["Createdby"] : null;
                                User.UpdatedAt = Reader["UpdatedAt"] != DBNull.Value ? (DateTime?)Reader["UpdatedAt"] : null;
                                User.Updatedby = Reader["Updatedby"] != DBNull.Value ? (string)Reader["Updatedby"] : null;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.LogError(ex);
                        return null;
                    }
                }
            }

            return User;
        }

        public static bool IsUserExistsbyUserName(string UserName)
        {
            int IsFound = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_IsUserExistsbyUserName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int)).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        conn.Open();

                        cmd.ExecuteNonQuery();

                        IsFound = (int)cmd.Parameters["@ReturnValue"].Value;

                        return IsFound == 1;
                    }
                    catch(Exception ex)
                    {
                        Logger.LogError(ex);
                        return false;
                    }
                }
            }
        }

        public static bool UpdateUserByUserName(string UserName,string Password,int Permissions,bool IsActive,string Phone,string FullName,string Updatedby)
        {
            int IsUpdated = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateUserByUserName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    if (string.IsNullOrEmpty(Password))
                        cmd.Parameters.AddWithValue("@Password", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Password", Password);

                    cmd.Parameters.AddWithValue("@Permissions", Permissions);
                    cmd.Parameters.AddWithValue("@IsActive", IsActive);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@FullName", FullName);

                    if (string.IsNullOrEmpty(Updatedby))
                        cmd.Parameters.AddWithValue("@Updatedby", DBNull.Value);
                    else
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


































































































    }
}
 