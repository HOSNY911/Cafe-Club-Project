create   Procedure SP_GetAllGames

as
begin
	select G.GameID,G.GameName,G.PricePerHour,G.CreatedAt,Createby.UserName as Createby,Updatedby.UserName as Updatedby,G.UpdatedAt from Games as G
	join
	Users as Createby on Createby.UserID = G.Createby
	left join
	Users as Updatedby on Updatedby.UserID = G.Updatedby
	where G.IsActive = 1;
End