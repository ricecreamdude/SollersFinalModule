import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';
import {of, Observable} from 'rxjs'


@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor( private httpclient:HttpClient ) { }


  getCustomers( ):Observable<any>
  {
    let url='http://localhost:50469/api/customer';

    let reqHeaders = new HttpHeaders();

      reqHeaders.set('Cache-Control', 'no-cache')
      reqHeaders.set('Pragma', 'no-cache')
      reqHeaders.set('Content-Type', 'application/json');

      
    return this.httpclient.get(
        url,
        {headers:reqHeaders}
    ).pipe( 
      retry(3),
      catchError( err=>of([]) ) 
    );
  }

}


