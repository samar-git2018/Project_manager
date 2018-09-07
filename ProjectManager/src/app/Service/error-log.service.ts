import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AppConstants } from 'app/Errorlogging/appconstants';
import { LogEntry } from 'app/Errorlogging/logentry';

//#region Handle Errors Service
@Injectable()
export class ErrorLoggService {
    location: string;
    constructor() {
        this.location = "logging";
    }

    //Log error method
    logError(error: any) {
        let entry: LogEntry = new LogEntry();
        //Returns a date converted to a string using Universal Coordinated Time (UTC).
        const date = new Date().toUTCString();
        //debugger;
        if (error instanceof HttpErrorResponse) {
            //The response body may contain clues as to what went wrong,
            entry.entryDate = date;
            entry.message = error.message;
            entry.type = AppConstants.httpError;
            console.error(date, AppConstants.httpError, error.message, 'Status code:',
                (<HttpErrorResponse>error).status);
            this.storeLog(entry);
        }
        else if (error instanceof TypeError) {
            entry.entryDate = date;
            entry.message = error.message;
            entry.stack = error.stack;
            entry.type = AppConstants.typeError;
            this.storeLog(entry);
            console.error(date, AppConstants.typeError, error.message, error.stack);
        }
        else if (error instanceof Error) {
            console.error(date, AppConstants.generalError, error.message, error.stack);
        }
        else if (error instanceof ErrorEvent) {
            //A client-side or network error occurred. Handle it accordingly.
            entry.entryDate = date;
            entry.message = error.message;
            entry.type = AppConstants.generalError;
            this.storeLog(entry);
            console.error(date, AppConstants.generalError, error.message);
        }
        else {
            entry.entryDate = date;
            entry.message = error.message;
            entry.stack = error.stack;
            entry.type = AppConstants.somethingHappened;
            this.storeLog(entry);
            console.error(date, AppConstants.somethingHappened, error.message, error.stack);
        }
    }
    storeLog(entry: LogEntry) {
        let values: LogEntry[];
        try {
            // Get previous values from local storage
            values = JSON.parse(
                localStorage.getItem(this.location))
                || [];
            // Add new log entry to array
            values.push(entry);
            // Store array into local storage
            localStorage.setItem(this.location,
                JSON.stringify(values));

        } catch (ex) {
            // Display error in console
            console.log(ex);
        }
    }

}