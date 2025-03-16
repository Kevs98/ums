export interface ApplicationI {
    id?: number;
    type_id?: number;
    type?: string;
    zone_id?: number;
    zone?: string;
    observations?: string;
    approver_id?: number;
    approver?: string;
    applicant_id?: number;
    applicant?: string;
    created_at?: Date;
    updated_at?: string;
    approved?: boolean;
}
