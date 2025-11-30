CREATE TABLE [dbo].[Games] (
    [GameID]       INT             IDENTITY (1, 1) NOT NULL,
    [GameName]     NVARCHAR (50)   NOT NULL,
    [PricePerHour] DECIMAL (10, 2) NOT NULL,
    [CreatedAt]    DATE            DEFAULT (getdate()) NOT NULL,
    [Createby]     INT             NOT NULL,
    [Updatedby]    INT             NULL,
    [UpdatedAt]    DATE            NULL,
    [IsActive]     BIT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_GameID_Games] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Createby_Games] FOREIGN KEY ([Createby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Updatedby_Games] FOREIGN KEY ([Updatedby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [UQ_GameName_Games] UNIQUE NONCLUSTERED ([GameName] ASC)
);

