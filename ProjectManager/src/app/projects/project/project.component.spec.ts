import { Project } from 'app/Model/project';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { ProjectComponent } from './project.component';
import { ProjectMockService } from '../../mocks/project.mock.service';
import { AppTestingModule } from 'app/app-testing-module';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { User } from 'app/Model/user';
import { By } from '@angular/platform-browser';

describe('ProjectComponent', () => {
    let comp: ProjectComponent;
    let fixture: ComponentFixture<ProjectComponent>;
    let de: DebugElement;
    let el: HTMLElement;
    let spy: jasmine.Spy;
    let element: HTMLElement;
    let projectService = null;
    let user: User;

    const projectForm = <NgForm>{
        value: {
            Project_ID: null,
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
        resetForm: () => null
    };
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(ProjectComponent);
        comp = fixture.componentInstance;
        projectService = new ProjectMockService();
        spy = spyOn(projectService, 'postProject').and.callThrough();
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(ProjectComponent).toBeTruthy();
    });

    it(`#saveProject method: should set submitted to true`, async(() => {
        comp.saveProject(projectForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#updateProject method: should set submitted to true`, async(() => {
        projectForm.value.Project_ID = 1;
        comp.updateProject(projectForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`should call the onSubmit method`, async(() => {
        spyOn(comp, 'onSubmit');
        el = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
        el.click();
        expect(comp.onSubmit).toHaveBeenCalled();
    }));
    it(`should call the restForm`, async(() => {
        spyOn(comp, 'resetForm');
        el = fixture.debugElement.query(By.css(".btn-secondary")).nativeElement;
        el.click();
        expect(comp.resetForm).toHaveBeenCalled();
    }));

    it(`#onSubmit method update projet: should set submitted to false if start date > end date`, async(() => {
        projectForm.value.Project_ID = 1;
        projectForm.value.Start_Date = Date() + 1;
        projectForm.value.ProjectName = "Esignature";
        projectForm.value.End_Date = Date();
        comp.onSubmit(projectForm);
        expect(comp.submitted).toBeFalsy();
    }));
    it(`#onSubmit method save projet: should set submitted to true if start date < end date`, async(() => {
        projectForm.value.ProjectName = "Esignature";
        projectForm.value.Project_ID = null;
        projectForm.value.End_Date = Date() + 1;
        projectForm.value.Start_Date = Date();
        comp.onSubmit(projectForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#onSubmit method update projet: should set submitted to true if start date < end date`, async(() => {
        projectForm.value.ProjectName = "Esignature";
        projectForm.value.Project_ID = 1;
        projectForm.value.End_Date = Date();
        comp.onSubmit(projectForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#onSubmit method update projet: should set submitted to true if start date and end date not defined`, async(() => {
        projectForm.value.ProjectName = "Esignature";
        projectForm.value.Project_ID = 1;
        projectForm.value.End_Date = null;
        projectForm.value.Start_Date = null;
        comp.onSubmit(projectForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#onSubmit method save projet: should set submitted to true if start date and end date not defined`, async(() => {
        projectForm.value.ProjectName = "Esignature";
        projectForm.value.Project_ID = null;
        projectForm.value.End_Date = null;
        projectForm.value.Start_Date = null;
        comp.onSubmit(projectForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#onSubmit method save project: should set submitted to false if start date > end date`, async(() => {
        projectForm.value.Start_Date = Date() + 1;
        projectForm.value.Project_ID = null;
        projectForm.value.ProjectName = "Esignature";
        projectForm.value.End_Date = Date();
        comp.onSubmit(projectForm);
        expect(comp.submitted).toBeFalsy();
    }));

    it(`#setUser method should set the user data`, async(() => {
        user = {
            User_ID: 1,
            First_Name: "Samar",
            Last_Name: "Dutta",
            Employee_ID: "EMP001",
            Project_ID: 1,
            Task_ID: 1
        };
        comp.setUser(user);
        expect(projectService.selectedProject.Manager_Id == 1);
    }));
});
