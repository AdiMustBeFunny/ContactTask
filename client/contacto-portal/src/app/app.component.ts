import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthComponentComponent} from '@contacto-portal/auth-component'
@Component({
  standalone: true,
  imports: [RouterModule, AuthComponentComponent],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush

})
export class AppComponent  {


  title = 'contacto-portal';
}
