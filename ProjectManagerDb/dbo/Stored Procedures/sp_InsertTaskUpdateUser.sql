-- =============================================
-- Author:		<Author,,Samar>
-- Create date: <Create Date,,>
-- Description:	<Description, Insert task table and update user table>
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertTaskUpdateUser]

@Parent_ID int,
@TaskName nvarchar(100),
@Start_Date datetime,
@End_Date datetime,
@Priority int,
@Status char(1),
@User_ID int,
@Project_ID int
AS
BEGIN
	SET NOCOUNT ON;

    insert into Task values (@Parent_ID, @Project_ID, @TaskName, @Start_Date, @End_Date, @Priority, @Status)

	update Users set Project_ID = @Project_ID , Task_ID = @@IDENTITY where User_ID = @User_ID;
END

