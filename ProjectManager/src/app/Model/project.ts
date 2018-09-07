export class Project {
    constructor(
        public Project_ID: number,
        public ProjectName: string,
        public Start_Date: string,
        public End_Date: string,
        public Priority: number,
        public Manager_Id: number,
        public ManagerName: string,
        public SetDate: Boolean,
        public TaskCount: number,
        public CompletedTaskCount: number,
    ) { }
}