﻿import { Component, OnInit } from '@angular/core';
import { TaskService } from 'app/Service/task.service';
import { UserService } from 'app/Service/user.service';
import { ProjectService } from 'app/Service/project.service';
import { Task } from 'app/Model/task';
import { Project } from 'app/Model/project';
import { User } from 'app/model/user';
import { ParentTask } from 'app/Model/parenttask';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-task',
    templateUrl: './task.component.html',
    styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
    constructor(private taskService: TaskService, private datePipe: DatePipe, private userService: UserService, private projectService: ProjectService) { }
    public searchText: string;
    ngOnInit() {
        this.resetForm();
    }
    resetForm(form?: NgForm) {
        var date = new Date();
        var currentDate = this.datePipe.transform(date, "yyyy-MM-dd");
        //debugger;
        if (form != null) {
            form.resetForm();
            form.controls["Priority"].setValue(0);
            form.controls["Start_Date"].setValue(currentDate);
            form.controls["End_Date"].setValue(this.datePipe.transform(date.setDate(date.getDate() + 1), "yyyy-MM-dd"));
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
            debugger;
            if (form.value.Parent_ID == null) {
                console.log(form.value);
                form.value.ParentTaskName = form.value.TaskName;
                this.taskService.postParentTask(form.value)
                    .subscribe(data => {
                        this.resetForm(form);
                        this.taskService.getParentTaskList().subscribe(x => this.taskService.TaskList = x as Task[]);;
                        alert('New Parent Task added Succcessfully');
                    });
            }
            else {
                this.taskService.putParentTask(form.value.Parent_ID, form.value)
                    .subscribe(data => {
                        this.resetForm(form);
                        this.taskService.getParentTaskList().subscribe(x => this.taskService.TaskList = x as Task[]);;;
                        alert('Parent Task updated Succcessfully');
                    });
            }
        }

        else {
            if (form.value.Task_ID == null) {
                console.log(form.value);
                form.value.Status = "I";
                if (typeof form.value.Start_Date != undefined && form.value.Start_Date
                    && typeof form.value.End_Date != undefined && form.value.End_Date) {
                    if (form.value.Start_Date > form.value.End_Date)
                        alert('Task end date should be greater than Task start date');
                    else {

                        this.taskService.postTask(form.value)
                            .subscribe(data => {
                                this.resetForm(form);
                                //this.taskService.getTaskList();
                                alert('New Task added Succcessfully');
                            });
                    }
                }
                else {
                    this.taskService.postTask(form.value)
                        .subscribe(data => {
                            this.resetForm(form);
                            //this.taskService.getTaskList();
                            alert('New Task added Succcessfully');
                        });
                }
            }
            else {
                if (typeof form.value.Start_Date != undefined && form.value.Start_Date
                    && typeof form.value.End_Date != undefined && form.value.End_Date) {
                    if (form.value.Start_Date > form.value.End_Date)
                        alert('Task end date should be greater than Task start date');
                    else {
                        this.taskService.putTask(form.value.Task_ID, form.value)
                            .subscribe(data => {
                                this.resetForm(form);
                                //this.taskService.getTaskList();
                                alert('Task updated Succcessfully');
                            });
                    }
                }
                else {
                    this.taskService.putTask(form.value.Task_ID, form.value)
                        .subscribe(data => {
                            this.resetForm(form);
                            //this.taskService.getTaskList();
                            alert('Task updated Succcessfully');
                        });
                }
            }
        }
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
        return this.taskService.selectedTask.UserName;
    }
    setProject(project: Project) {
        this.projectService.selectedProject = Object.assign({}, project);
        this.taskService.selectedTask.ProjectName = this.projectService.selectedProject.ProjectName;
        this.taskService.selectedTask.Project_ID = project.Project_ID;
        return this.taskService.selectedTask.ProjectName;
    }
    setParentTask(parentTask: ParentTask) {
        this.taskService.selectedTask.ParentTaskName = parentTask.ParentTaskName;
        this.taskService.selectedTask.Parent_ID = parentTask.Parent_ID;
        return this.taskService.selectedTask.ParentTaskName;
    }
}