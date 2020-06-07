import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { retry, catchError } from 'rxjs/operators';
import {of, Observable} from 'rxjs'


@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor( private httpclient:HttpClient ) { }

  url:string = 'http://localhost:50469/api/customer'; //Integration Point

  buildHeaders(){
    let reqHeaders = new HttpHeaders();

    reqHeaders.set('Cache-Control', 'no-cache');
    reqHeaders.set('Pragma', 'no-cache');
    reqHeaders.set('Content-Type', 'application/json');

    return reqHeaders;
  }

  //Get All Employees
  GetEmployees( ):Observable<any>
  {

    let headers = this.buildHeaders();
    let req = this.httpclient.get(this.url, {headers} );

    //Return our data pipe to component for execution
    return req.pipe( 
                retry(3), 
                catchError( err=>of([])) 
              );
  }

  //DELETE
  Delete( id:number ):Observable<any>{

    let headers = this.buildHeaders();

    return this.httpclient.delete( 
      `${this.url}/${id}`, 
      {headers}
    ).pipe( 
      retry(3), 
      catchError( err=>of([])) 
    );

  }

  //POST
  Post( data ):Observable<any>{

    let headers = this.buildHeaders();

    return this.httpclient.post( 
      this.url, 
      data,
      {headers:headers}
    ).pipe(  
      catchError( err=>of([])) 
    );

  }

  //UPDATE
  Update( id:number, data:object):Observable<any>{

    let headers = this.buildHeaders();

    return this.httpclient.put(
      `${this.url}/${id}`, 
      data,
      {headers}
    ).pipe( 
      retry(3), 
      catchError( err=>of([])) 
    );

  }
}


