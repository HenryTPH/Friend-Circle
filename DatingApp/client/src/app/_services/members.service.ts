import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  // Section 104
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers(){
    // Remove this.getHttpOptions() from section 109
    // return this.http.get<Member[]>(this.baseUrl + 'user', this.getHttpOptions())
    return this.http.get<Member[]>(this.baseUrl + 'user')
  }

  getMember(username: string){
    // Remove this.getHttpOptions() from section 109
    // return this.http.get<Member>(this.baseUrl + 'user/' + username, this.getHttpOptions())
    return this.http.get<Member>(this.baseUrl + 'user/' + username)
  }
  // Pass up the authorization token
  // Remove it from section 109
  // getHttpOptions(){
  //   const userString = localStorage.getItem('user')
  //   if(!userString) return;
  //   const user = JSON.parse(userString)
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token //Space behind the Bearer is very important
  //     })
  //   }
  // }

  updateMember(member: Member)
  {
    return this.http.put(this.baseUrl + 'user', member);
  }
}
