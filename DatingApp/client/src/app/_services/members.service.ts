import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  // Section 104
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }
  // Section 122: Using the Service to store state
  // This will let us store all members in service so that it will not reload again
  getMembers(){
    // Remove this.getHttpOptions() from section 109
    // return this.http.get<Member[]>(this.baseUrl + 'user', this.getHttpOptions())
    if (this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'user').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }

  getMember(username: string){
    // Remove this.getHttpOptions() from section 109
    // return this.http.get<Member>(this.baseUrl + 'user/' + username, this.getHttpOptions())
    const member = this.members.find(x => x.userName == username);
    if(member) return of(member);
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
    return this.http.put(this.baseUrl + 'user', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index], ...member}
      })
    );
  }
}
