﻿import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { Task } from '../Model/task';
import { ParentTask } from '../Model/parenttask';
import { AppConstants } from 'app/Errorlogging/appconstants';

const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

@Injectable()
export class TaskService {

    selectedTask: Task;
    public TaskList: Task[];
    public FilteredTaskList: Task[];
    selectedParentTask: ParentTask;
    public ParentTaskList: ParentTask[];
    constructor(private http: HttpClient) { }

    postTask(Task: Task) {
        var body = JSON.stringify(Task);
        console.log(body);
        return this.http.post(AppConstants.baseURL + 'Task', body, { headers: headers });
    }

    putTask(id: string, Task: Task) {
        //debugger;
        var body = JSON.stringify(Task);
        return this.http.put(AppConstants.baseURL + 'Task/' + id,
            body, { headers: headers });
    }

    
    getTaskList() {
        return this.http.request("GET", AppConstants.baseURL + 'Task/', { responseType: "json" })            
    }

    deleteTask(id: number) {
        return this.http.delete(AppConstants.baseURL + 'Task/' + id);
    }
    //Parent task
    postParentTask(ParentTask: ParentTask) {
        var body = JSON.stringify(ParentTask);
        console.log(body);
        return this.http.post(AppConstants.baseURL + 'ParentTask', body, { headers: headers });
    }

    putParentTask(id: string, ParentTask: string) {
        var body = JSON.stringify(ParentTask);
        return this.http.put(AppConstants.baseURL + 'ParentTask/' + id,
            body, { headers: headers });
    }


    getParentTaskList() {
        return this.http.request("GET", AppConstants.baseURL + 'ParentTask/', { responseType: "json" });
    }

    deleteParentTask(id: number) {
        return this.http.delete(AppConstants.baseURL + 'ParentTask/' + id);
    }
}