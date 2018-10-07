import { UserFilterPipe } from 'app/users/user-list/user-filter.pipe';
import { User } from 'app/Model/User';

describe('UserFilterPipe', () => {

    // This pipe is a pure, stateless function so there is no need for beforeEach function
    let pipe = new UserFilterPipe();
    let users: any;
    let filterUsers: any;

    beforeEach((() => {
        users = [
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
        ];
       filterUsers = [
            {
                User_ID: 1,
                First_Name: "Samar",
                Last_Name: "Dutta",
                Employee_ID: "EMP001",
                Project_ID: 1,
                Task_ID: 1
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
            expect(pipe.transform(users, null)).toEqual(users);
        });
    });

    it('should return results if first name match', () => {
            expect(pipe.transform(users, 'Samar')).toEqual(filterUsers);
        });

        it('should return results iflast name match', () => {
            expect(pipe.transform(users, 'Dutta')).toEqual(filterUsers);
        });

        it('should return results if Employee ID match', () => {
            expect(pipe.transform(users, 'EMP001')).toEqual(filterUsers);
        });
});