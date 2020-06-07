import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  //Add Customer Form Properties
  createCustomerForm: FormGroup;
  subimitted:boolean = false;

  //Customer Display Properties
  employees = [];
  columns = ["First Name", "Last Name", "Email", "Actions" ]

  constructor( private custService: CustomerService, private formBuilder:FormBuilder ) { }

  ngOnInit(): void {
    //Load our application
    this.getEmployees();
  
    this.createCustomerForm = this.formBuilder.group({
      firstName:  ['', Validators.required],
      lastName:   ['', Validators.required],
      email:      ['', [Validators.email, Validators.required] ],
    })
  
  }

  getEmployees()
  {
    this.custService.GetEmployees().subscribe(res=>{
      this.employees = res;
    })
  }

  //Todo - Make it so new people are only added to the end of the list
  createCustomer():void{
    this.subimitted = true;

    if (this.createCustomerForm.invalid) return;

    let data = this.createCustomerForm.value;

    this.custService.Post( data ).subscribe( res =>{
      this.getEmployees();
    })


  }

  update( emp ):void {

    this.custService.Update( emp.id, emp ).subscribe(res => {
      console.log(emp);
    })
    
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
