import { ErrorFieldValidationPipe } from './error-field-validation.pipe';

describe('ErrorFieldValidationPipe', () => {
  it('create an instance', () => {
    const pipe = new ErrorFieldValidationPipe();
    expect(pipe).toBeTruthy();
  });
});
