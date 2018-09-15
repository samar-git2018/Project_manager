CREATE TABLE [dbo].[Users] (
    [User_ID]     INT           IDENTITY (1, 1) NOT NULL,
    [First_Name]  NVARCHAR (50) NOT NULL,
    [Last_Name]   NVARCHAR (50) NOT NULL,
    [Employee_ID] NVARCHAR (50) NOT NULL,
    [Project_ID]  INT           NOT NULL,
    [Task_ID]     INT           NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([User_ID] ASC),
    UNIQUE NONCLUSTERED ([Employee_ID] ASC)
);

