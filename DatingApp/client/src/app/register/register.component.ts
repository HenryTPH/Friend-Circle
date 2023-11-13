import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  // @Input() usersFromHomeComponent: any; // Add in section 61, remove from section 63
  @Output() cancelRegister = new EventEmitter();
  model: any = {}
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next: res => {
        console.log(res);
        this.cancel();
      },
      error: err => console.log(err),
    }
    )
  }

  cancel(){
    this.cancelRegister.emit(false)
  }
}
