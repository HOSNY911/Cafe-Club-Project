CREATE TABLE [dbo].[Prices] (
    [PriceID]          INT             IDENTITY (1, 1) NOT NULL,
    [BilliardPrice]    DECIMAL (10, 2) DEFAULT ((1.00)) NOT NULL,
    [PingPongPrice]    DECIMAL (10, 2) DEFAULT ((1.00)) NOT NULL,
    [PlaystationPrice] DECIMAL (10, 2) DEFAULT ((1.00)) NOT NULL,
    [CreatedAt]        DATETIME        DEFAULT (getdate()) NOT NULL,
    [Createby]         INT             NOT NULL,
    [Updatedby]        INT             NULL,
    [UpdatedAt]        DATETIME        NULL,
    [IsActive]         BIT             DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Prices_PriceID] PRIMARY KEY CLUSTERED ([PriceID] ASC),
    CONSTRAINT [FK_Prices_Createby] FOREIGN KEY ([Createby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [FK_Prices_Updatedby] FOREIGN KEY ([Updatedby]) REFERENCES [dbo].[Users] ([UserID])
);

