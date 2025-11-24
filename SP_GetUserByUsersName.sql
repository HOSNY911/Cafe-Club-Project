USE [CafeClub]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetUserByUsersName]    Script Date: 11/24/2025 10:56:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create Procedure [dbo].[SP_GetUserByUsersName]

@UserName nvarchar(20)

as
begin 

	select P.FullName,U.UserName,U.Permissions,U.IsActive,P.Phone,U.Createdby,U.UpdatedAt from Users as U
	Join
	People AS P on P.PersonID = U.PersonID
	where U.UserName = @UserName;
end
GO


