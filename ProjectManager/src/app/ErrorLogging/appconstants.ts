﻿export class AppConstants {
    //public static get baseURL(): string { return 'http://localhost:51052/Api/'; }
    public static get baseURL(): string { return 'http://localhost/ProjectManagerWebService/Api/'; }
    
    public static get httpError(): string { return 'There was an HTTP error.'; }
    public static get typeError(): string { return 'There was a Type error.'; }
    public static get generalError(): string { return 'There was a general error.'; }
    public static get somethingHappened(): string { return 'Nobody threw an Error but something happened!'; }
}