import { Component, ComponentFactoryResolver, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register(): void {
    this.accountService.register(this.model).subscribe(() => {
      this.cancel();
    }, error => {
      console.error("Error registering user");
      console.error(error);
    })
  }

  cancel(): void {
    this.cancelRegister.emit(false);
  }

}
