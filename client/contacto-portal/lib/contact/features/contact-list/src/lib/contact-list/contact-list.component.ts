import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactService } from '@contacto-portal/data-access'
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { BehaviorSubject } from 'rxjs';
import { ContactListItem } from '@contacto-portal/data-model';
import { AuthFacade} from '@contacto-portal/auth-api-service'
import { Router } from '@angular/router';
@Component({
  selector: 'lib-contact-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-list.component.html',
  styleUrl: './contact-list.component.css',
})
export class ContactListComponent implements OnInit {
  private readonly _destroyRef = inject(DestroyRef);
  private readonly _contactService = inject(ContactService);
  private readonly _authFacade = inject(AuthFacade);
  private readonly _router = inject(Router);
  
  userAuthenticated$ = this._authFacade.userAuthenticated$;

  contactListSubject = new BehaviorSubject<ContactListItem[]>([]);
  contactList$ = this.contactListSubject.asObservable();

  ngOnInit(): void {
    this._contactService.getContacts()
    .pipe(
      takeUntilDestroyed(this._destroyRef)
    ).subscribe({
      next: data => {
        this.contactListSubject.next(data)
      }
    })
  }

  addNewContact() {
    console.log('asdas')
    this._router.navigate(['contact'])
  }
  
  deleteContact(id: string) {
    this._contactService.deleteContact(id)
    .pipe(
      takeUntilDestroyed(this._destroyRef)
    ).subscribe({
      complete: () =>{
        const contacts = this.contactListSubject.value;
        this.contactListSubject.next(contacts.filter(c => c.contactId != id));
      }
    })
  }
  
  showContact(id: string) {
    this._router.navigate([`contact/${id}`])
  }
}
