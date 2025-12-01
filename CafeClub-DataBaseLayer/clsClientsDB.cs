using CafeClub_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CafeClub_DataBaseLayer
{
   
    public static class clsClientsDB
    {


        public static int AddNewClient(string FullName,string Phone,decimal Amount, int Createdby,bool IsActive=true)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddNewClient", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", FullName);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@Createdby", Createdby);
                    cmd.Parameters.AddWithValue("@IsActive", IsActive);


                    cmd.Parameters.Add("@ClientID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    try
                    {

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int Result = (int)cmd.Parameters["@ReturnValue"].Value;
                        if (Result == 1)
                        {
                            return (int)cmd.Parameters["@ClientID"].Value;
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


        public static DataTable GetAllClients(int PageNumber, int RowPerPage, ref int TotalCount)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetClients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                    cmd.Parameters.AddWithValue("@RowPerPage", RowPerPage);


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


        public static ClientDTO GetClientbyPhone(string Phone)
        {

            ClientDTO Client = null;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetClientbyPhone", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Phone", Phone);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                Client = new ClientDTO();
                                Client.ClientID = (int)Reader["ClientID"];
                                Client.FullName = (string)Reader["FullName"];
                                Client.Phone = (string)Reader["Phone"];
                                Client.AmountOwed = (decimal)Reader["AmountOwed"];
                                Client.CreatedBy = (string)Reader["Createdby"];
                                Client.Updatedby = Reader["Updatedby"] != DBNull.Value ? (string)Reader["Updatedby"] : null;
                               
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

            return Client;
        }


        public static bool IsClientExistsbyPhoneNumber(string Phone)
        {
            int IsFound = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_IsClientExistsbyPhoneNumber", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PhoneNumber", Phone);

                    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int)).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        conn.Open();

                        cmd.ExecuteNonQuery();

                        IsFound = (int)cmd.Parameters["@ReturnValue"].Value;

                        return IsFound == 1;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        return false;
                    }
                }
            }
        }

        public static DataTable GetClientSessionsbyClientID(int PageNumber, int RowPerPage, ref int TotalCount,int ClientID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetClientSessionsbyClientID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PageNumber", PageNumber);
                    cmd.Parameters.AddWithValue("@RowPerPage", RowPerPage);
                    cmd.Parameters.AddWithValue("@ClientID", ClientID);

                    cmd.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int)).Direction = ParameterDirection.Output;

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

                        object totalCountVal = cmd.Parameters["@Total"].Value;

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


        public static bool UpdateClientByPhone(string FullName, string NewPhone, string OldPhone, int Updatedby)
        {
            int IsUpdated = 0;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateClientByPhone", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", FullName);

                    if(string.IsNullOrEmpty(NewPhone))
                        cmd.Parameters.AddWithValue("@NewPhone", DBNull.Value);
                    else
                         cmd.Parameters.AddWithValue("@NewPhone", NewPhone);

                    cmd.Parameters.AddWithValue("@OldPhone", OldPhone);
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
