CREATE TABLE [dbo].[Users] (
    [UserID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (20)  NOT NULL,
    [Password]    NVARCHAR (150) NOT NULL,
    [Permissions] INT            NOT NULL,
    [IsActive]    BIT            CONSTRAINT [DF__Users__IsActive__3D5E1FD2] DEFAULT ((1)) NOT NULL,
    [PersonID]    INT            NOT NULL,
    [Createdby]   INT            NULL,
    [CreatedAt]   DATE           CONSTRAINT [DF__Users__CreatedAt__3E52440B] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]   DATE           NULL,
    [Updatedby]   INT            NULL,
    CONSTRAINT [PK_Users_UserID] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_Createdby] FOREIGN KEY ([Createdby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Users_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([PersonID]),
    CONSTRAINT [UQ__Users__1788CCADE8510462] UNIQUE NONCLUSTERED ([UserID] ASC),
    CONSTRAINT [UQ__Users__AA2FFB84702C8063] UNIQUE NONCLUSTERED ([PersonID] ASC),
    CONSTRAINT [UQ__Users__C9F28456345A8AD8] UNIQUE NONCLUSTERED ([UserName] ASC)
);

