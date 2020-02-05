import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { StudentService } from 'src/app/_services/student.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Student } from 'src/app/_models/Student';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectService } from 'src/app/_services/subject.service';
import { Subject } from 'src/app/_models/Subject';

@Component({
  selector: 'app-student-add',
  templateUrl: './student-add.component.html',
  styleUrls: ['./student-add.component.css']
})
export class StudentAddComponent implements OnInit {

  submitted = false;

  userForm: FormGroup;

  subjectId: number;

  subject: Subject;

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'subjectId': ['', Validators.required],
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'firstName': ['', Validators.required],
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'surName': ['', Validators.required],
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'age': ['', Validators.required],
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'gpa': ['', Validators.required]
    });

    this.subjectId = this.route.snapshot.params.id;

    // tslint:disable-next-line: max-line-length
    this.subjectService.getSubject(this.subjectId).subscribe((subject: Subject) => { this.subject = subject; }, error => this.alertify.error(error.error));

  }

  // tslint:disable-next-line: max-line-length
  constructor(private formBuilder: FormBuilder, private studentService: StudentService, private alertify: AlertifyService, private route: ActivatedRoute, private subjectService: SubjectService, private router: Router) {

  }

   onSubmit() {
    this.submitted = true;

    // tslint:disable-next-line: no-string-literal
    this.userForm.controls['subjectId'].setValue(this.subjectId);

    if (this.userForm.invalid) {
      console.log('invalid form');
      return;
    }
    this.addStudent(this.userForm.value);

  }

  addStudent(student: Student) {
    this.studentService.addStudent(student).subscribe((res: boolean) => {
      if (res) {
        this.alertify.success('Student added successfully.');
        this.router.navigate(['subjects']);
      } else {
        this.alertify.error('A student with this Last Name already exists in this class!!.');
      }
    }, error => this.alertify.error(error.error));
  }
}
