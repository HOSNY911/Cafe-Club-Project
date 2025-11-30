create   Procedure SP_GetClientSessionsbyClientID

@ClientID int,
@RowPerPage int,
@PageNumber int,
@Total int Output

as
begin
	
set nocount on;
	select @Total = count(*) from Sessions where ClientID = @ClientID;

	select S.ClientID,P.FullName,P.Phone,S.StartTime,S.EndTime,S.TableID,S.TotalAmount,G.GameName,S.CreatedAt from Sessions as S
	join
	Clients as C on C.ClientID = S.ClientID
	join
	People as P on P.PersonID = C.PersonID
	join
	CafeTables as CT on CT.TableID = S.TableID
	join
	Games as G on G.GameID = CT.GameID
	where C.ClientID = @ClientID
	order by S.CreatedAt Desc

	Offset(@PageNumber-1)*@RowPerPage rows
	Fetch next @RowPerPage Row Only

end