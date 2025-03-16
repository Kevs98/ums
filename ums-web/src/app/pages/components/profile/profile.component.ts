import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { AvatarModule } from 'primeng/avatar';
import { TooltipModule } from 'primeng/tooltip';
import { User } from '../../../core/interfaces/user.interface';
import { ButtonModule } from 'primeng/button';
import { UserService } from '../../../core/services/user.service';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DropdownModule } from 'primeng/dropdown';
import { genderEnum } from '../../../core/constants/enums';
import { SelectModule } from 'primeng/select';

@Component({
    selector: 'app-profile',
    imports: [CommonModule, ReactiveFormsModule, CardModule, FloatLabelModule, InputTextModule, AvatarModule, TooltipModule, ButtonModule, ToastModule, ConfirmDialogModule, DatePickerModule, SelectModule],
    templateUrl: './profile.component.html',
    styleUrl: './profile.component.scss',
    providers: [MessageService, ConfirmationService]
})
export class ProfileComponent implements OnInit {
    profileForm!: FormGroup;
    user!: User;
    selectedImage: string = '';
    genders: genderEnum[] = [genderEnum.Male, genderEnum.Female, genderEnum.Other];

    constructor(
        private _formBuilder: FormBuilder,
        private userService: UserService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {
        this.profileForm = _formBuilder.group({
            name: [{ disabled: false }],
            last_name: [{ disabled: false }],
            username: [{ disabled: true }],
            email: [{ disabled: true }],
            birthDate: [{ disabled: false }],
            role: [{ disabled: true }],
            zone: [{ disabled: true }],
            gender: [{ disabled: false }],
            image: [{ disabled: false }]
        });
    }

    ngOnInit(): void {
        this.user = JSON.parse(localStorage.getItem('user')!);

        if (this.user.birthDate && typeof this.user.birthDate === 'string') {
            const birthDateStr: string = this.user.birthDate as string;
            const [year, month, day] = birthDateStr.split('-').map(Number);
            this.user.birthDate = new Date(year, month - 1, day);
        }

        this.profileForm.patchValue(this.user);

        Object.keys(this.profileForm.controls).forEach((key) => {
            if (!this.profileForm.get(key)?.value) {
                this.profileForm.get(key)?.setValue('--');
            }
        });
        this.profileForm.disable();

        const fieldsToEnable = ['name', 'last_name', 'birthDate', 'image', 'gender'];

        fieldsToEnable.forEach((field) => {
            this.profileForm.get(field)?.enable();
        });
    }

    triggerFileInput(): void {
        const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
        fileInput.click();
    }

    onFileSelected(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files[0]) {
            const file = input.files[0];
            const reader = new FileReader();

            reader.onload = () => {
                this.user.image = reader.result as string;
                this.selectedImage = reader.result as string;
            };

            reader.readAsDataURL(file);
        }
    }

    saveProfile() {
        this.confirmationService.confirm({
            header: 'Guardar Perfil',
            message: '¿Estás seguro de guardar los cambios?',
            icon: 'fas fa-exclamation-triangle',
            acceptLabel: 'Aprobar',
            rejectLabel: 'Cancelar',
            accept: () => {
                const updatedUser: User = {
                    id: this.user.id,
                    name: this.profileForm.get('name')?.value,
                    last_name: this.profileForm.get('last_name')?.value,
                    email: this.profileForm.get('email')?.value,
                    image: this.selectedImage ? this.selectedImage : this.user.image,
                    username: this.user.username,
                    password: this.user.password,
                    birthDate: this.profileForm.get('birthDate')?.value.toISOString().split('T')[0],
                    gender: this.profileForm.get('gender')?.value
                };

                this.userService.updateUser(updatedUser).subscribe((res) => {
                    if (res) {
                        this.messageService.add({ severity: 'success', summary: 'Perfil actualizado', detail: 'El perfil ha sido actualizado.' });
                        const userLocal = {
                            ...this.user,
                            ...updatedUser
                        };
                        console.log('userLocal', userLocal);
                        localStorage.setItem('user', JSON.stringify(userLocal));
                        this.user = JSON.parse(localStorage.getItem('user')!);

                        if (this.user.birthDate && typeof this.user.birthDate === 'string') {
                            const birthDateStr: string = this.user.birthDate as string;
                            const [year, month, day] = birthDateStr.split('-').map(Number);
                            this.user.birthDate = new Date(year, month - 1, day);
                        }
                        this.profileForm.patchValue(this.user);
                    }
                });
            },
            reject: () => {}
        });
    }
}
