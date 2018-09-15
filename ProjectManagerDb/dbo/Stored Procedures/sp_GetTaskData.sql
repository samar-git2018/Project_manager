
-- =============================================
-- Author:		<Author,,Samar>
-- Create date: <Create Date,,>
-- Description:	<Description, Insert task table and update user table>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTaskData]
AS
BEGIN
	SET NOCOUNT ON;

    select a.Task_ID, a.TaskName,a.Project_ID, c.ProjectName, a.Start_Date, a.End_Date, isnull(a.Priority, 0) Priority, a.Status, a.Parent_ID, b.ParentTaskName 
		from Task a(nolock)
		left outer join Parent_Task b(nolock)
		on a.Parent_ID = b.Parent_ID
		left outer join Project c(nolock)
		on a.Project_ID = c.Project_ID
END


