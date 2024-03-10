import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/'
  validationError: string[] = []

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    
  }

  get404Error()
  {
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    })
  }

  get400Error()
  {
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    })
  }

  get500Error()
  {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    })
  }

  get401Error()
  {
    this.http.get(this.baseUrl + 'buggy/auth').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    })
  }

  get400ValidationError()
  {
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      next: res => console.log(res),
      error: err => {
        console.log(err)
        this.validationError = err
      }
    })
  }

}
