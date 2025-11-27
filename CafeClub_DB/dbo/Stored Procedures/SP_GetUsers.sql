
CREATE procedure [dbo].[SP_GetUsers]
@PageNumber int,
@RowPerPage int,
@TotalCount int output

as
begin

	select @TotalCount = count(*) from Users;

	select P.FullName,U.UserName,U.Password,U.Permissions,U.IsActive,P.Phone,U.Createdby,U.UpdatedAt from Users as U
	Join
	People AS P on P.PersonID = U.PersonID

	ORDER BY U.UserID

	OFFSET (@PageNumber - 1) * @RowPerPage ROWS
    FETCH NEXT @RowPerPage ROWS ONLY;

end
		
