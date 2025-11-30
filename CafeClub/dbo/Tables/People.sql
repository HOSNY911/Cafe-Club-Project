CREATE TABLE [dbo].[People] (
    [PersonID] INT           IDENTITY (1, 1) NOT NULL,
    [Phone]    NVARCHAR (20) NOT NULL,
    [FullName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_People_PersonID] PRIMARY KEY CLUSTERED ([PersonID] ASC),
    UNIQUE NONCLUSTERED ([Phone] ASC)
);

