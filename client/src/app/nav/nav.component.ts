import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.sass']
})
export class NavComponent implements OnInit {
  model: any = {};

  //adding services to component through constructor
  constructor(
    public accountService: AccountService, 
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountService.login(this.model).subscribe(() => {
      this.router.navigateByUrl('/members');
    }, error => {
      console.error(error);
      this.toastr.error(error.error);
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
