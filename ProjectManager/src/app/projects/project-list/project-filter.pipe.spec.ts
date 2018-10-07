import { ProjectFilterPipe } from 'app/projects/project-list/project-filter.pipe';
import { Project } from 'app/Model/Project';

describe('UserFilterPipe', () => {

    // This pipe is a pure, stateless function so there is no need for beforeEach function
    let pipe = new ProjectFilterPipe();
    let projects: any;
    let filterProjects: any;

    beforeEach((() => {
        projects = [
            {
                Project_ID: 1,
                ProjectName: 'UVIS',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 10,
                Manager_Id: 1,
                ManagerName: "Samar",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            },
            {
                Project_ID: 2,
                ProjectName: 'Edelivery',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 20,
                Manager_Id: 2,
                ManagerName: "Abhik",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            }
        ];
        filterProjects = [
            {
                Project_ID: 1,
                ProjectName: 'UVIS',
                Start_Date: Date(),
                End_Date: Date() + 1,
                Priority: 10,
                Manager_Id: 1,
                ManagerName: "Samar",
                SetDate: false,
                TaskCount: 1,
                CompletedTaskCount: 0
            }
        ];
    }));

    it('should create an instance', () => {
        expect(pipe).toBeTruthy();
    });
    describe('Bad Inputs', () => {

        it('should return emty object if items is null ', () => {
            expect(pipe.transform(null, null)).toEqual([]);
        });

        it('should return all items if search text is empty', () => {
            expect(pipe.transform(projects, null)).toEqual(projects);
        });
    });
    it('should return results if project name match', () => {
        expect(pipe.transform(projects, 'UVIS')).toEqual(filterProjects);
    });

    it('should return results if priority match', () => {
        expect(pipe.transform(projects, '10')).toEqual(filterProjects);
    });
});