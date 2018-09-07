export class Task{
    constructor(
        public Task_ID: number,
        public Parent_ID: number,
        public ParentTaskName: string,
        public Project_ID: number,
        public ProjectName: string,
        public TaskName: string,
        public Start_Date: string,
        public End_Date: string,
        public Priority: number,
        public Status: string,
        public SetParentTask: Boolean,
        public User_ID: number,
        public UserName: string,
    )
    { }

}
