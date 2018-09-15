CREATE TABLE [dbo].[Task] (
    [Task_ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Parent_ID]  INT            DEFAULT ((0)) NOT NULL,
    [Project_ID] INT            NOT NULL,
    [TaskName]   NVARCHAR (100) NOT NULL,
    [Start_Date] DATETIME       NULL,
    [End_Date]   DATETIME       NULL,
    [Priority]   INT            NULL,
    [Status]     CHAR (1)       NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Task_ID] ASC)
);

