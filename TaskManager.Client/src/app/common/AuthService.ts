import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocalStorageAccessor } from './LocalStorageAccessor';
import { Role } from '../models/enums/Role';

@Injectable({
    providedIn: 'root',
})

export class AuthService {
    private _jwtHelperService: JwtHelperService = new JwtHelperService();
    public constructor(private _localStorageAccessor: LocalStorageAccessor) {
    }

    public get isLoggedIn(): boolean {
        const token = this._localStorageAccessor.token;
        return token && !this._jwtHelperService.isTokenExpired(token);
    }

    public HasRole(role: Role): boolean {
        const token = this._localStorageAccessor.token;
        return token && Role[role] === this._jwtHelperService.decodeToken(token).role;
    }
}
