import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Subject } from '../_models/Subject';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addSubject(subject: Subject): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + 'subjects', subject);
  }

  getSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.baseUrl + 'subjects');
  }

  getSubject(id: number): Observable<Subject> {
    return this.http.get<Subject>(this.baseUrl + 'subjects/' + id);
  }

  updateSubject(id: number, subject: Subject): Observable<Subject> {
    return this.http.put<Subject>(this.baseUrl + 'subjects/' + id, subject);
  }

  deleteSubject(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.baseUrl + 'subjects/' + id);
  }

}
