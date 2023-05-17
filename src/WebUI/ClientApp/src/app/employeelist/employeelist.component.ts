import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { EmpDTO, EmployeeClient, GridQuery } from '../SoftMash-Api';

@Component({
  selector: 'app-employeelist',
  templateUrl: './employeelist.component.html',
  styleUrls: ['./employeelist.component.scss'],
})
export class EmployeelistComponent implements OnInit {
  empdto: EmpDTO[] = [];
  dataSource = new MatTableDataSource<EmpDTO>();
  constructor(private emp: EmployeeClient) {}

  displayedColumns: string[] = ['name', 'address', 'phone', 'department'];
  ngOnInit(): void {
    this.getData();
  }

  query: GridQuery = <GridQuery>{
    page: 1,
    pageSize: 10,
    filter: {},
    ascending: true,
    sort: 'CreatedOn',
  };

  getData(): void {
    debugger;
    this.emp.employeesQuery(this.query).subscribe((response) => {
      if (response.data) {
        this.dataSource.data = response.data;
      } else {
        this.dataSource.data = [];
      }
      console.log(this.dataSource.data);
    });
  }
  addEmployee() {}
  editEmployee(emp: any) {}
  deleteEmployee(id: number) {}
}
