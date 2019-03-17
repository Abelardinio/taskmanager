import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './AuthService';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})

export class LoginActivate implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    public canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<boolean> | Promise<boolean> | boolean {
        if (!this.authService.isLoggedIn) {
            this.router.navigate(['/login']);
        }
        return true;
    }
}
