
CREATE Procedure [dbo].[SP_GetUserByUsersName]

@UserName nvarchar(20)

as
begin 

	select P.FullName,U.UserName,U.Permissions,U.IsActive,P.Phone,U.Createdby,U.UpdatedAt,U.Updatedby from Users as U
	Join
	People AS P on P.PersonID = U.PersonID
	where U.UserName = @UserName;
end
