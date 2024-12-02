import { Route } from '@angular/router';
import {ContactListComponent} from '@contacto-portal/contact-list'
import {ContactDetailsComponent} from '@contacto-portal/contact-details'
import { tokenRequiredFunction } from '@contacto-portal/auth-api-service'
export const appRoutes: Route[] = [
    { path: '', component: ContactListComponent },
    { path: 'contact/:id', component: ContactDetailsComponent },
    { path: 'contact', component: ContactDetailsComponent, canActivate: [tokenRequiredFunction] }
];
