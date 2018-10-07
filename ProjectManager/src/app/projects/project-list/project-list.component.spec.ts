import { User } from 'app/Model/user';
import { Project } from 'app/Model/project';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { ProjectListComponent } from './project-list.component';
import { AppTestingModule } from 'app/app-testing-module';
import { ProjectMockService } from 'app/Mocks/project.mock.service';
import { UserMockService } from 'app/Mocks/user.mock.service';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('ProjectListComponent', () => {
    let comp: ProjectListComponent;
    let fixture: ComponentFixture<ProjectListComponent>;
    let projectService: ProjectMockService;
    let userService: UserMockService;
    let project: Project;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(ProjectListComponent);
        comp = fixture.componentInstance;
        //userService = TestBed.get(userService);
        projectService = new ProjectMockService();
        userService = new UserMockService();
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(ProjectListComponent).toBeTruthy();
    });

    it(`should task list length greater than 0`, async(() => {
        comp.ngOnInit();
        expect(projectService.ProjectList.length > 0).toBeTruthy();
    }));
    it(`#showForEdit should get project data`, async(() => {
        project = {
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
        comp.showForEdit(project);
        expect(projectService.ProjectList.length > 0).toBeTruthy();
    }));
    it(`#onDelete method should delete project`, async(() => {
        spyOn(window, 'confirm').and.callFake(function () { return true; });
        spyOn(projectService, 'deleteProject');
        comp.onDelete(1);
        expect(projectService.deleteProject(1) == null).toBeTruthy();
    }));
    it(`#SortProject method should sort user list`, async(() => {
        comp.SortProject('ProjectName');
        expect(comp.direction > 0).toBeTruthy();
    }));
});