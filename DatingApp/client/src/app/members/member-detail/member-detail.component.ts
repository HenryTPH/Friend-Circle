import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member:Member | undefined;

  constructor(private memberService: MembersService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    var username = this.route.snapshot.paramMap.get('username');
    if (!username) return;
    // paramMap contains map or array of root parameter inside our route snapshot
    this.memberService.getMember(username).subscribe({
      next: member => this.member = member
    });
  }

}
