export interface User {
    id?: number;
    name?: string;
    last_name?: string;
    gender?: string;
    birthDate?: Date;
    image?: string;
    username?: string;
    email?: string;
    password?: string | null;
    role_id?: number;
    role?: string;
    zone_id?: number;
    zone?: string;
    otpSecret?: string | null;
}
