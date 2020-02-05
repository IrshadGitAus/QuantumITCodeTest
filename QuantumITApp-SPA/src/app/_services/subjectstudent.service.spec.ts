/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SubjectstudentService } from './subjectstudent.service';

describe('Service: Subjectstudent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SubjectstudentService]
    });
  });

  it('should ...', inject([SubjectstudentService], (service: SubjectstudentService) => {
    expect(service).toBeTruthy();
  }));
});
