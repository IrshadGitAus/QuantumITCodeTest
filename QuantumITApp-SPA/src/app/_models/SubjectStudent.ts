import { Student } from './Student';

export interface SubjectStudent {
    subjectid: number;
    subjectname: string;
    students: Student[];
}
