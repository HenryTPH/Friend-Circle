import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient){ }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/user').subscribe({
      next: res => this.users = res,
      error: error => console.log(error),
      complete: () => console.log("Request has completed - Home.component.ts")
    });
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }
}
