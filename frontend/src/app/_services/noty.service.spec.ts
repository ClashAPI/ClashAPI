import {TestBed} from '@angular/core/testing';

import {NotyService} from './noty.service';

describe('NotyService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NotyService = TestBed.get(NotyService);
    expect(service).toBeTruthy();
  });
});
