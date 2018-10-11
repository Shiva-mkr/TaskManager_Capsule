import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { HttpModule,Http } from '@angular/http';
import { AngularDateTimePickerModule } from 'angular2-datetimepicker';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,FormsModule,HttpModule,AngularDateTimePickerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
