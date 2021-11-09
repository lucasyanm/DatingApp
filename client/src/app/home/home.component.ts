import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor() { }

  ngOnInit(): void {
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }
  
  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

  // getUsers(){
  //   this.http.get(`${this.apiUrl}/users`).subscribe(users => {
  //     this.users = users;
  //   }, error => {
  //     console.error("Error to load users");
  //     console.error(error);
  //   }//"always" only needs to declare a arrow function without arguments () => {}
  //   )
  // }

}
