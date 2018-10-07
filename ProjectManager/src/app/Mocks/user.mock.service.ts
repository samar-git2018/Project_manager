import { Injectable } from '@angular/core';
import { Observable, Subject, throwError, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'app/Model/user';
import { Http, Headers } from '@angular/http';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';

@Injectable()
export class UserMockService {

    constructor() {
        this.UserList = [{
            User_ID: 1,
            First_Name: "Samar",
            Last_Name: "Dutta",
            Employee_ID: "EMP001",
            Project_ID: 1,
            Task_ID: 1
        }];
        this.selectedUser = {
            User_ID: 1,
            First_Name: "Samar",
            Last_Name: "Dutta",
            Employee_ID: "EMP001",
            Project_ID: 1,
            Task_ID: 1
        };
    }
    
    private _selectedUser: User;
    public get selectedUser(): User {
        return this._selectedUser;
    }
    public set selectedUser(value: User) { this._selectedUser = value; }

    private _userList: User[];
    get UserList(): User[] {
        return this._userList;
    }
    set UserList(value: User[]) { this._userList = value; }

    postUser(id: string, User: string): Observable<any> {
        return of([
            {
                User_ID: 1,
                First_Name: "Samar",
                Last_Name: "Dutta",
                Employee_ID: "EMP001",
                Project_ID: 1,
                Task_ID: 1
            }
        ]);
    }

    putUser(id: string, User: string): Observable<any> {
        return of([
            {
                User_ID: 1,
                First_Name: "Samar",
                Last_Name: "Dutta",
                Employee_ID: "EMP001",
                Project_ID: 1,
                Task_ID: 1
            }
        ]);
    }

    getUserList(): Observable<any> {
        return of([
            {
                User_ID: 1,
                First_Name: "Samar",
                Last_Name: "Dutta",
                Employee_ID: "EMP001",
                Project_ID: 1,
                Task_ID: 1
            },
            {
                User_ID: 2,
                First_Name: "Abhik",
                Last_Name: "Dey",
                Employee_ID: "EMP002",
                Project_ID: 2,
                Task_ID: 2
            }
        ]);
    }

    deleteUser(id: number): Observable<any> {
        return of(null);
    }
}
