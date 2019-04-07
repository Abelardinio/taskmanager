import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './common/AuthService';
import { Role } from './models/enums/Role';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'app';

  constructor(private router: Router, private authService: AuthService) {

  }

  public get isMenuPage() { return this.router.url !== '/login'; }
  public get isSiteAdministrator() { return this.authService.HasRole(Role.SiteAdministrator); }
}
