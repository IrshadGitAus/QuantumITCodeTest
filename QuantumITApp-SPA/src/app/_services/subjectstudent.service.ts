import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SubjectStudent } from '../_models/SubjectStudent';

@Injectable({
  providedIn: 'root'
})
export class SubjectStudentService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSubjectStudents(subjectId: number): Observable<SubjectStudent> {
    return this.http.get<SubjectStudent>(this.baseUrl + 'subjects/' + subjectId + '/students');
  }

}
