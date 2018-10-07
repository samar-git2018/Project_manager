import { Task } from 'app/Model/task';
import { User } from 'app/Model/user';
import { Project } from 'app/Model/project';
import { ParentTask } from 'app/Model/parenttask';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { TaskComponent } from './task.component';
import { AppTestingModule } from 'app/app-testing-module';
import { TaskMockService } from 'app/Mocks/task.mock.service';
import { ProjectMockService } from 'app/Mocks/project.mock.service';
import { UserMockService } from 'app/Mocks/user.mock.service';
import { DatePipe } from '@angular/common';
import { NgForm, FormControl, Validators, NgModel, FormBuilder } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('TaskComponent', () => {
    let comp: TaskComponent;
    let fixture: ComponentFixture<TaskComponent>;
    let de: DebugElement;
    let el: HTMLElement;
    let spy: jasmine.Spy;
    let element: HTMLElement;
    let taskService = null;
    let userService = null;
    let projectService = null;
    let parentTaskService = null;
    let user: User;
    let project: Project;
    let parenttask: ParentTask;
    let task: Task;
    let ngPriorityModel: NgModel;
    let ngStartDateModel: NgModel;
    let ngEndDateModel: NgModel;

    const taskForm = <NgForm>{
        value: {
            Task_ID: null,
            Parent_ID: null,
            ParentTaskName: "High level Analysis",
            Project_ID: 0,
            ProjectName: null,
            TaskName: 'Analysis-Iteration 1',
            Start_Date: Date(),
            End_Date: Date() + 1,
            Priority: 5,
            Status: "I",
            SetParentTask: false,
            User_ID: 0,
            UserName: null
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
        fixture = TestBed.createComponent(TaskComponent);
        comp = fixture.componentInstance;
        taskService = new TaskMockService();
        projectService = new ProjectMockService();
        userService = new UserMockService();
        spy = spyOn(taskService, 'postTask').and.callThrough();
        spy = spyOnProperty(taskService, 'TaskList').and.callThrough();
        spy = spyOnProperty(userService, 'UserList').and.callThrough();
        spy = spyOnProperty(projectService, 'ProjectList').and.callThrough();

        spy = spyOnProperty(taskService, 'ParentTaskList').and.callThrough();
        //comp.taskForm.form.controls["Priority"].setValue(8);
        //console.log(comp.taskForm.form.taskForm.controls["Priority"]);
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(TaskComponent).toBeTruthy();
    });

    it(`#addTask should set submitted to true`, async(() => {
        comp.addTask(taskForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#addParentTask should set submitted to true`, async(() => {
        taskForm.value.SetParentTask = true;
        comp.addParentTask(taskForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`#addParentTask method: should set submitted to false if ParentTaskName already exists`, async(() => {
        taskForm.value.ParentTaskName = "Analysis";
        taskForm.value.SetParentTask = true;
        comp.onSubmit(taskForm);
        expect(comp.submitted).toBeFalsy();
    }));

    it(`#addTask method: should set submitted to false if TaskName already exists`, async(() => {
        taskForm.value.TaskName = "Analysis-Iteration 1";
        comp.onSubmit(taskForm);
        expect(comp.submitted).toBeFalsy();
    }));

    it(`#addTask method: should set submitted to false if start date < end date`, async(() => {
        taskForm.value.Start_Date = Date() + 1;
        taskForm.value.TaskName = "Analysis-Iteration 2";
        //debugger;
        taskForm.value.End_Date = Date();
        comp.onSubmit(taskForm);
        expect(comp.submitted).toBeFalsy();
    }));
    it(`should call the onSubmit method`, async(() => {
        spyOn(comp, 'onSubmit');
        el = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
        el.click();
        expect(comp.onSubmit).toHaveBeenCalled();
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

    it(`should call the showUserData`, async(() => {
        spyOn(comp, 'showUserData');
        el = fixture.debugElement.query(By.css(".showUserData")).nativeElement;
        el.click();
        expect(comp.showUserData).toHaveBeenCalled();
    }));

    it(`should call the showProjectData`, async(() => {
        spyOn(comp, 'showProjectData');
        el = fixture.debugElement.query(By.css(".showProjectData")).nativeElement;
        el.click();
        expect(comp.showProjectData).toHaveBeenCalled();
    }));

    it(`should call the showParentTaskData`, async(() => {
        spyOn(comp, 'showParentTaskData');
        el = fixture.debugElement.query(By.css(".showParentTaskData")).nativeElement;
        el.click();
        expect(comp.showParentTaskData).toHaveBeenCalled();
    }));

    it(`showUserData should get user data`, async(() => {
        comp.showUserData();
        expect(userService.UserList.length > 0);
    }))

    it(`showProjectData should get projectt data`, async(() => {
        comp.showProjectData()
        expect(projectService.ProjectList.length > 0);
    }));

    it(`showParentTaskData should get parent task data`, async(() => {
        comp.showParentTaskData();
        expect(taskService.TaskList.length > 0);
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
        expect(userService.selectedUser.First_Name == "Samar");
    }));

    it(`#setProject method should set the project data`, async(() => {
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
        comp.setProject(project);
        expect(projectService.selectedProject.ProjectName == "UVIS");
    }));

    it(`#setParentTaskName method should set the parent task data`, async(() => {
        task = {
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
        };
        comp.setParentTaskName(task);
        expect(taskService.selectedTask.ParentTaskName == "Analysis");
    }));

    it(`#SetParentTask method should be called`, async(() => {
        spyOn(comp, 'setParentTask');
        el = fixture.debugElement.query(By.css("#SetParentTask")).nativeElement;
        el.click();
        expect(comp.setParentTask).toHaveBeenCalled();
    }));

    it(`#clearTaskData method should set the user data`, async(() => {
        comp.clearTaskData();
        expect(taskService.selectedTask.ProjectName == "");
    }));

    it(`#ngOnInit method should set user, project and task data`, async(() => {
        comp.ngOnInit();
        expect(taskService.TaskList.length > 0);
    }));
});