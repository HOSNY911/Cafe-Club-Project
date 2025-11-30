
CREATE procedure [dbo].[SP_GetClients]

@PageNumber int,
@RowPerPage int,
@TotalCount int output
as
begin

	select @TotalCount = count(*) from Clients where IsActive = 1;

	select C.ClientID,P.FullName,P.Phone,D.AmountOwed,U.UserName as Createdby,C.UpdateAt,Updater.UserName as Updatedby from people as P
	join
	Clients as C on C.PersonID = P.PersonID
	join
	Users as U on U.UserID = C.Createdby
	left join
	Users as Updater on Updater.UserID =  C.Updatedby
	join
	Debts as D on D.ClientID = C.ClientID
	where C.IsActive = 1

	order by C.ClientID

	Offset(@PageNumber -1) * @RowPerPage rows
	Fetch Next @RowPerPage Rows Only ;

end