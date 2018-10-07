import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppModule } from 'app/app.module';
import { ProjectService } from 'app/Service/project.service';
import { UserService } from 'app/Service/user.service';
import { UserMockService } from 'app/Mocks/user.mock.service';
import { ProjectMockService } from 'app/Mocks/project.mock.service';
import { TaskService } from 'app/Service/task.service';
import { TaskMockService } from 'app/Mocks/task.mock.service';
import { DatePipe } from '@angular/common';;
import { GlobalErrorHandler } from 'app/Errorlogging/globalerrorhandler';
import { ErrorLoggService } from 'app/Service/error-log.service';

@NgModule({
        imports: [
        AppModule
    ],
    providers: [ErrorLoggService, GlobalErrorHandler
        , { provide: ErrorHandler, useClass: GlobalErrorHandler }
        , { provide: ProjectService, useClass: ProjectMockService }
        , DatePipe, { provide: UserService, useClass: UserMockService }
        , { provide: TaskService, useClass: TaskMockService }
        , { provide: APP_BASE_HREF, useValue: '/' }]
})
export class AppTestingModule { }
