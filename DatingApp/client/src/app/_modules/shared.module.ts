import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs' //Section 112
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(), // Section 112
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule.forRoot({
      type: 'pacman'
    })
  ],
  exports:[
    BsDropdownModule,
    ToastrModule,
    TabsModule, // Section 112
    NgxSpinnerModule,
  ]
})
export class SharedModule { }
