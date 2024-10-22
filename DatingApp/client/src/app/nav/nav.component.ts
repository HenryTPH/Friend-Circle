import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model: any = {};
  // currentUser$: Observable<User | null> = of(null) // Section 58

  constructor(public accountService: AccountService, 
              private router: Router,
              private toastr: ToastrService) {}

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$; // Section 58
    
  }

  // getCurrentUser(){ // Hide it in section 58
  //   this.accountService.currentUser$.subscribe(
//     {next: user => this.loggedIn = !!user,
  //       error: err => console.log(err)
  //     }
  //   )
  // }

  login(){
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      // error: err => this.toastr.error(err.error),
    })
    console.log(this.model);
  }

  logout(){
    //Remove the item in the localStorage when log out
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
