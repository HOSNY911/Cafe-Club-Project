using CafeClub_BusinessLayer;
using CafeClub_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeClub_ConsoleTest
{
    internal class Program
    {


        //Users Functions Test

        static void AddNewUser()
        {
            clsUsersBS User = new clsUsersBS();


            User.UserName = "Admin";
            User.Password = "12345";
            User.Permissions = -1;
            User.CreatedBy = string.Empty;
            User.Phone = "01097476025";
            User.FullName = "Hosny Ayman";
            User.IsActive = true;
           
            if(User.Save())
            {
                Console.WriteLine("User Add Successfully");
            }
            else
            {
                Console.WriteLine("User Add Faild");
            }
        }

        public static void GetAllUsers()
        {
            int size = 0;
            DataTable dt = clsUsersBS.GetAllUsers(1, 10, ref size);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["UserID"]);
                Console.WriteLine(dr["FullName"]);
                Console.WriteLine(dr["UserName"]);
                Console.WriteLine(dr["Permissions"]);
                Console.WriteLine(dr["IsActive"]);
                Console.WriteLine(dr["Phone"]);
                Console.WriteLine(dr["UserName"]);
                Console.WriteLine(dr["Updatedby"]);
                Console.WriteLine(size);
                Console.WriteLine("\n\n");
            }
        }

        public static void GetUserByUsersName()
        {
            clsUsersBS User = clsUsersBS.GetUserByUsersName("Admin");

            Console.WriteLine(User.UserID);
            Console.WriteLine(User.FullName);
            Console.WriteLine(User.UserName);
            Console.WriteLine(User.Permissions);
            Console.WriteLine(User.IsActive);
            Console.WriteLine(User.Phone);
            Console.WriteLine(User.CreatedAt);
            Console.WriteLine(User.CreatedBy);
            Console.WriteLine(User.UpdatedAt);
            Console.WriteLine(User.Updatedby);

        }

        public static void IsUserExistsbyUserName()
        {
            if(clsUsersBS.IsUserExistsbyUserName("Admin"))
            {
                Console.WriteLine("Yes");
            }
            else
                Console.WriteLine("No");

        }

        public static void UpdateUserByUserName()
        {
            clsUsersBS User = clsUsersBS.GetUserByUsersName("Admin");
           
            User.Phone = "01112886137";
            User.Updatedby = CurrentUser.UserName;
            if (User.Save())
            {
                Console.WriteLine("User Updated Successfully");
            }
            else
            {
                Console.WriteLine("User Updated Faild");
            }
        }



        //Clients Functions Test

        static void AddNCleint()
        {
            clsClientsBS Client = new clsClientsBS();


            Client.FullName = "Mohammed Salah";
            Client.Phone = "0100002345";
            Client.AmountOwed = 10;
            

            if (Client.Save())
            {
                Console.WriteLine("Client Add Successfully");
            }
            else
            {
                Console.WriteLine("Client Add Faild");
            }
        }

        public static void GetAllClients()
        {
            int size = 0;
            DataTable dt = clsClientsBS.GetAllClient(1, 10, ref size);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["ClientID"]);
                Console.WriteLine(dr["FullName"]);
                Console.WriteLine(dr["Phone"]);
                Console.WriteLine(dr["AmountOwed"]);
                Console.WriteLine(dr["Createdby"]);
                Console.WriteLine(dr["UpdateAt"]);
                Console.WriteLine(dr["Updatedby"]);
                Console.WriteLine(size);
                Console.WriteLine("\n\n");
            }
        }

        public static void GetClientbyPhone()
        {
            clsClientsBS Client = clsClientsBS.GetClientbyPhone("0100002345");

            Console.WriteLine(Client.ClientID);
            Console.WriteLine(Client.FullName);
            Console.WriteLine(Client.Phone);
            Console.WriteLine(Client.AmountOwed);
            Console.WriteLine(Client.CreatedBy);
            Console.WriteLine(Client.Updatedby);
            

        }

        public static void IsClientExistsbyPhoneNumber()
        {
            if (clsClientsBS.IsClientExistsbyPhoneNumber("0100002345"))
            {
                Console.WriteLine("Yes");
            }
            else
                Console.WriteLine("No");

        }

        public static void UpdateClientByPhone()
        {
            clsClientsBS Client = clsClientsBS.GetClientbyPhone("012222234567") ;

             Client.NewPhone = "01222223456";
           // Client.FullName = "Mohammed Salahh";
            if (Client.Save())
            {
                Console.WriteLine("Cleint Updated Successfully");
            }
            else
            {
                Console.WriteLine("Cleint Updated Faild");
            }
        }

        //Debits Functions Test
        public static void AddNewDebit()
        {
            if(clsDebitsBS.AddNewDebit(10, "01222223456"))
            {

                Console.WriteLine("Debit Added Successfully");
            }
            else
            {
                Console.WriteLine("Debit Added Faild");
            }
        }


        //Payments Functions Test

        static void AddPayment()
        {


            if (clsPaymentsBS.AddPayment(2, "01222223456"))
            {
                Console.WriteLine("Payment Aded Successfully");
            }
            else
            {
                Console.WriteLine("Payment Aded Faild");
            }
        }

        public static void GetAllPaymentsbyPhoneNumber()
        {
            int size = 0;
            DataTable dt = clsPaymentsBS.GetAllPaymentsbyPhoneNumber(1, 10, ref size, "01222223456");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["FullName"]);
                Console.WriteLine(dr["Phone"]);
                Console.WriteLine(dr["Amount"]);
                Console.WriteLine(dr["CreatedAt"]);
                Console.WriteLine(dr["Createdby"]);
                Console.WriteLine(size);
                Console.WriteLine("\n\n");
            }
        }

        static void Main(string[] args)
        {

            //Users Functions Test

            //AddNewUser();
            //GetAllUsers();
            //GetUserByUsersName();
            //IsUserExistsbyUserName();
            //UpdateUserByUserName();



            //Clients Functions Test

            //AddNCleint();
            //GetAllClients();
            //GetClientbyPhone();
            //IsClientExistsbyPhoneNumber();
            //UpdateClientByPhone();



            //Debits Functions Test

            //AddNewDebit();



            //Payments Functions Test
            //AddPayment();
            //GetAllPaymentsbyPhoneNumber();
        }
    }
}
