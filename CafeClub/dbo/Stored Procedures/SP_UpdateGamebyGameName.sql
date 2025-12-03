
CREATE   procedure [dbo].[SP_UpdateGamebyGameName]
@GameID int,
@GameName nvarchar(50),
@PricePerHour decimal(10,2),
@Updatedby int
as
begin
	Begin Try
		if exists(select 1 from Games where GameName = @GameName AND GameID != @GameID)
			Begin
				Throw 50001,'الاسم موجود بالفعل',11;
			End

		Update Games set  GameName = @GameName,PricePerHour=@PricePerHour,UpdatedAt=getdate(),Updatedby=@Updatedby
		where GameName = @GameName;

		return 1;
	End Try
Begin Catch
	Throw;
End Catch
End