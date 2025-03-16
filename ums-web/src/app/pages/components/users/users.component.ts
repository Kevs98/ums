import { InputTextModule } from 'primeng/inputtext';
import { Component, effect, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user.service';
import { RoleService } from '../../../core/services/role.service';
import { ZoneService } from '../../../core/services/zone.service';
import { User } from '../../../core/interfaces/user.interface';
import { Role } from '../../../core/interfaces/role.interface';
import { Zone } from '../../../core/interfaces/zone.interface';
import { Table, TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';

@Component({
    selector: 'app-users',
    imports: [TableModule, ButtonModule, CardModule, TooltipModule, InputTextModule],
    templateUrl: './users.component.html',
    styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
    users: User[] = [];
    roles: Role[] = [];
    zones: Zone[] = [];

    constructor(
        private userService: UserService,
        private roleService: RoleService,
        private zoneService: ZoneService
    ) {
        effect(() => {
            this.users = this.userService.allUsers();
            this.roles = this.roleService.allRoles();
            this.zones = this.zoneService.allZones();
        });
    }

    ngOnInit(): void {
        this.userService.getUsers().subscribe();
        this.roleService.getRoles().subscribe();
        this.zoneService.getZones().subscribe();
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    clear(table: Table) {
        table.clear();
    }

    getRoleName(roleId: number): string {
        const role = this.roles.find((role) => role.role_id === roleId);
        return role ? role.role! : '';
    }

    getZoneName(zoneId: number): string {
        const zone = this.zones.find((zone) => zone.zone_id === zoneId);
        return zone ? zone.zone : '';
    }
}
