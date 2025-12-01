CREATE Procedure [dbo].[SP_UpdateClientByPhone]
@NewPhone nvarchar(20),
@FullName nvarchar(50),
@OldPhone nvarchar(20),
@Updatedby int
AS
BEGIN
    SET NOCOUNT ON; 
    BEGIN TRANSACTION
    BEGIN TRY

        
        IF NOT EXISTS(SELECT 1 FROM People WHERE Phone = @OldPhone)
        BEGIN
            THROW 50001, 'العميل الذي تحاول تحديث بياناته غير موجود', 5;
        END

        
        IF (@NewPhone IS NOT NULL AND @NewPhone <> '' AND @NewPhone <> @OldPhone)
        BEGIN
            IF EXISTS (SELECT 1 FROM People WHERE Phone = @NewPhone)
            BEGIN
                THROW 50002, 'رقم الهاتف الجديد مستخدم بالفعل لعميل آخر', 1;
            END
        END

        DECLARE @PersonID int;

        
        SELECT @PersonID = Clients.PersonID 
        FROM Clients
        JOIN People ON People.PersonID = Clients.PersonID
        WHERE People.Phone = @OldPhone;

		IF @PersonID IS NULL
            BEGIN
                THROW 50003, 'هذا الرقم لا ينتمي لعميل مسجل (قد يكون موظفاً أو مستخدماً)', 1;
            END

        
        UPDATE People 
        SET Phone = CASE WHEN @NewPhone IS NULL OR @NewPhone = '' THEN @OldPhone ELSE @NewPhone END,
            FullName = @FullName 
        WHERE PersonID = @PersonID;

        
        UPDATE Clients 
        SET Updatedby = @Updatedby, 
            UpdateAt = GETDATE() 
        WHERE PersonID = @PersonID;

        COMMIT TRANSACTION
        RETURN 1;

    END TRY
    BEGIN CATCH
        IF (@@TRANCOUNT > 0)
        BEGIN
            ROLLBACK TRANSACTION
        END;
        THROW; 
    END CATCH
END