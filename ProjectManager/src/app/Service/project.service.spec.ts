import { TestBed, inject } from '@angular/core/testing';
import { Project } from 'app/Model/project';
import { ProjectService } from './project.service';
import { HttpModule } from '@angular/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AppConstants } from 'app/Errorlogging/appconstants';

describe('ProjectService', () => {
    let projectService: ProjectService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule
            ],
            providers: [
                ProjectService
            ]
        });

        projectService = TestBed.get(ProjectService);
        httpMock = TestBed.get(HttpTestingController);
    });

    it('should successfully get projects', (done) => {

        projectService.getProjectList()
            .subscribe(res => {
                expect(res).toEqual(
                    [
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
                    ]
                );
                done();
            });
        let projectRequest = httpMock.expectOne(AppConstants.baseURL+ 'Project/');
        console.log(projectRequest.request.url);
        expect(projectRequest.request.method).toBe('GET');
        projectRequest.flush([
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

        httpMock.verify();
    });
    it('should post the correct data', () => {

        let project =
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
            };
        projectService.postProject(project).subscribe((data: any) => {
            expect(data.ProjectName).toBe('UVIS');
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + 'Project',
            'post to api'
        );
        expect(req.request.method).toBe('POST');

        req.flush({
            ProjectName: 'UVIS',
        });

        httpMock.verify();
    });

    it('should put the correct data', () => {
        let project =
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
            };
        projectService.putProject
            ("1", JSON.stringify(project)).subscribe((data: any) => {
                expect(data.ProjectName).toBe('CMS');
            });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `Project/1`,
            'put to api'
        );
        expect(req.request.method).toBe('PUT');

        req.flush({
            ProjectName: 'CMS',
        });

        httpMock.verify();
    });

    it('should delete the correct data', () => {
        projectService.deleteProject(1).subscribe((data: any) => {
            expect(data).toBe(1);
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL +`Project/1`,
            'delete to api'
        );
        expect(req.request.method).toBe('DELETE');

        req.flush(1);

        httpMock.verify();
    });

});