import { Component, OnInit } from '@angular/core';

import { UserService } from 'app/Service/user.service';
import { User } from 'app/model/user';
@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

    constructor(public userService: UserService) { }

    column: string = 'First_Name';
    isDesc: boolean = false;
    searchText: any;

    ngOnInit() {
        this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
    }

    showForEdit(user: User) {
        this.userService.selectedUser = Object.assign({}, user);
        return this.userService.selectedUser;
    }


    onDelete(id: number) {
        if (confirm('Are you sure to delete this record ?') == true) {
            this.userService.deleteUser(id)
                .subscribe(x => {
                    this.userService.getUserList().subscribe(x => { this.userService.UserList = x as User[] });
                })
        }
    }
    // Declare local variable
    direction: number;
    // Change sort function to this: 
    SortUser(property) {
        this.isDesc = !this.isDesc; //change the direction    
        this.column = property;
        this.direction = this.isDesc ? 1 : -1;
    }
}
