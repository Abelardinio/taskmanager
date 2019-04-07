import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './AuthService';
import { Observable } from 'rxjs';
import { Role } from '../models/enums/Role';

@Injectable({
    providedIn: 'root',
})

export class SiteAdministratorActivate implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    public canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | Promise<boolean> | boolean {
        return this.authService.HasRole(Role.SiteAdministrator);
    }
}
