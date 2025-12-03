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
    public class clsGamesBS
    {

        public enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.Update;

        public int GameID { get; private set; }
        public string GameName { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Createby { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedAt { get; set; }
        

        public clsGamesBS()
        {
            this.GameID = -1;
            this.GameName = string.Empty;
            this.PricePerHour = -1;
            this.CreatedAt = DateTime.Now;
            this.Createby = string.Empty;
            this.UpdatedAt = null;


            Mode = enMode.AddNew;
        }

        private clsGamesBS(int GameID, string GameName,decimal PricePerHour, DateTime CreatedAt, string Createdby,string Updatedby, DateTime? UpdatedAt)
        {
            this.GameID = GameID;
            this.GameName = GameName;
            this.PricePerHour = PricePerHour;
            this.CreatedAt = CreatedAt;
            this.Createby = Createby;
            this.UpdatedAt = null;
            this.Updatedby = Updatedby;

            Mode = enMode.Update;
        }


        private bool AddNewGame()
        {
            this.GameID = clsGamesDB.AddNewGame(this.GameName, this.PricePerHour, CurrentUser.UserID);
            return (GameID != -1);

        }

        public static DataTable GetAllGames()
        {
            return clsGamesDB.GetAllGames();
        }

        public static bool DeleteGamebyGameID(int GameID)
        {
            return clsGamesDB.DeleteGamebyGameID(GameID);
        }

        public static clsGamesBS GetGameByGameName(string GameName)
        {
            GameDTO Game = clsGamesDB.GetGameByGameName(GameName);
            return new clsGamesBS(Game.GameID, Game.GameName, Game.PricePerHour, Game.CreatedAt, Game.CreatedBy, Game.Updatedby, Game.UpdatedAt);
        }

        private bool UpdateGamebyGameID()
        {

            return clsGamesDB.UpdateGamebyGameID(this.GameID, this.GameName, this.PricePerHour,CurrentUser.UserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (AddNewGame())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return UpdateGamebyGameID();

            }
            return false;
        }



    }
}
