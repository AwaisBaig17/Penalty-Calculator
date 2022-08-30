import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { InstructionsComponent } from './instructions/instructions.component';
import { PenaltyCalculatorComponent } from './penalty-calculator/penalty-calculator.component';

const routes: Routes = [
  {path: 'instruction', component: InstructionsComponent},
  {path: 'calculator', component: PenaltyCalculatorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [AppComponent,InstructionsComponent, PenaltyCalculatorComponent]
