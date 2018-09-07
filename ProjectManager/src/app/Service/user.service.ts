import { Injectable } from '@angular/core';
///import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'app/Model/user';
import { AppConstants } from 'app/Errorlogging/appconstants';

const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });


@Injectable()
export class UserService {

    selectedUser: User;
    UserList: User[];
    constructor(private http: HttpClient) { }

    postUser(User: User) {
        //debugger;
        var body = JSON.stringify(User);
        return this.http.post(AppConstants.baseURL + 'User/', body, { headers: headers });
    }

    putUser(id: string, User: string) {
        var body = JSON.stringify(User);
        return this.http.put(AppConstants.baseURL + 'User/' + id,
            body, { headers: headers });
    }

    getUserList() {
        return this.http.request("GET", AppConstants.baseURL + 'User/', { responseType: "json" });
    }

    deleteUser(id: number) {
        return this.http.delete(AppConstants.baseURL + 'User/' + id);
    }
}