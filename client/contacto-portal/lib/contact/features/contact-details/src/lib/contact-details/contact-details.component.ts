import { Component, computed, DestroyRef, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService, CreateContactRequest } from '@contacto-portal/data-access';
import { forkJoin } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ContactListItem, ContactDetails, ContactCategory, ContactSubCategory } from '@contacto-portal/data-model'
import { FormsModule } from '@angular/forms';
import { AuthFacade} from '@contacto-portal/auth-api-service'
import { EditContactRequest } from '@contacto-portal/data-access'
import { DatePipe } from '@angular/common';
@Component({
  selector: 'lib-contact-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.css',
  // providers: [DatePipe]
})
export class ContactDetailsComponent implements OnInit {
categoryChanged() {
  this.contactSubCategoryId.set(null)
  this.customContactCategory.set(null)
}

  private readonly _activatedRoute = inject(ActivatedRoute)
  private readonly _contactService = inject(ContactService)
  private readonly _destroyRef = inject(DestroyRef)
  private readonly _authFacade = inject(AuthFacade);
  private readonly _router = inject(Router);

  userAuthenticated$ = this._authFacade.userAuthenticated$;

  id = signal('');
  name = signal('');
  password = signal('');
  surname = signal('');
  phoneNumber = signal('');
  email = signal('');
  birthDate = signal("2000-01-01" as any as Date);
  contactCategoryId = signal<string | null>(null);
  contactSubCategoryId = signal<string | null>(null);
  customContactCategory = signal<string | null>(null);

  contactCategories = signal<ContactCategory[]>([])

  subCategories = computed<ContactSubCategory[]>(() =>{
    const selectedCategoryId = this.contactCategoryId()
    const availableCategories = this.contactCategories()
    const category = availableCategories.find(c => c.id == selectedCategoryId)
    if(!category || !category.subCategories){
      return []
    }
    
    return category.subCategories
  })

  showCustomCategory = computed<boolean>(() =>{
    const selectedCategoryId = this.contactCategoryId()
    const availableCategories = this.contactCategories()
    const category = availableCategories.find(c => c.id == selectedCategoryId)
    if(!category){
      return false
    }

    return category.customCategory
  })

  errorMessage = signal<string>('')


  ngOnInit(): void {
    const id = this._activatedRoute.snapshot.params['id'] as string
    if(!id){
      this._contactService.getContactCategories()
      .pipe(
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe({
        next: data =>{
          this.contactCategories.set(data)
        }
      })
    }
    else{
      this.id.set(id)
      forkJoin({
        contact: this._contactService.getContact(id),
        contactCategories: this._contactService.getContactCategories()
      })
      .pipe(
        takeUntilDestroyed(this._destroyRef)
      )
      .subscribe({
        next: data =>{
          this.contactCategories.set(data.contactCategories)
          const contact = data.contact;
          console.log(contact)
          this.name.set(contact.name)
          this.surname.set(contact.surname)
          this.phoneNumber.set(contact.phoneNumber)
          this.email.set(contact.email)
          this.birthDate.set(contact.birthDate)
          this.contactCategoryId.set(contact.contactCategoryId)
          this.contactSubCategoryId.set(contact.contactSubCategoryId)
          this.customContactCategory.set(contact.customContactCategory)
        }
      })
    }
    
  }

  backToContactList() {
    this._router.navigate(['']);
  
  }

  saveContact() {
    const id = this.id();

    this.errorMessage.set('');

    if(!id){
      //create
      const createRequest: CreateContactRequest = {
        name: this.name(),
        password: this.password(),
        surname: this.surname(),
        phoneNumber: this.phoneNumber(),
        email: this.email(),
        birthDate: this.birthDate(),
        contactCategoryId: this.contactCategoryId(),
        contactSubCategoryId: this.contactSubCategoryId(),
        customContactCategory: this.customContactCategory(),
      }

      this._contactService.createContact(createRequest).subscribe({
        complete: () => {
          this._router.navigate(['']);
        },
        error: err => {
          const errorMessage = err?.error?.detail as any as string
          this.errorMessage.set(errorMessage);
        }
      })
    } else{
      //edit
      const editRequest: EditContactRequest = {
        name: this.name(),
        surname: this.surname(),
        phoneNumber: this.phoneNumber(),
        email: this.email(),
        birthDate: this.birthDate(),
        contactCategoryId: this.contactCategoryId(),
        contactSubCategoryId: this.contactSubCategoryId(),
        customContactCategory: this.customContactCategory(),
      }
    
      this._contactService.editContact(id, editRequest).subscribe({
        complete: () => {
          this._router.navigate(['']);
        },
        error: err => {
          const errorMessage = err?.error?.detail as any as string
          this.errorMessage.set(errorMessage);
        }
      })
    }
  }
}