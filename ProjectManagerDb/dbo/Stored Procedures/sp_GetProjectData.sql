
-- =============================================
-- Author:		<Author,,Samar>
-- Create date: <Create Date,,>
-- Description:	<Description, Insert task table and update user table>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetProjectData]
AS
BEGIN
	SET NOCOUNT ON;

    select a.Project_ID, a.ProjectName, a.Start_Date, a.End_Date, a.Priority, a.Manager_Id, count(b.Project_ID) TaskCount, count(d.Project_ID) CompletedTaskCount
		from Project a(nolock)
		left outer join Task b(nolock)
		on a.Project_ID = b.Project_ID
		left outer join (select Project_ID,Task_ID from Task c(nolock) where Status = 'C') d
		on a.Project_ID = d.Project_ID and b.Task_ID = d.Task_ID
		group by a.Project_ID, a.ProjectName, a.Start_Date, a.End_Date, a.Manager_Id, a.Priority
END


