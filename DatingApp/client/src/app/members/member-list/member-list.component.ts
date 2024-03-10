import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit{
  /* Change from Section 122: Using the service to store state
  members: Member[] = []

  constructor(private memberService: MembersService) {}
  ngOnInit(): void {
    console.log("MemberListComponent")
    this.loadMembers()
  }

  loadMembers(){
    this.memberService.getMembers().subscribe({
      next: members => this.members = members
    })
  }
  */
  member$: Observable<Member[]> | undefined;
  constructor(private memberService: MembersService) {}
  ngOnInit(): void {
    this.member$ = this.memberService.getMembers();
  }
}
