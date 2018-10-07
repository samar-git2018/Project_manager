import { Component, OnInit } from '@angular/core';
declare var closeModal: Function;
import { TaskService } from 'app/Service/task.service';
import { UserService } from 'app/Service/user.service';
import { ProjectService } from 'app/Service/project.service';
import { Task } from 'app/model/task';
import { Project } from 'app/Model/project';
import { User } from 'app/Model/user';
import { ParentTask } from 'app/Model/parenttask';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
@Component({
    selector: 'app-task-list',
    templateUrl: './task-list.component.html',
    styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

    submitted = false;
    constructor(public taskService: TaskService, public userSerVice: UserService, public projectService: ProjectService, public datePipe: DatePipe) { }
    
    column: string = 'Start_Date';
    isDesc: boolean = false;
    public searchText: any;
    project: Project;

    ngOnInit() {
        this.taskService.getTaskList().subscribe(x => { this.taskService.TaskList = x as Task[] });
        this.userSerVice.getUserList().subscribe(x => { this.userSerVice.UserList = x as User[] });
        console.log(this.taskService.TaskList);
        var date = new Date();
        var currentDate = this.datePipe.transform(date, "yyyy-MM-dd");
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
        this.projectService.selectedProject = Object.assign({}, this.project);
        this.projectService.selectedProject.ProjectName = "";
        this.projectService.selectedProject.Project_ID = null;
    }

    showForEdit(task: Task) {
        //console.log(task);
        //debugger;
        this.taskService.selectedTask = Object.assign({}, task);
        if (typeof task.Start_Date != undefined && task.Start_Date
            && typeof task.End_Date != undefined && task.End_Date) {
            this.taskService.selectedTask.Start_Date = this.datePipe.transform(new Date(task.Start_Date), "yyyy-MM-dd");
            this.taskService.selectedTask.End_Date = this.datePipe.transform(new Date(task.End_Date), "yyyy-MM-dd");
        }
        return this.taskService.selectedTask;
    }
    removeParentTask() {
        //debugger;
        //debugger;
        this.taskService.selectedTask.Parent_ID = 0;
        this.taskService.selectedTask.ParentTaskName = null;
        return this.taskService.selectedTask;
    }


    endTask(task: Task, id: string) {
        if (confirm('Are you sure want to mark this task as Completed ?') == true) {
            task.Status = 'C';
            //debugger;
            this.taskService.putTask(id, task)
                .subscribe(data => {
                    //this.taskService.getTaskList();
                });
        }
    }
    onSubmit(form: NgForm) {
        //debugger;
        if (typeof form.value.Start_Date != undefined && form.value.Start_Date
            && typeof form.value.End_Date != undefined && form.value.End_Date) {
            if (form.value.Start_Date > form.value.End_Date)
                alert('Task end date should be greater than Task start date');
            else {
                closeModal();
                this.taskService.putTask(form.value.Task_ID, form.value)
                    .subscribe(data => {
                        this.taskService.getTaskList().subscribe(x => { this.taskService.TaskList = x as Task[] });
                        alert('Task updated Succcessfully');
                        this.submitted = true;
                    });
            }
        }
    }
    // Declare local variable
    direction: number;
    // Change sort function to this: 
    SortTask(property) {
        this.isDesc = !this.isDesc; //change the direction    
        this.column = property;
        this.direction = this.isDesc ? 1 : -1;
    }
    showProjectData() {
        this.searchText = "";
        this.projectService.getProjectList().subscribe(x => { this.projectService.ProjectList = x as Project[] });
    }
    setProject(project: Project) {
        this.projectService.selectedProject = Object.assign({}, project);
        this.searchText = project.Project_ID;
        return this.projectService.selectedProject;
    }
    showParentTaskData() {
        this.searchText = "";
        this.taskService.getParentTaskList().subscribe(x => { this.taskService.ParentTaskList = x as ParentTask[] });
    }
    setParentTask(parentTask: ParentTask) {
        this.taskService.selectedTask.ParentTaskName = parentTask.ParentTaskName;
        this.taskService.selectedTask.Parent_ID = parentTask.Parent_ID;
        return this.taskService.selectedTask.ParentTaskName;
    }
}
