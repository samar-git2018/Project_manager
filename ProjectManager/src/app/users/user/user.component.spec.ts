import { User } from 'app/Model/user';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { UserComponent } from './user.component';
import { AppTestingModule } from 'app/app-testing-module';
import { UserMockService } from 'app/Mocks/user.mock.service';
import { DatePipe } from '@angular/common';
import { NgForm } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('UserComponent', () => {
    let comp: UserComponent;
    let fixture: ComponentFixture<UserComponent>;
    let de: DebugElement;
    let el: HTMLElement;
    let spy: jasmine.Spy;
    let element: HTMLElement;
    let userService: UserMockService;

    const userForm = <NgForm>{
        value: {
            User_ID: null,
            First_Name: 'Samar',
            Last_Name: 'Dutta',
            Employee_ID: 'EMP334S',
            Project_ID: 0,
            Task_ID: 0
        },
        resetForm: () => null
    };
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(UserComponent);
        comp = fixture.componentInstance;
        //userService = fixture.debugElement.injector.get(UserMockService);
        //userService = TestBed.get(userService);
        userService = new UserMockService();
        spy = spyOn(userService, 'postUser').and.callThrough();
        spyOnProperty(userService, 'UserList');
        spyOnProperty(userService, 'selectedUser');
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(UserComponent).toBeTruthy();
    });

    it(`#saveUser method: should set submitted to true`, async(() => {
        comp.saveUser(userForm);
        expect(comp.submitted).toBeTruthy();
    }));
    it(`#saveUser method: should set submitted to false if Employee ID already exists`, async(() => {
        userForm.value.Employee_ID = "EMP001";
        comp.onSubmit(userForm);
        expect(comp.submitted).toBeFalsy();
    }));

    it(`#updateUser method: should set submitted to false if Employee ID alreday exists`, async(() => {
        userForm.value.Employee_ID = "EMP001";
        userForm.value.User_ID = "2";
        comp.onSubmit(userForm);
        expect(comp.submitted).toBeFalsy();
    }));

    it(`#updateUser method: should set submitted to true`, async(() => {
        userForm.value.User_ID = 1;
        comp.updateUser(userForm);
        expect(comp.submitted).toBeTruthy();
    }));

    it(`should call the onSubmit method`, async(() => {
        spyOn(comp, 'onSubmit');
        el = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
        el.click();
        expect(comp.onSubmit).toHaveBeenCalled();
    }));
    it(`should call the restForm`, async(() => {
        spyOn(comp, 'resetForm');
        el = fixture.debugElement.query(By.css('button')).nativeElement;
        el.click();
        expect(comp.resetForm).toHaveBeenCalled();
    }));
});