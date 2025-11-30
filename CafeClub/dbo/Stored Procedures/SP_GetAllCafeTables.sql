create   Procedure SP_GetAllCafeTables

as
begin
	select CT.TableID,CT.TableName,G.GameName,G.PricePerHour,CT.IsActive from CafeTables AS CT
	join
	Games as G on G.GameID = CT.GameID
	Where CT.IsActive = 1

End