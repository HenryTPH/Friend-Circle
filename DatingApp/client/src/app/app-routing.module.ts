import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guard/auth.guard';

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {path:'members', component: MemberListComponent},
      {path:'members/:id', component: MemberDetailComponent},
      {path:'list', component: ListsComponent},
      {path:'messages', component: MessagesComponent},
    ]
  },  
  {path:'**', component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
