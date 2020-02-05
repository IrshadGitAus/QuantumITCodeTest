import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Subject } from 'src/app/_models/Subject';

@Component({
  // tslint:disable-next-line: component-selector
  selector: '[app-subject]',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {

  @Input() subject: Subject;
  @Input() index: number;
  @Output() subjectClicked = new EventEmitter();

  constructor() { }

  ngOnInit() {

  }

  emitStudent() {
    this.subjectClicked.emit(this.subject);
  }


}
