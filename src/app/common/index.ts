import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UIRouterModule } from '@uirouter/angular';

import { UserBadgeComponent } from './user-badge';
import { NavbarComponent } from './navbar';
import { DevButtonComponent } from './dev-button';

@NgModule({
  imports: [
    UIRouterModule.forChild(),
    BrowserModule,
    NgbModule,
  ],
  declarations: [
    UserBadgeComponent,
    NavbarComponent,
    DevButtonComponent,
  ],
  exports: [
    DevButtonComponent
  ]
})
export class CommonModule {
}
