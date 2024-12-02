import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthFacade} from '@contacto-portal/auth-api-service'
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { BehaviorSubject, finalize } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'auth-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './auth-component.component.html',
  styleUrl: './auth-component.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AuthComponentComponent {

  _authFacade = inject(AuthFacade);
  _destroyRef = inject(DestroyRef);

  userAuthenticated$ = this._authFacade.userAuthenticated$
  executingRequestSubject = new BehaviorSubject<boolean>(false);
  executingRequest$ = this.executingRequestSubject.asObservable()
  
  errorMessage = signal<string>('')
  username: string = ''
  password: string = ''

  logOut() {
    this._authFacade.logOut()
  }

  logIn() {

    if(!this.username || !this.password){
      return;
    }
    this.errorMessage.set('');

    this.executingRequestSubject.next(true)

    this._authFacade.authenticateUser(this.username, this.password)
    .pipe(
      takeUntilDestroyed(this._destroyRef),
      finalize(() => this.executingRequestSubject.next(false))
    )
    .subscribe({
      next: data =>{
        this.username = ''
        this.password = ''
      },
      error: err => {
        const errorMessage = err?.error?.detail as any as string
        this.errorMessage.set(errorMessage);
      }
    })
  }
}
