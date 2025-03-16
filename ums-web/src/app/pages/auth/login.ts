import { Component, ElementRef, signal, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { AppFloatingConfigurator } from '../../layout/component/app.floatingconfigurator';
import { User } from '../../core/interfaces/user.interface';
import { AuthService } from '../../core/services/auth.service';
import { UserService } from '../../core/services/user.service';
import { catchError, tap, throwError } from 'rxjs';
import { LoginRequest } from '../../core/interfaces/login.interface';
import * as QRCode from 'qrcode';
import { DialogModule } from 'primeng/dialog';
import { InputOtpModule } from 'primeng/inputotp';
import { InputValidationsText } from '../../core/interfaces/input-validation-text';
import { ErrorTextComponent } from '../../core/utils/components/error-text/error-text.component';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [ButtonModule, ErrorTextComponent, CheckboxModule, InputTextModule, PasswordModule, FormsModule, RouterModule, RippleModule, DialogModule, InputOtpModule, CommonModule, FormsModule, ReactiveFormsModule],
    styles: [
        `
            :host ::ng-deep .pi-eye,
            :host ::ng-deep .pi-eye-slash {
                transform: scale(1.6);
                margin-right: 1rem;
                color: var(--primary-color) !important;
            }

            canvas {
                max-width: 100%;
                height: auto;
                display: block;
            }
        `
    ],
    template: `
        <div class="surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden">
            <div class="flex flex-column align-items-center justify-content-center">
                <div
                    style="
        border-radius: 56px;
        padding: 0.3rem;
        background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%);
      "
                >
                    <div class="w-full surface-card py-8 px-5 sm:px-8" style="border-radius: 53px">
                        <div class="text-center mb-5">
                            <img src="https://cdn.dribbble.com/userupload/9965380/file/original-da598743381778180d5b0c1db32f7bce.jpg?format=webp&resize=400x300&vertical=center" alt="UMS logo" class="mb-5 w-15rem flex-shrink-0" />
                            <div class="text-900 text-3xl font-medium mb-3">User Management System</div>
                            <span class="text-600 font-medium">Inicio de sesión</span>
                        </div>

                        <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
                            <div class="mb-5">
                                <div class="flex flex-column mb-5">
                                    <label for="username" class="block text-900 text-xl font-medium mb-2">Usuario</label>
                                    <input id="username" formControlName="username" type="text" placeholder="Usuario" pInputText class="w-full md:w-30rem mb-2" style="padding: 1rem" />

                                    <app-error-text [control]="loginForm.get('username')!" [errorMessages]="validationMessages['username']"> </app-error-text>
                                </div>
                                <div class="flex flex-column mb-3">
                                    <label for="password" class="block text-900 font-medium text-xl mb-2">Contraseña</label>
                                    <p-password id="password" formControlName="password" placeholder="Contraseña" [toggleMask]="true" [feedback]="false" styleClass="mb-2 ng-invalid ng-dirty" inputStyleClass="w-full p-3 md:w-30rem"></p-password>
                                    <app-error-text [control]="loginForm.get('password')!" [errorMessages]="validationMessages['password']" />
                                </div>
                            </div>
                            <button
                                pButton
                                type="submit"
                                pRipple
                                [label]="isLoading() ? 'Iniciando sesión...' : 'Iniciar sesión'"
                                [icon]="isLoading() ? 'pi pi-spin pi-spinner' : 'pi pi-sign-in'"
                                [disabled]="loginForm.invalid"
                                iconPos="right"
                                aria-label="Iniciar sesión"
                                class="w-full p-3 text-xl"
                            ></button>
                        </form>
                    </div>
                </div>
            </div>
        </div>

        <p-dialog header="Verificación de código" [(visible)]="otpQrDialogVisible" [modal]="true" [style]="{ width: '60vw' }" [breakpoints]="{ '960px': '75vw', '640px': '90vw' }" [draggable]="false" [closable]="false">
            <div class="flex flex-col justify-center items-center">
                <div class="flex justify-center items-center">
                    <canvas #qrCodeCanvas width="200" height="200"></canvas>
                    <div class="text-center text-xl">
                        Escanee el código QR con su aplicación de autenticación: <br />
                        <strong>{{ otpSecret }}</strong>
                        <div class="flex justify-content-center">
                            <p-inputotp [(ngModel)]="otpCode" [integerOnly]="true" [length]="6" styleClass="mt-3" />
                        </div>
                        <p-button label="Verificar" (click)="verifyOtp()" icon="fas fa-check-double" styleClass="mt-4" />
                    </div>
                </div>
            </div>
        </p-dialog>

        <p-dialog header="Verificación de código" [(visible)]="otpDialogVisible" [modal]="true" [style]="{ width: '30vw' }" [breakpoints]="{ '960px': '50vw', '640px': '75vw' }" [draggable]="false" [closable]="false">
            <div class="text-center">
                <p>Introduzca el código de verificación:</p>
                <div class="flex justify-content-center">
                    <p-inputotp [(ngModel)]="otpCode" [integerOnly]="true" [length]="6" />
                </div>
                <p-button label="Verificar" (click)="verifyOtp()" icon="fas fa-check-double" styleClass="mt-4" />
            </div>
        </p-dialog>
    `
})
export class Login {
    username: string = '';
    password: string = '';
    valCheck: string[] = ['remember'];
    isLoading = signal<boolean>(false);
    otpSecret: string | null = null;
    otpDialogVisible: boolean = false;
    otpQrDialogVisible: boolean = false;
    otpCode: number | undefined;
    userLogged: User | undefined;

    @ViewChild('qrCodeCanvas') qrCodeCanvas!: ElementRef;

    loginForm: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        private _authService: AuthService,
        private _router: Router,
        private _userService: UserService
    ) {
        this.loginForm = this._formBuilder.group({
            username: ['', [Validators.required]],
            password: ['', [Validators.required, Validators.minLength(4)]]
        });
    }

    validationMessages: InputValidationsText = {
        username: {
            required: 'El nombre de usuario es requerido'
        },
        password: {
            required: 'La contraseña es requerida',
            minlength: 'La contraseña debe tener al menos 4 caracteres'
        }
    };

    login(): void {
        this.loginForm.markAllAsTouched();

        if (this.loginForm.invalid || this.isLoading()) {
            return;
        }

        const username = this.loginForm.get('username')?.value as string;
        const password = this.loginForm.get('password')?.value as string;

        const loginRequest: LoginRequest = {
            username,
            password
        };

        this.isLoading.set(true);
        this._authService
            .login(loginRequest)
            .pipe(
                tap((response) => {
                    if (response) {
                        this._userService.getUser(username, password).subscribe((response) => {
                            if (response) {
                                localStorage.setItem('user', JSON.stringify(response));
                            }
                        });

                        setTimeout(() => {
                            this.userLogged = JSON.parse(localStorage.getItem('user')!);
                            console.log('user after', this.userLogged);

                            if (!this.userLogged?.otpSecret) {
                                this.userLogged!.otpSecret = response.otpSecret;
                                this.otpSecret = response.otpSecret;
                                this.otpQrDialogVisible = true;
                                this.generateQrCode(this.otpSecret!);
                            } else {
                                this.otpDialogVisible = true;
                            }
                        }, 500);
                    }
                }),
                catchError((error) => {
                    console.error(error);
                    this.isLoading.set(false);
                    return throwError(error);
                })
            )
            .subscribe();
    }

    onSuccessLogin(): void {
        this._router.navigateByUrl('/dashboard');
        this.isLoading.set(false);
    }

    onSubmit() {
        if (this.loginForm.valid) {
            this.login();
        }
    }

    generateQrCode(otpSecret: string) {
        const otpAuthUrl = `otpauth://totp/UserManagementSystem:${this.loginForm.get('username')?.value}?secret=${otpSecret}&issuer=UserManagementSystem`;

        QRCode.toCanvas(this.qrCodeCanvas.nativeElement, otpAuthUrl, (error) => {
            if (error) {
                console.error(error);
            } else {
                console.log('QR Code generated successfully');
            }
        });
    }

    verifyOtp() {
        if (this.userLogged?.otpSecret) {
            this._authService.verifyOtp(this.userLogged!.otpSecret!, this.otpCode!, this.loginForm.get('username')?.value).subscribe((response) => {
                if (response) {
                    localStorage.setItem('token', response.token);
                    this._userService.updateUser(this.userLogged!).subscribe();
                    this.otpDialogVisible = false;
                    this.otpQrDialogVisible = false;
                    this.isLoading.set(false);
                    this.otpCode = undefined;

                    this.onSuccessLogin();
                }
            });
        } else {
            this._authService.verifyOtp(this.otpSecret!, this.otpCode!, this.loginForm.get('username')?.value).subscribe((response) => {
                if (response) {
                    this._userService.updateUser(this.userLogged!).subscribe();
                    this.otpDialogVisible = false;
                    this.otpQrDialogVisible = false;
                    this.isLoading.set(false);

                    this.onSuccessLogin();
                }
            });
        }
    }
}
