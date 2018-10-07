import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { ProjectsComponent } from './projects.component';
import { AppTestingModule } from 'app/app-testing-module';
import { NgForm } from '@angular/forms';

describe('ProjectsComponent', () => {
    let comp: ProjectsComponent;
    let fixture: ComponentFixture<ProjectsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [AppTestingModule]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        //initialization  
        fixture = TestBed.createComponent(ProjectsComponent);
        comp = fixture.componentInstance;
        //ask fixture to detect changes
        fixture.detectChanges();
    });

    it('should be created', () => {
        expect(ProjectsComponent).toBeTruthy();
    });
});