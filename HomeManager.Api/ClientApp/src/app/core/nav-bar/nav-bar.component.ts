import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  isAdmin: boolean;
  constructor(http: HttpClient) {
    http.get<boolean>('https://localhost:44392/api/user/isadmin').subscribe(result => {
      this.isAdmin = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
  }

}
