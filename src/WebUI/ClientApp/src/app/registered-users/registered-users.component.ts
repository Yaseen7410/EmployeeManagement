import { Component, OnInit } from '@angular/core';
import { GridQuery, RegisterClient, RegisterDTO } from '../SoftMash-Api';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-registered-users',
  templateUrl: './registered-users.component.html',
  styleUrls: ['./registered-users.component.scss'],
})
export class RegisteredUsersComponent implements OnInit {
  dataSource = new MatTableDataSource<RegisterDTO>();
  constructor(private get: RegisterClient) {}

  ngOnInit(): void {
    this.getAll();
  }
  query: GridQuery = <GridQuery>{
    page: 1,
    pageSize: 20,
    filter: {},
    ascending: true,
    sort: 'CreatedOn',
  };
  displayedColumns: string[] = ['name', 'email', 'address', 'phoneNo','Actions'];
  getAll(): void {
    this.get.registerQuery(this.query).subscribe((response) => {
      debugger;
      if (response.data) {
        console.log(response);
        this.dataSource.data = response.data;
      } else {
        this.dataSource.data = [];
      }
      console.log(this.dataSource.data);
    });
  }
}
