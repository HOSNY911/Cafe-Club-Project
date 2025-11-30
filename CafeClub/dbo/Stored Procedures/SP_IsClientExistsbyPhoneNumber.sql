CREATE   Procedure SP_IsClientExistsbyPhoneNumber
@PhoneNumber nvarchar(20)

AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 
        FROM Clients C
        JOIN People P ON P.PersonID = C.PersonID
        WHERE P.Phone = @PhoneNumber
    )
    BEGIN
        RETURN 1;
    END

    RETURN 0;
END