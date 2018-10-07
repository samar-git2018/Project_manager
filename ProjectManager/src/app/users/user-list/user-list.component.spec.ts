import { User } from 'app/Model/user';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { UserListComponent } from './user-list.component';
import { AppTestingModule } from 'app/app-testing-module';
import { UserMockService } from 'app/Mocks/user.mock.service';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('UserListComponent', () => {
    let comp: UserListComponent;
    let fixture: ComponentFixture<UserListComponent>;
    let de: DebugElement;
    let el: HTMLElement;
    let spy: jasmine.Spy;
    let element: HTMLElement;
    let userService: UserMockService;
    let user: User;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(UserListComponent);
        comp = fixture.componentInstance;
        //userService = TestBed.get(userService);
        userService = new UserMockService();
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(UserListComponent).toBeTruthy();
    });

    it(`should user list length greater than 0`, async(() => {
        comp.ngOnInit();
        expect(userService.UserList.length > 0).toBeTruthy();
    }));

    it(`#showForEdit should get user data`, async(() => {
        user = {
            User_ID: 1,
            First_Name: "Samar",
            Last_Name: "Dutta",
            Employee_ID: "EMP001",
            Project_ID: 1,
            Task_ID: 1
        };
        comp.showForEdit(user);
        expect(userService.UserList.length > 0).toBeTruthy();
    }));
    it(`#onDelete method should delete user`, async(() => {
        spyOn(window, 'confirm').and.callFake(function () { return true; });
        spyOn(userService, 'deleteUser');
        comp.onDelete(1);
        expect(userService.deleteUser(1) == null).toBeTruthy();
    }));
    it(`#SortUser method should sort user list`, async(() => {
        comp.SortUser('First_Name');
        expect(comp.direction > 0).toBeTruthy();
    }));
});