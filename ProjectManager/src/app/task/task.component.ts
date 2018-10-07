import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { TaskService } from 'app/Service/task.service';
import { UserService } from 'app/Service/user.service';
import { ProjectService } from 'app/Service/project.service';
import { Task } from 'app/Model/task';
import { Project } from 'app/Model/project';
import { User } from 'app/model/user';
import { ParentTask } from 'app/Model/parenttask';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
declare var closeProjectModal, closePTaskModal, closeUserModal: Function;

@Component({
    selector: 'app-task',
    changeDetection: ChangeDetectionStrategy.OnPush,
    templateUrl: './task.component.html',
    styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
    constructor(public taskService: TaskService, public datePipe: DatePipe, public userService: UserService, public projectService: ProjectService) { }
    public searchText: string;
    submitted = false;
    ngOnInit() {
        this.resetForm();
        this.taskService.getTaskList().subscribe(x => this.taskService.TaskList = x as Task[]);
        this.taskService.getParentTaskList().subscribe(x => this.taskService.ParentTaskList = x as ParentTask[]);
        this.projectService.getProjectList().subscribe(x => this.projectService.ProjectList = x as Project[]);
        this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
    }
    resetForm(form?: NgForm) {
        var date = new Date();
        var currentDate = this.datePipe.transform(date, "yyyy-MM-dd");
        //debugger;
        if (form != null) {
            form.resetForm();
            if (form.controls != undefined) {
                form.controls["Priority"].setValue(0);
                form.controls["Start_Date"].setValue(currentDate);
                form.controls["End_Date"].setValue(this.datePipe.transform(date.setDate(date.getDate() + 1), "yyyy-MM-dd"));
            }
        }
        this.taskService.selectedTask = {
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
        }
        this.taskService.selectedTask.Start_Date = currentDate;
        this.taskService.selectedTask.End_Date = this.datePipe.transform(date.setDate(date.getDate() + 1), "yyyy-MM-dd");
    }

    onSubmit(form: NgForm) {
        if (form.value.SetParentTask) {
            //debugger;
            if (!this.taskService.ParentTaskList.find(dr => dr.ParentTaskName == form.value.ParentTaskName && dr.Parent_ID != form.value.Parent_ID)) {
                if (form.value.Parent_ID == null) {
                    form.value.ParentTaskName = form.value.TaskName;
                    this.addParentTask(form);
                    this.resetForm(form);
                }
            }
            else { alert('Parent task already exists'); }
        }
        else {
            if (!this.taskService.TaskList.find(dr => dr.TaskName == form.value.TaskName && dr.Task_ID != form.value.Task_ID)) {
                if (form.value.Task_ID == null) {
                    form.value.Status = "I";
                    if (typeof form.value.Start_Date != undefined && form.value.Start_Date
                        && typeof form.value.End_Date != undefined && form.value.End_Date) {
                        if (form.value.Start_Date > form.value.End_Date)
                            alert('Task end date should be greater than Task start date');
                        else {
                            this.addTask(form);
                            this.resetForm(form);
                        }
                    }
                    else {
                        this.addTask(form);
                        this.resetForm(form);
                    }
                }
            }
            else { alert('Task already exists'); }
        }
    }

    addParentTask(form: NgForm) {
        this.taskService.postParentTask(form.value)
            .subscribe(data => {
                this.taskService.getParentTaskList().subscribe(x => this.taskService.ParentTaskList = x as ParentTask[]);
                alert('New Parent Task added Succcessfully');
                this.submitted = true;
            });
    }

    addTask(form: NgForm) {
        this.taskService.postTask(form.value)
            .subscribe(data => {
                alert('New Task added Succcessfully');
                this.submitted = true;
            });
    }

    showUserData() {
        this.searchText = "";
        this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
    }
    showProjectData() {
        this.searchText = "";
        this.projectService.getProjectList().subscribe(x => this.projectService.ProjectList = x as Project[]);

    }
    showParentTaskData() {
        this.searchText = "";
        this.taskService.getParentTaskList().subscribe(x => this.taskService.ParentTaskList = x as ParentTask[]);
    }

    setUser(user: User) {
        this.userService.selectedUser = Object.assign({}, user);
        this.taskService.selectedTask.UserName = this.userService.selectedUser.First_Name + " " + this.userService.selectedUser.Last_Name;
        this.taskService.selectedTask.User_ID = user.User_ID;
        closeUserModal();
        return this.taskService.selectedTask.UserName;
    }
    setProject(project: Project) {
        this.projectService.selectedProject = Object.assign({}, project);
        this.taskService.selectedTask.ProjectName = this.projectService.selectedProject.ProjectName;
        this.taskService.selectedTask.Project_ID = project.Project_ID;
        closeProjectModal();
        return this.taskService.selectedTask.ProjectName;
    }
    setParentTaskName(parentTask: ParentTask) {
        this.taskService.selectedTask.ParentTaskName = parentTask.ParentTaskName;
        this.taskService.selectedTask.Parent_ID = parentTask.Parent_ID;
        closePTaskModal();
        return this.taskService.selectedTask.ParentTaskName;
    }
    setParentTask(e) {
        if (e.target.checked) {
            this.clearTaskData();
        }
    }

    clearTaskData() {
        this.taskService.selectedTask.ProjectName = "";
        this.taskService.selectedTask.Project_ID = null;
        this.taskService.selectedTask.ParentTaskName = "";
        this.taskService.selectedTask.Parent_ID = null
        this.taskService.selectedTask.Priority = 0;
        this.taskService.selectedTask.UserName = "";
        this.taskService.selectedTask.User_ID = null
        return this.taskService.selectedTask;
    }
}
