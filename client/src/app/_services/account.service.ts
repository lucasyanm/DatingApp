import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = 'https://localhost:5001/api';
   
  //to store the current user of the section
  private currentUserSource = new ReplaySubject<User>(1);
  //the simbol "$" is to identify as a observable
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(`${this.baseUrl}/account/login`, model).pipe(
      map((response: User) => {
        const user = response;
        if(user)
        {
          this.setCurrentUserAndSaveSection(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}/account/register`, model).pipe(
      map ((user: User) => {
        if (user)
        { 
          this.setCurrentUserAndSaveSection(user);
        }
      })
    )
  }

  setCurrentUserAndSaveSection(user: User): void {    
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  setCurrentUser(user: User): void{
    this.currentUserSource.next(user);
  }

  logout(): void{
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
//services are unique instances that are destroyed only when the application is turned down (singleton)
//it usually used to http requests, but have more utilities
