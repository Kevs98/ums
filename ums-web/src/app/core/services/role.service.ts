import { HttpClient } from '@angular/common/http';
import { Injectable, Signal, signal } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { Role } from '../interfaces/role.interface';

@Injectable({
    providedIn: 'root'
})
export class RoleService {
    private _allRoles = signal<Role[]>([]);

    constructor(private http: HttpClient) {}

    get allRoles(): Signal<Role[]> {
        return this._allRoles;
    }

    getRoles(): Observable<any> {
        return this.http.get<any>(`${API_ENDPOINTS.getRoles}`).pipe(
            map((response) => {
                this._allRoles.set(response.Data);
                return this.allRoles();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }
}
