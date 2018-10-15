import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'app';

  constructor(private router: Router) {

  }

  public isTasksLinkActive(url: string): boolean {
    const currentUrl = this.router.url;
    return (currentUrl.indexOf(url) >= 0) && (currentUrl.indexOf('tasks/add') <= 0);
  }
}
