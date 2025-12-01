using CafeClub_DataBaseLayer;
using CafeClub_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClub_BusinessLayer
{
    public class clsClientsBS
    {

        public enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.Update;

        public int ClientID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public decimal AmountOwed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Updatedby { get; set; }
        public bool IsActive { get; set; } = true;
        public string NewPhone { get; set; }

        public clsClientsBS()
        {
            this.ClientID = -1;
            this.FullName = string.Empty;
            this.Phone = string.Empty;
            this.AmountOwed = -1;
            this.CreatedBy = string.Empty;
            this.UpdatedAt = null;
            this.CreatedAt = DateTime.Now;
            this.Updatedby = string.Empty;


            Mode = enMode.AddNew;
        }

        private clsClientsBS(int ClientID,string FullName, string Phone, decimal AmountOwed,string CreatedBy,DateTime UpdatedAt, DateTime CreatedAt,string Updatedby)
        {
            this.ClientID = ClientID;
            this.FullName = FullName;
            this.Phone = Phone;
            this.AmountOwed = AmountOwed;
            this.CreatedBy = CreatedBy;
            this.UpdatedAt = UpdatedAt;
            this.CreatedAt = CreatedAt;
            this.Updatedby = Updatedby;

            Mode = enMode.Update;
        }


        private bool AddNewClient()
        {
            this.ClientID = clsClientsDB.AddNewClient(this.FullName, this.Phone, this.AmountOwed,CurrentUser.UserID,this.IsActive);
            return (this.ClientID != -1);

        }

        public static DataTable GetAllClient(int PageNumber, int RowPerPage, ref int TotalCount)
        {
            return clsClientsDB.GetAllClients(PageNumber, RowPerPage, ref TotalCount);
        }

        public static clsClientsBS GetClientbyPhone(string Phone)
        {
            ClientDTO Client = clsClientsDB.GetClientbyPhone(Phone);
            return new clsClientsBS(Client.ClientID, Client.FullName, Client.Phone, Client.AmountOwed, Client.CreatedBy, Client.UpdatedAt, Client.CreatedAt, Client.Updatedby);
        }

        public static bool IsClientExistsbyPhoneNumber(string PhoneNumber)
        {
            return clsClientsDB.IsClientExistsbyPhoneNumber(PhoneNumber);
        }

        private bool UpdateClientByPhone()
        {

            return clsClientsDB.UpdateClientByPhone(this.FullName, this.NewPhone, this.Phone, CurrentUser.UserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewClient())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return UpdateClientByPhone();

            }
            return false;
        }


    }


}

