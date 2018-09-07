using System;
using System.Collections.Generic;
using ProjectManager.Persistence;

namespace ProjectManager.WebApi.Tests.TestsHelper
{
    /// <summary>
    /// Data initializer for unit tests
    /// </summary>
    public class DataInitializer
    {
        /// <summary>
        /// Dummy users
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
            var users = new List<User>
                               {
                                   new User() {User_ID = 1,First_Name = "Sam", Last_Name = "Das", Employee_ID = "EMP001"},
                                   new User() {User_ID = 2,First_Name = "Ram", Last_Name = "Das", Employee_ID = "EMP002"},
                                   new User() {User_ID = 3,First_Name = "Kuntal", Last_Name = "Das", Employee_ID = "EMP003"},
                                   new User() {User_ID = 4,First_Name = "Jack", Last_Name = "Das", Employee_ID = "EMP00"},
                                   new User() {User_ID = 5,First_Name = "Nilanjan", Last_Name = "Das", Employee_ID = "EMP005"}
                               };
            return users;
        }

        /// <summary>
        /// Dummy Projects
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetAllProjects()
        {
            var Projects = new List<Project>
                               {
                                   new Project()
                                       {
                                       Project_ID =1,
                                           ProjectName = "UVIS",
                                           Start_Date = DateTime.Now,
                                           End_Date = DateTime.Now.AddDays(1),
                                           Priority = 10
                                       },
                                   new Project()
                                       {
                                       Project_ID=2,
                                           ProjectName = "Edelivery",
                                           Start_Date = DateTime.Now,
                                           End_Date = DateTime.Now.AddDays(1),
                                           Priority = 20
                                       }
                               };

            return Projects;
        }

        /// <summary>
        /// Dummy tasks
        /// </summary>
        /// <returns></returns>
        public static List<Task> GetAllTasks()
        {
            var Tasks = new List<Task>
                               {
                                   new Task()
                                       {
                                       Task_ID=1,
                                           TaskName = "Analysis",
                                           Project_ID = 1,
                                           Priority = 10,
                                           Parent_ID = 1,
                                           Start_Date =  DateTime.Now,
                                           End_Date =  DateTime.Now.AddDays(1),
                                           User_ID = 1
                                       },
                                   new Task()
                                       {
                                       Task_ID=2,
                                          TaskName = "Developement",
                                           Project_ID = 1,
                                           Priority = 10,
                                           Parent_ID = 1,
                                           Start_Date =  DateTime.Now,
                                           End_Date =  DateTime.Now.AddDays(1),
                                           User_ID = 1
                                       },
                                   new Task()
                                       {
                                       Task_ID =3,
                                          TaskName = "Testing",
                                           Project_ID = 1,
                                           Priority = 10,
                                           Parent_ID = 1,
                                           Start_Date =  DateTime.Now,
                                           End_Date =  DateTime.Now.AddDays(1),
                                           User_ID = 3
                                       }
                               };

            return Tasks;
        }
        /// <summary>
        /// Dummy parent tasks
        /// </summary>
        /// <returns></returns>
        public static List<Parent_Task> GetAllParentTasks()
        {
            var ParentTasks = new List<Parent_Task>
                               {
                                   new Parent_Task()
                                       {
                                       Parent_ID = 1,
                                       ParentTaskName = "Project management"
                                       },
                                   new Parent_Task()
                                       {
                                       Parent_ID = 2,
                                       ParentTaskName = "Testing"
                                       }
            };

            return ParentTasks;
        }
    }
}
