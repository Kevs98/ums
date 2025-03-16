import { HttpClient } from '@angular/common/http';
import { Injectable, Signal, signal } from '@angular/core';
import { Zone } from '../interfaces/zone.interface';
import { catchError, map, Observable, throwError } from 'rxjs';
import { API_ENDPOINTS } from '../constants/api-endpoints';

@Injectable({
    providedIn: 'root'
})
export class ZoneService {
    private _allZones = signal<Zone[]>([]);

    constructor(private http: HttpClient) {}

    get allZones(): Signal<Zone[]> {
        return this._allZones;
    }

    getZones(): Observable<any> {
        return this.http.get<any>(`${API_ENDPOINTS.getZones}`).pipe(
            map((response) => {
                this._allZones.set(response.Data);
                return this.allZones();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    addZone(zone: Zone): Observable<any> {
        return this.http.post<any>(`${API_ENDPOINTS.addZone}`, zone).pipe(
            map((response) => {
                return this.allZones();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }

    deleteZone(zoneId: number): Observable<any> {
        return this.http.delete<any>(`${API_ENDPOINTS.deleteZone}/${zoneId}`).pipe(
            map((response) => {
                return this.allZones();
            }),
            catchError((error) => {
                console.error(error);
                return throwError(error);
            })
        );
    }
}
