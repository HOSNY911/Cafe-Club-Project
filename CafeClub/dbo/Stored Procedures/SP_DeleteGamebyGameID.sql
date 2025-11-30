create   Procedure SP_DeleteGamebyGameID

@GameID int
as
begin
	Begin Try

		if not exists(select 1 from Games where GameID = @GameID)
			Begin
				Throw 50001,'اللعبه غير موجوده',12;
			End

			Update Games set IsActive=0 where GameID = @GameID;

			return 1;
	End Try
Begin Catch
	Throw
End Catch
End