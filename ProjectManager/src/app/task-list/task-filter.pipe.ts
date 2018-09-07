import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
    name: 'filter'
})
export class TaskFilterPipe implements PipeTransform {
    transform(items: any[], searchText: string): any[] {
        if (!items) return [];
        if (!searchText) return items;
        return items.filter(it => {
            if (Number(searchText))
            { return ((it.Project_ID != null && it.Project_ID == searchText)); }
            else {
                searchText = searchText.toLowerCase();
                return ((it.First_Name != null && it.First_Name.toLowerCase().includes(searchText.toLowerCase()))
                    || (it.Last_Name != null && it.Last_Name.toLowerCase().includes(searchText.toLowerCase()))
                    || (it.ProjectName != null && it.ProjectName.toLowerCase().includes(searchText.toLowerCase()))
                    || (it.Parent_Task != null && it.Parent_Task.toLowerCase().includes(searchText.toLowerCase())));
            }
        });
    }
}