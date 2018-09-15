import { Injectable, ErrorHandler } from '@angular/core';
import { ErrorLoggService } from 'app/Service/error-log.service';

// Global error handler for logging errors
@Injectable()
export class GlobalErrorHandler extends ErrorHandler {
    constructor(private errorLogService: ErrorLoggService) {
        //Angular provides a hook for centralized exception handling.
        //constructor ErrorHandler(): ErrorHandler
        super();
    }

    handleError(error): void {
        //debugger;
        this.errorLogService.logError(error);
        throw error;
    }
}