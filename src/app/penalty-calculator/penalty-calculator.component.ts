import { Component, OnInit } from '@angular/core';
import { Country } from '../models/country';
import { GetDataService } from '../services/get-data.service';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Validators } from '@angular/forms';
import { Test } from '../models/test';
import { Penalty } from '../models/penalty';

@Component({
  selector: 'app-penalty-calculator',
  templateUrl: './penalty-calculator.component.html',
  styleUrls: ['./penalty-calculator.component.css']
})
export class PenaltyCalculatorComponent implements OnInit {

  countries: Country[] = [];
  selectedCountry: Country = {} as Country;
  penaltyForm!: FormGroup;
  test: Test ={} as Test;
  recievedPenalty: Penalty ={} as Penalty;

  constructor(private service:GetDataService, private formBuilder:FormBuilder) {
    this.penaltyForm = this.formBuilder.group({
      issueDate:['',[Validators.required]],
      returnDate:['',[Validators.required]],
      country:['',[Validators.required]]
    })
   }

  ngOnInit(): void {
    this.service.get().subscribe(data=>{
      console.log(data);
      this.countries = data;
    }, error=>{
      console.log(error)
    }
    )
  }
  // I am basicaly doing this step to store the value of dropdown into an object of class Country to access its attributes
  giveCountryId(){
    this.selectedCountry = this.penaltyForm.get("country")?.value;
    console.log(this.selectedCountry);
  }

  //Function called upon the click of calculate button
  calculatePenalty(){
    console.log("Hello World!")

    var issueDate  = this.penaltyForm.get("issueDate")?.value;
    var returnDate = this.penaltyForm.get("returnDate")?.value;
    var country = this.selectedCountry.countryID;

    this.test = {checkIn:issueDate, checkOut:returnDate, countryId: country} as Test;
    console.log(this.test);
    this.service.getPenaltyAmount(this.test).subscribe(data=>{
      console.log(data);
      this.recievedPenalty = data;
    })
    this.penaltyForm.reset();


  }

}
