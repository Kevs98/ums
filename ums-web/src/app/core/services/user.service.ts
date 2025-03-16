import { Injectable, Signal, signal } from '@angular/core';
import { User } from '../interfaces/user.interface';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { API_ENDPOINTS } from '../constants/api-endpoints';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private _allUsers = signal<User[]>([]);

    constructor(private http: HttpClient) {}

    get allUsers(): Signal<User[]> {
        return this._allUsers;
    }

    getUsers(): Observable<any> {
        return this.http.get<any>(`${API_ENDPOINTS.getUsers}`).pipe(
            map((response) => {
                this._allUsers.set(response.Data);
                return this.allUsers();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    getUser(username: string, password: string): Observable<any> {
        let params = new HttpParams();
        params = params.append('username', username);
        params = params.append('password', password);
        return this.http.get<any>(`${API_ENDPOINTS.getUser}`, { params }).pipe(
            map((response) => {
                return response.Data;
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    updateUser(user: User): Observable<any> {
        return this.http.patch<any>(`${API_ENDPOINTS.updateUser}`, user).pipe(
            map((response) => {
                const updatedUser = this._allUsers().map((userL) => (userL.id === user.id ? user : userL));
                this._allUsers.set(updatedUser);
                return user;
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }
}
