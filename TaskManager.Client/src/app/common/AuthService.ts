import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocalStorageAccessor } from './LocalStorageAccessor';

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
}
