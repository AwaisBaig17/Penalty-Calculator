import { Injectable } from '@angular/core';
import { Country } from '../models/country';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Test } from '../models/test';
import { Penalty } from '../models/penalty';

@Injectable({
  providedIn: 'root'
})
export class GetDataService {

  constructor(private http:HttpClient) { }
  get():Observable<Country[]>{
    return this.http.get<Country[]>('http://localhost/MyAPI/GetCountries');
  }
  getPenaltyAmount(test:Test):Observable<Penalty>{
    return this.http.post<Penalty>('http://localhost/finalAPI/Calculate',test);
  }
}
