import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../interfaces/login.interface';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(
        private http: HttpClient,
        private router: Router
    ) {}

    login(loginRequest: LoginRequest): Observable<any> {
        return this.http
            .post<any>(`${API_ENDPOINTS.login}`, {
                username: loginRequest.username,
                password: loginRequest.password
            })
            .pipe(
                map((response) => {
                    return response.Data;
                }),
                catchError((error) => {
                    console.error(error);
                    return throwError(error);
                })
            );
    }

    verifyOtp(secret: string, otpCode: number, username: string): Observable<any> {
        return this.http
            .post<any>(`${API_ENDPOINTS.verifyOtp}`, {
                SecretKey: secret,
                OtpCode: otpCode,
                Username: username
            })
            .pipe(
                map((response) => {
                    return response.Data;
                }),
                catchError((error) => {
                    console.error(error);
                    return throwError(error);
                })
            );
    }

    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        this.router.navigateByUrl('/login');
    }
}
