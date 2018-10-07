import { Component, OnInit } from '@angular/core';
import { UserService } from 'app/Service/user.service';
import { User } from 'app/Model/user';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    constructor(public userService: UserService) { }
    public submitted: Boolean;
    ngOnInit() {
        this.resetForm();
    }

    resetForm(form?: NgForm) {
        if (form != null)
            form.resetForm();
        this.userService.selectedUser = {
            User_ID: null,
            First_Name: '',
            Last_Name: '',
            Employee_ID: '',
            Project_ID: 0,
            Task_ID: 0
        }
    }

    onSubmit(form: NgForm) {
        //debugger;
        if (form.value.User_ID == null) {
            if (!this.userService.UserList.find(dr => dr.Employee_ID == form.value.Employee_ID && dr.User_ID != form.value.User_ID)) {
                this.saveUser(form);
                this.resetForm(form);
            }
            else
            { alert('This Employee ID already exists for another user'); }
        }
        else {
            if (!this.userService.UserList.find(dr => dr.Employee_ID == form.value.Employee_ID && dr.User_ID != form.value.User_ID)) {
                this.updateUser(form);
                this.resetForm(form);
            }
            else
            { alert('This Employee ID already exists for another user'); }
        }
    }
    saveUser(form: NgForm) {
        this.userService.postUser(form.value)
            .subscribe(data => {
                this.submitted = true;
                this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
                alert('New User added Succcessfully');
            });
    }
    updateUser(form: NgForm) {
        this.userService.putUser(form.value.User_ID, form.value)
            .subscribe(data => {
                this.submitted = true;
                this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
                alert('User updated Succcessfully');
            });
    }
}
