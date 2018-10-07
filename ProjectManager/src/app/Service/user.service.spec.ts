import { TestBed, inject } from '@angular/core/testing';
import { User } from 'app/Model/user';
import { UserService } from './user.service';
import { HttpModule } from '@angular/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AppConstants } from 'app/Errorlogging/appconstants';

describe('UserService', () => {
    let userService: UserService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule
            ],
            providers: [
                UserService
            ]
        });

        userService = TestBed.get(UserService);
        httpMock = TestBed.get(HttpTestingController);
    });

    it('should successfully get users', (done) => {

        userService.getUserList()
            .subscribe(res => {
                expect(res).toEqual(
                    [
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
                    ]
                );
                done();
            });
        let userRequest = httpMock.expectOne(AppConstants.baseURL + 'User/');
        expect(userRequest.request.method).toBe('GET');
        userRequest.flush([
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

        httpMock.verify();
    });
    it('should post the correct data', () => {

        let user =
            {
                User_ID: null,
                First_Name: 'Samar',
                Last_Name: 'Dutta',
                Employee_ID: 'EMP334S',
                Project_ID: 0,
                Task_ID: 0
            };
        userService.postUser(user).subscribe((data: any) => {
            expect(data.First_Name).toBe('Samar');
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `User/`,
            'post to api'
        );
        expect(req.request.method).toBe('POST');

        req.flush({
            First_Name: 'Samar',
        });

        httpMock.verify();
    });

    it('should put the correct data', () => {
        let user =
            {
                User_ID: null,
                First_Name: 'Samar',
                Last_Name: 'Dutta',
                Employee_ID: 'EMP3349',
                Project_ID: 0,
                Task_ID: 0
            };
        userService.putUser
            ("1", JSON.stringify(user)).subscribe((data: any) => {
                expect(data.Employee_ID).toBe('EMP3349');
            });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `User/1`,
            'put to api'
        );
        expect(req.request.method).toBe('PUT');

        req.flush({
            Employee_ID: 'EMP3349',
        });

        httpMock.verify();
    });

    it('should delete the correct data', () => {
        userService.deleteUser(1).subscribe((data: any) => {
            expect(data).toBe(1);
        });

        const req = httpMock.expectOne(
            AppConstants.baseURL + `User/1`,
            'delete to api'
        );
        expect(req.request.method).toBe('DELETE');

        req.flush(1);

        httpMock.verify();
    });

});