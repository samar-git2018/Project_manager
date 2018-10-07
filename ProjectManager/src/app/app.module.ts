import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ProjectService } from './Service/project.service';
import { UserService } from './Service/user.service';
import { TaskService } from './Service/task.service';
import { DatePipe } from '@angular/common';;
import { ProjectsComponent } from './projects/projects.component';
import { ProjectComponent } from 'app/projects/project/project.component';
import { ProjectListComponent } from 'app/projects/project-list/project-list.component';
import { UsersComponent } from './users/users.component';
import { UserListComponent } from 'app/users/user-list/user-list.component';
import { UserComponent } from './users/user/user.component';
import { UserFilterPipe } from 'app/users/user-list/user-filter.pipe';
import { UserOrderByPipe } from 'app/users/user-list/user-orderby.pipe';
import { ProjectFilterPipe } from 'app/projects/project-list/project-filter.pipe';
import { ProjectOrderByPipe } from 'app/projects/project-list/project-orderby.pipe';
import { TaskFilterPipe } from 'app/task-list/task-filter.pipe';
import { TaskOrderByPipe } from 'app/task-list/task-orderby.pipe';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskComponent } from './task/task.component';
import { RequiredIfDirective } from 'app/task/required-if-validator';
import { GlobalErrorHandler } from 'app/Errorlogging/globalerrorhandler';
import { ErrorLoggService } from 'app/Service/error-log.service';

@NgModule({
    declarations: [
        AppComponent,
        ProjectsComponent,
        ProjectComponent,
        ProjectListComponent,
        UsersComponent,
        UserComponent,
        UserListComponent,
        UserFilterPipe,
        UserOrderByPipe,
        ProjectFilterPipe,
        ProjectOrderByPipe,
        TaskListComponent,
        TaskComponent,
        TaskFilterPipe,
        TaskOrderByPipe,
        RequiredIfDirective],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot([
            { path: '', redirectTo: '/users', pathMatch: 'full'},
            { path: 'users', component: UsersComponent },
            { path: 'projects', component: ProjectsComponent },
            { path: 'task', component: TaskComponent },
            { path: 'tasklist', component: TaskListComponent },
        ])
    ],
    providers: [ErrorLoggService, GlobalErrorHandler, { provide: ErrorHandler, useClass: GlobalErrorHandler},ProjectService, DatePipe, UserService, TaskService],
    bootstrap: [AppComponent]
})
export class AppModule { }
