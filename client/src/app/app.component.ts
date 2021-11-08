import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit{
  title = 'The Dating App';
  users: any;

  constructor(private http: HttpClient, private accountService: AccountService) {
    
  }
  // A component instance has a lifecycle that 
  // starts when Angular instantiates the component 
  // class and renders the component view along with 
  // its child views. The lifecycle continues with 
  // change detection, as Angular checks to see when 
  // data-bound properties change, and updates both the 
  // view and the component instance as needed. 
  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }, 
    error => {
      console.error(error);
    }, //"always" only needs to declare a arrow function without arguments () => {}
    )
  }
}
