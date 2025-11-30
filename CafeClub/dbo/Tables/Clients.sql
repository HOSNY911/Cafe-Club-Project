CREATE TABLE [dbo].[Clients] (
    [ClientID]  INT  IDENTITY (1, 1) NOT NULL,
    [PersonID]  INT  NOT NULL,
    [Createdby] INT  NOT NULL,
    [IsActive]  BIT  NULL,
    [UpdateAt]  DATE NULL,
    [Updatedby] INT  NULL,
    CONSTRAINT [PK_Clients_ClientID] PRIMARY KEY CLUSTERED ([ClientID] ASC),
    CONSTRAINT [FK_Clients_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([PersonID]),
    CONSTRAINT [FK_Clients_UserID] FOREIGN KEY ([Createdby]) REFERENCES [dbo].[Users] ([UserID]),
    CONSTRAINT [UQ__Clients__AA2FFB8474F1A6D0] UNIQUE NONCLUSTERED ([PersonID] ASC)
);

