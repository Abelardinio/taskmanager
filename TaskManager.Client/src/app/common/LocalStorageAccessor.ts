import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
})

export class LocalStorageAccessor{
    private TokenParameter:string = 'token';
    public get token(): string { return localStorage.getItem(this.TokenParameter); }
    public set token(token: string){ localStorage.setItem(this.TokenParameter, token); }
}
