import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Student } from '../_models/Student';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addStudent(student: Student): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + 'students', student);
  }

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseUrl + 'students');
  }

  getStudent(id: number): Observable<Student> {
    return this.http.get<Student>(this.baseUrl + 'students/' + id);
  }

  updateStudent(id: number, student: Student): Observable<Student> {
    return this.http.put<Student>(this.baseUrl + 'students/' + id, student);
  }

  deleteStudent(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.baseUrl + 'students/' + id);
  }

}
