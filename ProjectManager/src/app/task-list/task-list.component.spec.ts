import { ParentTask } from 'app/Model/parenttask';
import { User } from 'app/Model/user';
import { Project } from 'app/Model/project';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { TaskListComponent } from './task-list.component';
import { AppTestingModule } from 'app/app-testing-module';
import { TaskMockService } from 'app/Mocks/task.mock.service';
import { UserMockService } from 'app/Mocks/user.mock.service';
import { ProjectMockService } from 'app/mocks/project.mock.service';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Task } from 'app/model/task';
import { By } from '@angular/platform-browser';

describe('TaskListComponent', () => {
    let comp: TaskListComponent;
    let fixture: ComponentFixture<TaskListComponent>;
    let taskService: TaskMockService;
    let userService: UserMockService;
    let projectService: ProjectMockService;
    let task: Task;
    let project: Project;
    let parentTask: ParentTask;
    const taskForm = <NgForm>{
        value: {
            Task_ID: null,
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
        resetForm: () => null
    };
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();

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
            User_ID: 1,
            UserName: 'Samar'
        };
        parentTask = {
            Parent_ID: 1,
            ParentTaskName: "Analysis"
        };
        project = {
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
        };
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(TaskListComponent);
        comp = fixture.componentInstance;
        //userService = TestBed.get(userService);
        taskService = new TaskMockService();
        userService = new UserMockService();
        projectService = new ProjectMockService();
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(TaskListComponent).toBeTruthy();
    });

    it(`should task list length greater than 0`, async(() => {
        comp.ngOnInit();
        expect(taskService.TaskList.length > 0).toBeTruthy();
    }));

    it(`#showForEdit should get task data`, async(() => {
        comp.showForEdit(task);
        expect(taskService.TaskList.length > 0).toBeTruthy();
    }));

    it(`#showProjectData should get  all project data`, async(() => {
        comp.showProjectData();
        expect(comp.searchText == "").toBeTruthy();
        expect(projectService.ProjectList.length > 0).toBeTruthy();
    }));

    it(`#setProject should set project data`, async(() => {
        comp.setProject(project);
        expect(projectService.selectedProject.ProjectName == "UVIS").toBeTruthy();
    }));

    it(`#showParentTaskData should get all parent task data`, async(() => {
        comp.showParentTaskData();
        expect(comp.searchText == "").toBeTruthy();
        expect(taskService.ParentTaskList.length > 0).toBeTruthy();
    }));

    it(`#setParentTask should set parent task data`, async(() => {
        comp.setParentTask(parentTask);
        expect(taskService.selectedTask.ParentTaskName != "").toBeTruthy();
    }));

    it(`#removeParentTask method should delete parent task`, async(() => {
        spyOn(window, 'confirm').and.callFake(function () { return true; });
        spyOn(taskService, 'deleteParentTask');
        comp.removeParentTask();
        expect(taskService.deleteParentTask(1) == null).toBeTruthy();
    }));
    it(`#endTask method should complete the task`, async(() => {
        spyOn(window, 'confirm').and.callFake(function () { return true; });
        spyOn(taskService, 'postTask');
        comp.endTask(task, "1");
        expect(true).toBeTruthy();
    }));
    it(`#SortTask method should sort task list`, async(() => {
        comp.SortTask('TaskName');
        expect(comp.direction > 0).toBeTruthy();
    }));

    it(`#onSubmit method update task: should set submitted to false if start date > end date`, async(() => {
        taskForm.value.Priority = "25";
        taskForm.value.Task_ID = 1;
        taskForm.value.End_Date = Date();
        taskForm.value.Start_Date = Date() + 1;
        comp.onSubmit(taskForm);
        expect(comp.submitted).toBeFalsy();
    }));

    it(`#onSubmit method update projet: should set submitted to true if start date < end date`, async(() => {
        taskForm.value.Priority = "25";
        taskForm.value.Task_ID = 1;
        taskForm.value.End_Date = Date() + 1;
        taskForm.value.Start_Date = Date();
        comp.onSubmit(taskForm);
        expect(comp.submitted).toBeTruthy();
    }));
});