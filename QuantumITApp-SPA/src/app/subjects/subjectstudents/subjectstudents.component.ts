import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { SubjectStudent } from 'src/app/_models/SubjectStudent';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { SubjectStudentService } from 'src/app/_services/subjectstudent.service';
import { Subject } from 'src/app/_models/Subject';
import { Student } from 'src/app/_models/Student';
import { GridApi, ColumnApi } from 'ag-grid-community';
import { StudentService } from 'src/app/_services/student.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-subjectstudents',
  templateUrl: './subjectstudents.component.html',
  styleUrls: ['./subjectstudents.component.css']
})
export class SubjectStudentsComponent implements OnInit, OnChanges {

  @Input() subject: Subject;
  @Input() subjectSelected: Subject;

  subjectStudents: SubjectStudent;

  studentWeGotOnClick: Subject;

  // row data and column definitions
  rowData: Subject[];
  columnDefs: any[];

  private selectedRow;

  // gridApi and columnApi
  private api: GridApi;
  private columnApi: ColumnApi;

  // tslint:disable-next-line: max-line-length
  constructor(private subjectStudentService: SubjectStudentService, private studentService: StudentService, private alertify: AlertifyService) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit() {
    this.loadSubjectStudents();
  }

  // one grid initialisation, grap the APIs and auto resize the columns to fit the available space
  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;

    this.api.sizeColumnsToFit();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.subjectSelected) {
      this.loadSubjectStudents();
    }
  }

  loadSubjectStudents() {
    // tslint:disable-next-line: max-line-length
    this.subjectStudentService.getSubjectStudents(this.subjectSelected.id).subscribe((subjectStudents: SubjectStudent) => { this.subjectStudents = subjectStudents; }, error => this.alertify.error(error.error));
  }

  // create some simple column definitions
  private createColumnDefs() {
    return [
      { headerName: 'Student First Name', field: 'firstName', editable: true },
      { headerName: 'Student Last Name', field: 'surName', editable: true },
      { headerName: 'Age', field: 'age', editable: true },
      { headerName: 'GPA', field: 'gpa', editable: true }
    ];
  }

  putStudentSelected(event) {
    if (event.node.selected) {
      this.studentWeGotOnClick = event.node.data;
      this.selectedRow = event.node;
    }
  }

  editStudentRow() {

    // tslint:disable-next-line: triple-equals
    if (this.api.getSelectedRows().length == 0) {
      this.alertify.error('Please select a Student.');
      return;
    }

    this.api.startEditingCell({
      rowIndex: this.selectedRow.rowIndex,
      colKey: 'age'
    });

  }

  updateStudent(params) {
    const row = this.api.getSelectedRows();
    const studentToEdit: any = {
      subjectId: this.subjectSelected.id,
      firstName: row[0].firstName,
      surName: row[0].surName,
      age: row[0].age,
      gpa: row[0].gpa
    };

    this.studentService.updateStudent(this.studentWeGotOnClick.id, studentToEdit)
      .subscribe(data => {
        this.alertify.success('Student updated successfully!');
      }, error => { this.alertify.error(error.error); this.api.undoCellEditing(); });
  }

  getRowStyleGPA(params) {
     if (params.data.gpa > 3.2) {
      return {
        // tslint:disable-next-line: semicolon
        'background-color': 'sandybrown !important'
      // tslint:disable-next-line: semicolon
      }
    }
  }

  deleteStudent(params) {

    // tslint:disable-next-line: triple-equals
    if (this.api.getSelectedRows().length == 0) {
      this.alertify.error('Please select a Student.');
      return;
    }

    const row = this.api.getSelectedRows();

    this.studentService.deleteStudent(row[0].id)
      .subscribe(data => {
        this.ngOnInit();
        this.api.refreshRows(null);
        this.alertify.success('Student deleted successfully!');
      });

  }

}
