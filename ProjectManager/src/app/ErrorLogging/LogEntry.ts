export class LogEntry {
    // Public Properties
    entryDate: string = new Date().toUTCString();
    message: string = "";
    //level: LogLevel = LogLevel.Debug;
    extraInfo: any[] = [];
    stack: string = "";
    type: string;
}