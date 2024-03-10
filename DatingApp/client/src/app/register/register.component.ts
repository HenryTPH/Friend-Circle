import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  // @Input() usersFromHomeComponent: any; // Add in section 61, remove from section 63
  @Output() cancelRegister = new EventEmitter();
  model: any = {}
  constructor(private accountService: AccountService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: err => this.toastr.error(err.error)
    }
    )
  }

  cancel(){
    this.cancelRegister.emit(false)
  }
}
