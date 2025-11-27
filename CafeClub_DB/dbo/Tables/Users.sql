CREATE TABLE [dbo].[Users] (
    [UserID]      INT            IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (20)  NOT NULL,
    [Password]    NVARCHAR (150) NOT NULL,
    [Permissions] INT            NOT NULL,
    [IsActive]    BIT            DEFAULT ((1)) NOT NULL,
    [PersonID]    INT            NOT NULL,
    [Createdby]   INT            NULL,
    [CreatedAt]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]   DATE           NULL,
    [Updatedby]   INT            NULL,
    CONSTRAINT [PK_Users_UserID] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Users_Createdby] FOREIGN KEY ([Createdby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Users_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([PersonID]),
    UNIQUE NONCLUSTERED ([PersonID] ASC),
    UNIQUE NONCLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

