import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'app/Service/project.service';
import { UserService } from 'app/Service/user.service';
import { Project } from 'app/model/project';
import { User } from 'app/model/user';
import { DatePipe } from '@angular/common';
@Component({
    selector: 'app-project-list',
    templateUrl: './project-list.component.html',
    styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

    constructor(public projectService: ProjectService, public userSerVice: UserService, public datePipe: DatePipe) { }

    column: string = 'Start_Date'; 
    isDesc: boolean = false;
    searchText: any;

    ngOnInit() {
        this.projectService.getProjectList().subscribe(x => { this.projectService.ProjectList = x as Project[] });;
        this.userSerVice.getUserList().subscribe(x => { this.userSerVice.UserList = x as User[] });
    }

    showForEdit(project: Project) {
        console.log(project)
        this.projectService.selectedProject = Object.assign({}, project);
        if (typeof project.Start_Date != undefined && project.Start_Date
            && typeof project.End_Date != undefined && project.End_Date) {
            this.projectService.selectedProject.Start_Date = this.datePipe.transform(new Date(project.Start_Date), "yyyy-MM-dd");
            this.projectService.selectedProject.End_Date = this.datePipe.transform(new Date(project.End_Date), "yyyy-MM-dd");
            this.projectService.selectedProject.SetDate = true;
        }
        if (this.userSerVice.UserList.find(user => user.User_ID == project.Manager_Id))
            this.projectService.selectedProject.ManagerName = this.userSerVice.UserList.find(user => user.User_ID == project.Manager_Id).First_Name + " " + this.userSerVice.UserList.find(user => user.User_ID == project.Manager_Id).Last_Name;
        return this.projectService.selectedProject;
    }


    onDelete(id: number) {
        if (confirm('Are you sure to delete this record ?') == true) {
            this.projectService.deleteProject(id)
                .subscribe(x => {
                    this.projectService.getProjectList().subscribe(x => this.projectService.ProjectList = x as Project[]);
                })
        }
    }
    // Declare local variable
    direction: number;
    // Change sort function to this: 
    SortProject(property) {
        this.isDesc = !this.isDesc; //change the direction    
        this.column = property;
        this.direction = this.isDesc ? 1 : -1;
    }
}
