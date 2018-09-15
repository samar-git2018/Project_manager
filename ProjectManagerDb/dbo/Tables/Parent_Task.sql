CREATE TABLE [dbo].[Parent_Task] (
    [Parent_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [ParentTaskName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Parent_Task] PRIMARY KEY CLUSTERED ([Parent_ID] ASC)
);

