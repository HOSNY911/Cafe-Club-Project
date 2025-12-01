



CREATE procedure [dbo].[SP_GetUsers]
@PageNumber int,
@RowPerPage int,
@TotalCount int output

as
begin

	select @TotalCount = count(*) from Users;

	select U.UserID,P.FullName,U.UserName,U.Permissions,U.IsActive,P.Phone,Createdby.UserName as Createdby,Updatedby.UserName as Updatedby from Users as U
	Join
	People AS P on P.PersonID = U.PersonID
	left join
	Users as Createdby on Createdby.UserID = U.Createdby
	left join
	Users as Updatedby on Updatedby.UserID = U.Updatedby
	where U.IsActive=1

	ORDER BY U.UserID

	OFFSET (@PageNumber - 1) * @RowPerPage ROWS
    FETCH NEXT @RowPerPage ROWS ONLY;

end