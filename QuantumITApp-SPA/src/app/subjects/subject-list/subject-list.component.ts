import { Component, OnInit } from '@angular/core';
import { Subject } from 'src/app/_models/Subject';
import { SubjectService } from 'src/app/_services/subject.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { SubjectComponent } from '../subject/subject.component';
import { GridApi, ColumnApi } from 'ag-grid-community';

@Component({
  selector: 'app-subject-list',
  templateUrl: './subject-list.component.html',
  styleUrls: ['./subject-list.component.css']
})
export class SubjectListComponent implements OnInit {

  subjects: Subject[];

  subjectWeGotOnClick: Subject; // this is sent from child (Student) to Parent (StudentList)

  subjectToUpdate: Subject;

  // row data and column definitions
  rowData: Subject[];
  columnDefs: any[];

  // gridApi and columnApi
  private api: GridApi;
  private columnApi: ColumnApi;

  private selectedRow;

  constructor(private subjectService: SubjectService, private alertify: AlertifyService) {
    this.columnDefs = this.createColumnDefs();
  }

  ngOnInit() {
    this.loadSubjects();
  }

  // one grid initialisation, grap the APIs and auto resize the columns to fit the available space
  onGridReady(params): void {
    this.api = params.api;
    this.columnApi = params.columnApi;

    this.api.sizeColumnsToFit();
  }

  loadSubjects() {
    // tslint:disable-next-line: max-line-length
    this.subjectService.getSubjects().subscribe((subjects: Subject[]) => { this.subjects = subjects; }, error => this.alertify.error(error.error));
  }

  putSubjectSelected(event) {
    if (event.node.selected) {
      this.subjectWeGotOnClick = event.node.data;
      this.selectedRow = event.node;
    }

    // console.log(subject);
    // this.subjectWeGotOnClick = subject;
  }

  // create some simple column definitions
  private createColumnDefs() {
    return [
      { headerName: 'Class Name', field: 'name', editable: true, sortable: true },
      { headerName: 'Location', field: 'location', editable: true },
      { headerName: 'Teacher Name', field: 'teacherName', editable: true }
    ];
  }

  editSubjectRow() {

    // tslint:disable-next-line: triple-equals
    if (this.api.getSelectedRows().length == 0) {
      this.alertify.error('Please select a Class.');
      return;
    }

    this.api.startEditingCell({
      rowIndex: this.selectedRow.rowIndex,
      colKey: 'name'
    });

  }

  updateSubject(params) {
    const row = this.api.getSelectedRows();
    this.subjectService.updateSubject(this.subjectWeGotOnClick.id, row[0])
      .subscribe(data => {
        this.alertify.success('Class updated successfully!');
      }, error => {this.alertify.error(error.error); this.api.undoCellEditing(); });
  }

  deleteSubject(params) {

    // tslint:disable-next-line: triple-equals
    if (this.api.getSelectedRows().length == 0) {
      this.alertify.error('Please select a Class.');
      return;
    }


    const row = this.api.getSelectedRows();

    this.subjectService.deleteSubject(row[0].id)
      .subscribe(data => {
        this.ngOnInit();
        this.api.refreshRows(null);
        this.alertify.success('Class deleted successfully!');
      });

  }

}
