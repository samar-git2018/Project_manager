import { Pipe, PipeTransform } from '@angular/core';
@Pipe({
    name: 'filter'
})
export class UserFilterPipe implements PipeTransform {
    transform(items: any[], searchText: string): any[] {
        if (!items) return [];
        if (!searchText) return items;
        searchText = searchText.toLowerCase();
        return items.filter(it => {
            return (it.First_Name.toLowerCase().includes(searchText.toLowerCase())
                || it.Last_Name.toLowerCase().includes(searchText.toLowerCase())
                || it.Employee_ID.toLowerCase().includes(searchText.toLowerCase()));
        });
    }
}