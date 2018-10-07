import { TestBed, async } from '@angular/core/testing';
import { APP_BASE_HREF } from '@angular/common';
import { AppModule } from './app.module';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
    
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [
                
            ],
            imports: [
                AppModule
            ],
            providers: [
                { provide: APP_BASE_HREF, useValue: '/' }
            ]
        }).compileComponents();
    }));

    it('should create the app', async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it(`should have as title 'Project Manager'`, async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app.title).toEqual('Project Manager');
    }));

    it('should render title in a h4 tag', async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        fixture.detectChanges();
        const compiled = fixture.debugElement.nativeElement;
        expect(compiled.querySelector('h4').textContent).toContain("Project Manager");
    }));
});
