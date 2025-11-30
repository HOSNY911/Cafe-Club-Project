create   Procedure SP_InsertErrorLog

@Msg nvarchar(Max),
@Loc nvarchar(Max)

as
begin

	INSERT INTO ErrorLogs (ErrorMessage, ErrorLocation) 
    VALUES (@Msg, @Loc)
END