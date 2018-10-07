import { TestBed, inject } from '@angular/core/testing';
import { Task } from 'app/Model/task';
import { ParentTask } from 'app/Model/parenttask';
import { TaskService } from './task.service';
import { HttpModule } from '@angular/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AppConstants } from 'app/Errorlogging/appconstants';

describe('TaskService', () => {
    let taskService: TaskService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule
            ],
            providers: [
                TaskService
            ]
        });

        taskService = TestBed.get(TaskService);
        httpMock = TestBed.get(HttpTestingController);
    });

    it('should successfully get tasks', (done) => {

        taskService.getTaskList()
            .subscribe(res => {
                expect(res).toEqual(
                    [
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
                            SetParentTask: false,
                            User_ID: 1,
                            UserName: "Samar"
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
                            User_ID: 2,
                            UserName: "Abhik"
                        }
                    ]
                );
                done();
            });
        let taskRequest = httpMock.expectOne(AppConstants.baseURL + 'Task/');
        console.log(taskRequest.request.url);
        expect(taskRequest.request.method).toBe('GET');
        taskRequest.flush([
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
                SetParentTask: false,
                User_ID: 1,
                UserName: "Samar"
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
                User_ID: 2,
                UserName: "Abhik"
            }
        ]);

        httpMock.verify();
    });
    it('should post the correct task data', () => {

        let task =
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
                SetParentTask: false,
                User_ID: 1,
                UserName: "Samar"
            };
        taskService.postTask(task).subscribe((data: any) => {
            expect(data.TaskName).toBe('UVIS');
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `Task`,
            'post to api'
        );
        expect(req.request.method).toBe('POST');

        req.flush({
            TaskName: 'UVIS',
        });

        httpMock.verify();
    });

    it('should put the correct task data', () => {
        let task =
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
                SetParentTask: false,
                User_ID: 1,
                UserName: "Samar"
            };
        taskService.putTask
            ("1", task).subscribe((data: any) => {
                expect(data.TaskName).toBe('CMS');
            });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `Task/1`,
            'put to api'
        );
        expect(req.request.method).toBe('PUT');

        req.flush({
            TaskName: 'CMS',
        });

        httpMock.verify();
    });

    it('should delete the correct task data', () => {
        taskService.deleteTask(1).subscribe((data: any) => {
            expect(data).toBe(1);
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `Task/1`,
            'delete to api'
        );
        expect(req.request.method).toBe('DELETE');

        req.flush(1);

        httpMock.verify();
    });

    it('should successfully get parent tasks', (done) => {

        taskService.getParentTaskList()
            .subscribe(res => {
                expect(res).toEqual(
                    [
                        {
                            Parent_ID: 1,
                            ParentTaskName: "Analysis",
                        },
                        {
                            Parent_ID: 2,
                            ParentTaskName: "Developement",
                        }
                    ]
                );
                done();
            });
        let parenttaskRequest = httpMock.expectOne(AppConstants.baseURL + 'ParentTask/');
        console.log(parenttaskRequest.request.url);
        expect(parenttaskRequest.request.method).toBe('GET');
        parenttaskRequest.flush([
            {
                Parent_ID: 1,
                ParentTaskName: "Analysis",
            },
            {
                Parent_ID: 2,
                ParentTaskName: "Developement",
            }
        ]);

        httpMock.verify();
    });
    it('should post the correct parent task data', () => {

        let parenttask =
            {
                Parent_ID: 1,
                ParentTaskName: "Analysis",
            };
        taskService.postParentTask(parenttask).subscribe((data: any) => {
            expect(data.ParentTaskName).toBe('UVIS');
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `ParentTask`,
            'post to api'
        );
        expect(req.request.method).toBe('POST');

        req.flush({
            ParentTaskName: 'UVIS',
        });

        httpMock.verify();
    });

    it('should put the correct parent task data', () => {
        let parenttask =
            {
                Parent_ID: 1,
                ParentTaskName: "Analysis",
            };
        taskService.putParentTask
            ("1", JSON.stringify(parenttask)).subscribe((data: any) => {
                expect(data.ParentTaskName).toBe('CMS');
            });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `ParentTask/1`,
            'put to api'
        );
        expect(req.request.method).toBe('PUT');

        req.flush({
            ParentTaskName: 'CMS',
        });

        httpMock.verify();
    });

    it('should delete the correct parent task data', () => {
        taskService.deleteParentTask(1).subscribe((data: any) => {
            expect(data).toBe(1);
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `ParentTask/1`,
            'delete to api'
        );
        expect(req.request.method).toBe('DELETE');

        req.flush(1);

        httpMock.verify();
    });

});