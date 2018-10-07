import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
    name: 'filter'
})
export class ProjectFilterPipe implements PipeTransform {
    transform(items: any[], searchText: any): any[] {
        if (!items) return [];
        if (!searchText) return items;
        searchText = searchText.toLowerCase();
        return items.filter(it => {
            return ((it.Start_Date!= null && it.Start_Date == searchText)
                || (it.End_Date != null && it.End_Date == searchText)
                || it.ProjectName.toLowerCase().includes(searchText.toLowerCase())
                || (it.Priority.toString() == searchText)
                || (it.CompletedTaskCount == searchText));
        });
    }
}