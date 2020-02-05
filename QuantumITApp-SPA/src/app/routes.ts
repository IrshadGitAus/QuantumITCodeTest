import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SubjectListComponent } from './subjects/subject-list/subject-list.component';
import { SubjectAddComponent } from './subjects/subject-add/subject-add.component';
import { StudentAddComponent } from './students/student-add/student-add.component';

export const appRoutes: Routes = [
    { path: '', redirectTo: '/subjects', pathMatch: 'full' },
    {path: 'subjects/add', component: SubjectAddComponent},
    {path: 'addstudent/:id', component: StudentAddComponent},
    {
        path: 'subjects', component: SubjectListComponent,
        runGuardsAndResolvers: 'always',
    },

    { path: '**', redirectTo: '/subjects', pathMatch: 'full' }

];
