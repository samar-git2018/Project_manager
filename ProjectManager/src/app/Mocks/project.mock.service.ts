import { Injectable } from '@angular/core';
import { Observable, Subject, throwError, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Project } from 'app/Model/project';

@Injectable()
export class ProjectMockService {

    constructor() {
        this.ProjectList = [{
            Project_ID: 1,
            ProjectName: 'UVIS',
            Start_Date: Date(),
            End_Date: Date() + 1,
            Priority: 10,
            Manager_Id: 1,
            ManagerName: "Samar",
            SetDate: false,
            TaskCount: 1,
            CompletedTaskCount: 0
        },
        {
            Project_ID: 2,
            ProjectName: 'Edelivery',
            Start_Date: Date(),
            End_Date: Date() + 1,
            Priority: 20,
            Manager_Id: 2,
            ManagerName: "Abhik",
            SetDate: false,
            TaskCount: 1,
            CompletedTaskCount: 0
        }];

        this.selectedProject = {
            Project_ID: 1,
            ProjectName: 'UVIS',
            Start_Date: Date(),
            End_Date: Date() + 1,
            Priority: 10,
            Manager_Id: 1,
            ManagerName: "Samar",
            SetDate: false,
            TaskCount: 1,
            CompletedTaskCount: 0
        }
    }
    private _selectedProject: Project;
    public get selectedProject(): Project {
        return this._selectedProject;
    }
    public set selectedProject(value: Project) { this._selectedProject = value; }
    private _projectList: Project[];
    public get ProjectList(): Project[] {
        return this._projectList;
    }
    public set ProjectList(value: Project[]) { this._projectList = value; }

    postProject(Project: Project): Observable<any> {
        return of([
            {
                Project_ID: 1,
                ProjectName: 'UVIS',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 10,
                Manager_Id: 1,
                ManagerName: "Samar",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            }
        ]);
    }

    putProject(id: string, Project: string): Observable<any> {
        return of([
            {
                Project_ID: 1,
                ProjectName: 'UVIS',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 10,
                Manager_Id: 1,
                ManagerName: "Samar",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            }
        ]);
    }

    getProjectList(): Observable<any> {
        return of([
            {
                Project_ID: 1,
                ProjectName: 'UVIS',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 10,
                Manager_Id: 1,
                ManagerName: "Samar",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            },
            {
                Project_ID: 2,
                ProjectName: 'Edelivery',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 20,
                Manager_Id: 2,
                ManagerName: "Abhik",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            }
        ]);
    }

    deleteProject(id: number): Observable<any> {
        return of(null);
    }
}

