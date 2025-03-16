import { Component, Input } from '@angular/core';
import { ValidationMessages } from '../../../interfaces/input-validation-text';
import { AbstractControl, FormControl, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ErrorFieldValidationPipe } from '../../pipes/error-field-validation.pipe';

@Component({
    selector: 'app-error-text',
    imports: [CommonModule, ReactiveFormsModule, ErrorFieldValidationPipe],
    templateUrl: './error-text.component.html',
    styleUrl: './error-text.component.scss'
})
export class ErrorTextComponent {
    @Input() control!: FormControl | AbstractControl;

    @Input() errorMessages!: ValidationMessages;

    constructor(public formDirective: FormGroupDirective) {}
}
