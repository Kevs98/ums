import { ApplicationI } from './../../../core/interfaces/application.interface';
import { Component, effect, OnInit } from '@angular/core';
import { ApplicationsService } from '../../../core/services/applications.service';
import { Table, TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { InputTextModule } from 'primeng/inputtext';
import { ApplicationType } from '../../../core/interfaces/applicationType.interface';
import { User } from '../../../core/interfaces/user.interface';
import { UserService } from '../../../core/services/user.service';
import { Zone } from '../../../core/interfaces/zone.interface';
import { ZoneService } from '../../../core/services/zone.service';
import { forkJoin } from 'rxjs';
import { ChipModule } from 'primeng/chip';
import { DatePipe } from '@angular/common';
import { honduranDate } from '../../../core/constants/time-constant';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
    selector: 'app-applications',
    imports: [TableModule, ButtonModule, CardModule, TooltipModule, InputTextModule, ChipModule, DatePipe, ConfirmDialogModule, ToastModule],
    templateUrl: './applications.component.html',
    styleUrl: './applications.component.scss',
    providers: [ConfirmationService, MessageService]
})
export class ApplicationsComponent implements OnInit {
    applications: ApplicationI[] = [];
    applicationTypes: ApplicationType[] = [];
    users: User[] = [];
    zones: Zone[] = [];

    constructor(
        private applicationService: ApplicationsService,
        private userService: UserService,
        private zoneService: ZoneService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService
    ) {
        // effect(() => {
        //     this.applications = this.applicationService.allApplications();
        //     this.applicationTypes = this.applicationService.allApplicationTypes();
        //     this.users = this.userService.allUsers();
        // });
    }

    ngOnInit(): void {
        forkJoin({
            applications: this.applicationService.getApplications(),
            applicationTypes: this.applicationService.getApplicationTypes(),
            users: this.userService.getUsers(),
            zones: this.zoneService.getZones()
        }).subscribe(({ applications, applicationTypes, users, zones }) => {
            this.applications = applications.sort((a: any, b: any) => new Date(b.created_at).getTime() - new Date(a.created_at).getTime());
            this.applicationTypes = applicationTypes;
            this.users = users;
            this.zones = zones;
            this.populateApplications();
        });

        console.log('applications', this.applications);
    }

    populateApplications() {
        this.applications.forEach((application) => {
            application.type = this.applicationTypes.find((type) => type.type_id === application.type_id)?.type || 'Desconocido';
            const approver = this.users.find((user) => user.id === application.approver_id);
            application.approver = approver ? `${approver.name} ${approver.last_name ?? ''}`.trim() : 'Sin aprobador';

            const applicant = this.users.find((user) => user.id === application.applicant_id);
            application.applicant = applicant ? `${applicant.name} ${applicant.last_name ?? ''}`.trim() : 'Sin solicitante';
            application.zone = this.zones.find((zone) => zone.zone_id === application.zone_id)?.zone || 'Sin zona';
        });
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
    }

    getApprovalLabel(status: boolean | null): string {
        if (status === true) return 'Aprobado';
        if (status === false) return 'Rechazado';
        return 'Pendiente';
    }

    getApprovalClass(status: boolean | null): string {
        if (status === true) return 'bg-green-500 text-white';
        if (status === false) return 'bg-red-500 text-white';
        return 'bg-blue-500 text-white';
    }

    approveApplication(application: ApplicationI) {
        this.confirmationService.confirm({
            header: '¿Estás seguro de aprobar esta solicitud?',
            message: 'Esta acción no puede ser revertida, confirme para proceder.',
            icon: 'fas fa-exclamation-triangle',
            acceptLabel: 'Aprobar',
            rejectLabel: 'Cancelar',
            accept: () => {
                const applicationObj: ApplicationI = {
                    id: application.id,
                    type_id: application.type_id,
                    zone_id: application.zone_id,
                    observations: application.observations,
                    approver_id: application.approver_id,
                    applicant_id: application.applicant_id,
                    created_at: application.created_at,
                    updated_at: honduranDate.clone().subtract(6, 'hours').utc().toISOString(),
                    approved: true
                };
                this.applicationService.updateApplication(applicationObj).subscribe();
                this.refreshApplications();
                this.messageService.add({ severity: 'success', summary: 'Solicitud aprobada', detail: 'La solicitud ha sido aprobada.' });
            }
        });
    }

    rejectApplication(application: ApplicationI) {
        this.confirmationService.confirm({
            header: '¿Estás seguro de rechazar esta solicitud?',
            message: 'Esta acción no puede ser revertida, confirme para proceder.',
            icon: 'fas fa-exclamation-triangle',
            acceptLabel: 'Rechazar',
            rejectLabel: 'Cancelar',
            accept: () => {
                const applicationObj: ApplicationI = {
                    id: application.id,
                    type_id: application.type_id,
                    zone_id: application.zone_id,
                    observations: application.observations,
                    approver_id: application.approver_id,
                    applicant_id: application.applicant_id,
                    created_at: application.created_at,
                    updated_at: honduranDate.clone().subtract(6, 'hours').utc().toISOString(),
                    approved: false
                };
                this.applicationService.updateApplication(applicationObj).subscribe();
                this.refreshApplications();
                this.messageService.add({ severity: 'success', summary: 'Solicitud rechazada', detail: 'La solicitud ha sido rechazada.' });
            }
        });
    }

    refreshApplications() {
        this.applicationService.getApplications().subscribe((applications) => {
            this.applications = applications.sort((a: any, b: any) => new Date(b.created_at).getTime() - new Date(a.created_at).getTime());
            this.populateApplications();
        });
    }
}
