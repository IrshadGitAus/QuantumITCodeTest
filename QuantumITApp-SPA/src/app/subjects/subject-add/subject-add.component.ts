import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { SubjectService } from 'src/app/_services/subject.service';
import { Subject } from 'src/app/_models/Subject';
import { Router } from '@angular/router';


@Component({
  selector: 'app-subject-add',
  templateUrl: './subject-add.component.html',
  styleUrls: ['./subject-add.component.css']
})
export class SubjectAddComponent implements OnInit {

  submitted = false;

  userForm: FormGroup;

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'name': ['', Validators.required],
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'location': ['', Validators.required],
      // tslint:disable-next-line: quotemark
      // tslint:disable-next-line: object-literal-key-quotes
      'teacherName': ['', Validators.required]
    });

  }

  // tslint:disable-next-line: max-line-length
  constructor(private formBuilder: FormBuilder, private subjectService: SubjectService, private alertify: AlertifyService, private router: Router) { }

  onSubmit() {
    this.submitted = true;
    if (this.userForm.invalid) {
      return;
    }
    console.log('Subject Object: ' + this.userForm.value);

    this.addSubject(this.userForm.value);

  }

  addSubject(subject: Subject) {
    this.subjectService.addSubject(subject).subscribe(next => {
      this.alertify.success('Subject added successfully!');
      this.userForm.reset(subject);
      this.router.navigate(['subjects']);
    }, error => {
      this.alertify.error(error.error);
    });
  }


}
