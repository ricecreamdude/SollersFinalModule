import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { CustomerService } from '../customer.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  employees = [{
    firstName: "Joshua",
    lastName: "Ho",
    email: "ho.joshua4@gmail.com",
    gender: "Male"
  }];
  columns = ["First Name", "Last Name", "Email", "Gender", "Actions" ]

  constructor( private custService: CustomerService ) { }

  ngOnInit(): void {
  }

  getEmployees()
  {
    this.custService.getCustomers().subscribe(res=>{

      let dummyData = [{
        firstName: "Joshua",
        lastName: "Ho",
        email: "ho.joshua4@gmail.com",
        gender: "Male"
      }];

      //Fetch the information and view it
      console.log(res);

      this.employees = dummyData;

    })
  }

  update( emp ):void {
    console.log(emp);
  }

  delete( emp ):void {
    console.log(emp);
  }

}
