import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'app/Service/project.service';
import { UserService } from 'app/Service/user.service';
import { Project } from 'app/Model/project';
import { User } from 'app/model/user';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-project',
    templateUrl: './project.component.html',
    styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {
    constructor(public projectService: ProjectService, public datePipe: DatePipe, public userService: UserService) { }
    submitted = false;
    searchText: any;

    ngOnInit() {
        this.resetForm();
    }

    resetForm(form?: NgForm) {
        if (form != null) {
            form.resetForm();
            if (form.controls != undefined)
                form.controls["Priority"].setValue(0);
        }
        this.projectService.selectedProject = {
            Project_ID: null,
            ProjectName: '',
            Start_Date: null,
            End_Date: null,
            Priority: 0,
            Manager_Id: 0,
            ManagerName: "",
            SetDate: false,
            TaskCount: 0,
            CompletedTaskCount: 0
        }
    }
    SetDateRange(e) {
        if (e.target.checked) {
            var date = new Date();
            this.projectService.selectedProject.Start_Date = this.datePipe.transform(date, "yyyy-MM-dd");
            this.projectService.selectedProject.End_Date = this.datePipe.transform(date.setDate(date.getDate() + 1), "yyyy-MM-dd");
        }
        else {
            this.projectService.selectedProject.Start_Date = this.projectService.selectedProject.End_Date = null;
        }
    }

    onSubmit(form: NgForm) {
        if (form.value.Project_ID == null) {
            console.log(form.value);
            if (typeof form.value.Start_Date != undefined && form.value.Start_Date
                && typeof form.value.End_Date != undefined && form.value.End_Date) {
                if (form.value.Start_Date > form.value.End_Date)
                    alert('Project end date should be greater than Project start date');
                else {
                    this.saveProject(form);
                    this.resetForm(form);
                }
            }
            else {
                this.saveProject(form);
                this.resetForm(form);
            }
        }
        else {
            if (typeof form.value.Start_Date != undefined && form.value.Start_Date
                && typeof form.value.End_Date != undefined && form.value.End_Date) {
                if (form.value.Start_Date > form.value.End_Date)
                    alert('Project end date should be greater than Project start date');
                else {
                    this.updateProject(form);
                    this.resetForm(form);
                }
            }
            else {
                this.updateProject(form);
                this.resetForm(form);
            }
        }
    }

    showUserData() {
        return this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
    }
    setUser(user: User) {
        this.userService.selectedUser = Object.assign({}, user);
        this.projectService.selectedProject.ManagerName = this.userService.selectedUser.First_Name + " " + this.userService.selectedUser.Last_Name;
        this.projectService.selectedProject.Manager_Id = user.User_ID;
        return this.projectService.selectedProject.ManagerName;
    }

    saveProject(form: NgForm) {
        this.projectService.postProject(form.value)
            .subscribe(data => {
                this.projectService.getProjectList().subscribe(x => this.projectService.ProjectList = x as Project[]);
                alert('New Project added Succcessfully');
                this.submitted = true;
            })
    }
    updateProject(form: NgForm) {
        this.projectService.putProject(form.value.Project_ID, form.value)
            .subscribe(data => {
                this.projectService.getProjectList().subscribe(x => this.projectService.ProjectList = x as Project[]);;
                alert('Project updated Succcessfully');
                this.submitted = true;
            });
    }
}
