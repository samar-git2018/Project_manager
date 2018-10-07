import { User } from 'app/Model/user';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { DebugElement } from '@angular/core';
import { UsersComponent } from './users.component';
import { AppTestingModule } from 'app/app-testing-module';
import { NgForm } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('UsersComponent', () => {
    let comp: UsersComponent;
    let fixture: ComponentFixture<UsersComponent>;
    let de: DebugElement;
    let el: HTMLElement;
    let spy: jasmine.Spy;
    let element: HTMLElement;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(UsersComponent);
        comp = fixture.componentInstance;            
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(UsersComponent).toBeTruthy();
    });   
});