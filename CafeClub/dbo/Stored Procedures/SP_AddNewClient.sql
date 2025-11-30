CREATE Procedure [dbo].[SP_AddNewClient]
@Amount decimal(10,2),
@Phone nvarchar(20),
@FullName nvarchar(50),
@Createdby int,
@ClientID int output,
@IsActive bit = 1
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
        BEGIN TRY
            DECLARE @PersonID int;

          
            SELECT @PersonID = PersonID FROM People WHERE Phone = @Phone;

           
            IF @PersonID IS NOT NULL
            BEGIN
               
                IF EXISTS(SELECT 1 FROM Clients WHERE PersonID = @PersonID)
                BEGIN
                    THROW 50001, 'العميل موجود بالفعل', 2;
                END
               
            END
            ELSE
            BEGIN
               
                INSERT INTO People (Phone, FullName) VALUES (@Phone, @FullName);
                SET @PersonID = SCOPE_IDENTITY();
            END

           
            
            INSERT INTO Clients (PersonID, Createdby, IsActive) 
            VALUES (@PersonID, @Createdby, @IsActive);

            SET @ClientID = SCOPE_IDENTITY();

            INSERT INTO Debts (ClientID, AmountOwed, Createdby) 
            VALUES (@ClientID, @Amount, @Createdby);

            COMMIT TRANSACTION;
            
            RETURN 1;

        END TRY
        BEGIN CATCH
            IF(@@TRANCOUNT > 0)
            BEGIN
                ROLLBACK TRANSACTION;
            END;
            THROW;
        END CATCH
END