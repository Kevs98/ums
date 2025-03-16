import { Component } from '@angular/core';
import { NotificationsWidget } from './components/notificationswidget';
import { StatsWidget } from './components/statswidget';
import { RecentSalesWidget } from './components/recentsaleswidget';
import { BestSellingWidget } from './components/bestsellingwidget';
import { RevenueStreamWidget } from './components/revenuestreamwidget';
import { UsersComponent } from '../components/users/users.component';
import { ApplicationsComponent } from '../components/applications/applications.component';

@Component({
    selector: 'app-dashboard',
    imports: [ApplicationsComponent],
    template: `
        <div class="grid grid-cols-12 gap-8">
            <app-applications></app-applications>
        </div>
    `
})
export class Dashboard {}
