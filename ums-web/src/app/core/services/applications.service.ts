import { HttpClient } from '@angular/common/http';
import { Injectable, Signal, signal } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
import { API_ENDPOINTS } from '../constants/api-endpoints';
import { ApplicationI } from '../interfaces/application.interface';

@Injectable({
    providedIn: 'root'
})
export class ApplicationsService {
    private _allApplications = signal<any[]>([]);
    private _allApplicationTypes = signal<any[]>([]);

    constructor(private http: HttpClient) {}

    get allApplications(): Signal<any[]> {
        return this._allApplications;
    }

    get allApplicationTypes(): Signal<any[]> {
        return this._allApplicationTypes;
    }

    getApplications(): Observable<any> {
        return this.http.get<any>(`${API_ENDPOINTS.getApplications}`).pipe(
            map((response) => {
                this._allApplications.set(response.Data);
                return this.allApplications();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    getApplication(id: number): Observable<any> {
        return this.http.get<any>(`${API_ENDPOINTS.getApplication}`, { params: { applicationId: id } }).pipe(
            map((response) => {
                this._allApplications.set(response.Data);
                return this.allApplications();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    getApplicationTypes(): Observable<any> {
        return this.http.get<any>(`${API_ENDPOINTS.getApplicationTypes}`).pipe(
            map((response) => {
                this._allApplicationTypes.set(response.Data);
                return this.allApplicationTypes();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    addApplication(application: any): Observable<any> {
        return this.http.post<any>(`${API_ENDPOINTS.addApplication}`, application).pipe(
            map((response) => {
                const updated = [...this._allApplications(), application];
                this._allApplications.set(updated);
                return this.allApplications();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    updateApplication(application: ApplicationI): Observable<any> {
        return this.http.patch<any>(`${API_ENDPOINTS.updateApplication}`, application).pipe(
            map((response) => {
                const updated = this._allApplications().map((appL) => (appL.id === application.id ? application : appL));
                this._allApplications.set(updated);
                return application;
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }
}
