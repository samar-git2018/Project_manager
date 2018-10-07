import { Injectable } from '@angular/core';
///import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { Project } from '../Model/project';
import { AppConstants } from 'app/Errorlogging/appconstants';

const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });


@Injectable()
export class ProjectService {
    
    selectedProject: Project;
    ProjectList: Project[];
    constructor(private http: HttpClient) { }

    public postProject(project: Project) {
        return this.http.post(AppConstants.baseURL + 'Project', project, { headers: headers });
    }

    putProject(id: string, project: string) {
        //debugger; 
        return this.http.put(AppConstants.baseURL + 'Project/' + id,
            project, { headers: headers });
    }

    getProjectList() {
        return this.http.request("GET", AppConstants.baseURL + 'Project/', { responseType: "json" });
    }

    deleteProject(id: number) {
        return this.http.delete(AppConstants.baseURL + 'Project/' + id);
    }
}