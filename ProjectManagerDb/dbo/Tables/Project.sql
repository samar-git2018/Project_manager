CREATE TABLE [dbo].[Project] (
    [Project_ID]  INT            IDENTITY (1, 1) NOT NULL,
    [ProjectName] NVARCHAR (100) DEFAULT ('') NOT NULL,
    [Start_Date]  DATETIME       NULL,
    [End_Date]    DATETIME       NULL,
    [Priority]    INT            DEFAULT ((0)) NOT NULL,
    [Manager_Id]  INT            NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Project_ID] ASC)
);

