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
    public class clsUsersBS
    {

        public enum enMode { Update=0,AddNew=1}
        private enMode Mode = enMode.Update;

        public int UserID { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }
        public string CreatedBy { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Updatedby { get; set; }

        public clsUsersBS()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.Permissions = 0;
            this.CreatedBy = string.Empty;
            this.Phone = string.Empty;
            this.FullName = string.Empty;
            this.IsActive = false;
            this.UpdatedAt = DateTime.MinValue;
            this.Updatedby = string.Empty;


            Mode = enMode.AddNew;
        }

        private clsUsersBS(string UserName,string Password,int Permissions,bool IsActive,string Createdby,string Phone,string FullName)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.Permissions = Permissions;
            this.IsActive = IsActive;
            this.CreatedBy = Createdby;
            this.Phone = Phone;
            this.FullName = FullName;

            Mode = enMode.Update;
        }


        private bool AddNewUser()
        {
            this.UserID = clsUsersDB.AddNewUser(this.UserName, this.Password, this.Permissions, this.CreatedBy, this.Phone, this.FullName, this.IsActive);
            return (this.UserID != -1);

        }

        public static DataTable GetAllUsers(int PageNumber, int RowPerPage, ref int TotalCount)
        {
            return clsUsersDB.GetAllUsers(PageNumber, RowPerPage, ref TotalCount);
        }

        public static UserDTO GetUserByUsersName(string UserName)
        {
            return clsUsersDB.GetUserByUsersName(UserName);
        }

        public static bool IsUserExistsbyUserName(string UserName)
        {
            return clsUsersDB.IsUserExistsbyUserName(UserName);
        }

        private bool UpdateUserByUserName()
        {
            return clsUsersDB.UpdateUserByUserName(this.UserName, this.Password, this.Permissions, this.IsActive, this.Phone, this.FullName,this.Updatedby);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                 case enMode.Update:
                    return UpdateUserByUserName();

            }
            return false;
        }


    }
}
