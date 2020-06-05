import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { CustomerService } from '../customer.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  employees = [];
  columns = ["First Name", "Last Name", "Email", "Gender", "Actions" ]

  constructor( private custService: CustomerService ) { }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees()
  {

    this.custService.GetEmployees().subscribe(res=>{
      this.employees = res;
    })
  }

  update( emp ):void {
    console.log(emp);
  }

  delete( emp ):void {

    //Delete value via service
    this.custService.Delete( emp.id ).subscribe( res => {

      //Delete element in the array
      this.employees.forEach((element, i) => {
        if (element.id == emp.id){
          this.employees.splice(i, 1);
        }
      });

    });
  }
}
