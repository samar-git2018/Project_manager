import { Injectable } from '@angular/core';
import { Observable, Subject, throwError, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Task } from 'app/Model/task';
import { ParentTask } from 'app/Model/parenttask';

@Injectable()
export class TaskMockService {

    constructor() {
        this.selectedTask = {
            Task_ID: null,
            Parent_ID: null,
            ParentTaskName: null,
            Project_ID: null,
            ProjectName: "",
            TaskName: '',
            Start_Date: null,
            End_Date: null,
            Priority: 0,
            Status: "I",
            SetParentTask: false,
            User_ID: null,
            UserName: ""
        };

        this.TaskList = [{
            Task_ID: 1,
            Parent_ID: 1,
            ParentTaskName: "Analysis",
            Project_ID: 1,
            ProjectName: "UVIS",
            TaskName: 'Analysis-Iteration 1',
            Start_Date: Date(),
            End_Date: Date() + 1,
            Priority: 5,
            Status: "I",
            SetParentTask: false,
            UserName: "Samar",
            User_ID: 1
        },
        {
            Task_ID: 2,
            Parent_ID: 2,
            ParentTaskName: "Developement",
            Project_ID: 2,
            ProjectName: "Edelivery",
            TaskName: 'Developement-Iteration 1',
            Start_Date: Date(),
            End_Date: Date() + 1,
            Priority: 8,
            Status: "I",
            SetParentTask: false,
            UserName: "Samar",
            User_ID: 1
        }];

        this.ParentTaskList = [{
            Parent_ID: 1,
            ParentTaskName: "Analysis",
        },
        {
            Parent_ID: 2,
            ParentTaskName: "Developement",
        }];
    }
    selectedTask: Task;
    private _taskList: Task[];
    public get TaskList(): Task[] {
        return this._taskList;
    }
    public set TaskList(value: Task[]) { this._taskList = value; }

    private _parentTaskList: ParentTask[];
    public get ParentTaskList(): ParentTask[] {
        return this._parentTaskList;
    }
    public set ParentTaskList(value: ParentTask[]) { this._parentTaskList = value; }

    postTask(Task: Task): Observable<any> {
        return of([
            {
                Task_ID: 1,
                Parent_ID: 1,
                ParentTaskName: "Analysis",
                Project_ID: 1,
                ProjectName: "UVIS",
                TaskName: 'Analysis-Iteration 1',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 5,
                Status: "I",
                SetParentTask: false
            }
        ]);
    }

    putTask(id: string, Task: string): Observable<any> {
        return of([
            {
                Task_ID: 1,
                Parent_ID: 1,
                ParentTaskName: "Analysis",
                Project_ID: 1,
                ProjectName: "UVIS",
                TaskName: 'Analysis-Iteration 1',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 5,
                Status: "I",
                SetParentTask: false
            }
        ]);
    }

    getTaskList(): Observable<any> {
        return of([
            {
                Task_ID: 1,
                Parent_ID: 1,
                ParentTaskName: "Analysis",
                Project_ID: 1,
                ProjectName: "UVIS",
                TaskName: 'Analysis-Iteration 1',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 5,
                Status: "I",
                SetParentTask: false
            },
            {
                Task_ID: 2,
                Parent_ID: 2,
                ParentTaskName: "Developement",
                Project_ID: 2,
                ProjectName: "Edelivery",
                TaskName: 'Developement-Iteration 1',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 8,
                Status: "I",
                SetParentTask: false
            }
        ]);
    }

    deleteTask(id: number): Observable<any> {
        return null;
    }

    getParentTaskList(ParentTask: ParentTask): Observable<any> {
        return of([
            {
                Parent_ID: 1,
                ParentTaskName: "Analysis",
            },
            {
                Parent_ID: 2,
                ParentTaskName: "Developement",
            }
        ]);
    }

    postParentTask(Task: Task): Observable<any> {
        return of([
            {
                Parent_ID: 1,
                ParentTaskName: "Analysis"
            }
        ]);
    }

    putParentTask(id: string, Task: string): Observable<any> {
        return of([
            {
                Parent_ID: 1,
                ParentTaskName: "Analysis"
            }
        ]);
    }

    deleteParentTask(id: number): Observable<any> {
        return of(null);
    }
}
