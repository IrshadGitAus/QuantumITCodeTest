import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {BsDropdownModule} from 'ngx-bootstrap';
import { AgGridModule } from 'ag-grid-angular';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { SubjectListComponent } from './subjects/subject-list/subject-list.component';
import { AlertifyService } from './_services/alertify.service';
import { SubjectService } from './_services/subject.service';
import { SubjectComponent } from './subjects/subject/subject.component';
import { StudentComponent } from './students/student/student.component';
import { StudentListComponent } from './students/student-list/student-list.component';
import { StudentEditComponent } from './students/student-edit/student-edit.component';
import { StudentService } from './_services/student.service';
import {SubjectStudentService } from './_services/subjectstudent.service';
import { SubjectStudentsComponent } from './subjects/subjectstudents/subjectstudents.component';
import { appRoutes } from './routes';
import { HeaderComponent } from './header/header.component';
import { TestComponent } from './test/test.component';
import { SubjectAddComponent } from './subjects/subject-add/subject-add.component';
import { StudentAddComponent } from './students/student-add/student-add.component';


@NgModule({
   declarations: [
      AppComponent,
      SubjectListComponent,
      SubjectComponent,
      StudentComponent,
      StudentListComponent,
      StudentEditComponent,
      SubjectStudentsComponent,
      HeaderComponent,
      TestComponent,
      SubjectAddComponent,
      StudentAddComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      AgGridModule.withComponents([])
   ],
   providers: [
      AlertifyService,
      SubjectService,
      StudentService,
      SubjectStudentService,
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
