import { environment } from '../../environments/environment';

export const API_ENDPOINTS = {
    login: `${environment.apiBaseUrl}/Authentication/login`,
    verifyOtp: `${environment.apiBaseUrl}/Authentication/validate-otp`,
    getUsers: `${environment.apiBaseUrl}/User/getUsers`,
    getUser: `${environment.apiBaseUrl}/User/getUser`,
    updateUser: `${environment.apiBaseUrl}/User/updateUser`,
    getRoles: `${environment.apiBaseUrl}/Role/getRoles`,
    getZones: `${environment.apiBaseUrl}/Zone/getZones`,
    addZone: `${environment.apiBaseUrl}/Zone/addZone`,
    deleteZone: `${environment.apiBaseUrl}/Zone/deleteZone`,
    getApplications: `${environment.apiBaseUrl}/Application/getApplications`,
    getApplication: `${environment.apiBaseUrl}/Application/getApplication`,
    updateApplication: `${environment.apiBaseUrl}/Application/updateApplication`,
    addApplication: `${environment.apiBaseUrl}/Application/addApplication`,
    getApplicationTypes: `${environment.apiBaseUrl}/Application/getApplicationTypes`
};
