import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'errorFieldValidation',
    standalone: true
})
export class ErrorFieldValidationPipe implements PipeTransform {
    transform(value: any, errorObject: Object): unknown {
        return value[Object.keys(errorObject)[0]];
    }
}
