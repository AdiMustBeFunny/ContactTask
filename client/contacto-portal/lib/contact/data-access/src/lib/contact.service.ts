import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ContactListItem, ContactDetails, ContactCategory } from '@contacto-portal/data-model'
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { CreateContactRequest, EditContactRequest } from "./contact.requests";
import { AuthFacade} from '@contacto-portal/auth-api-service'

@Injectable({providedIn:'root'})
export class ContactService{
    private readonly _http = inject(HttpClient);
    private readonly _authFacade = inject(AuthFacade);
    private readonly CONTACTS_URL = 'https://localhost:7123/contact';
    private readonly CONTACT_URL = 'https://localhost:7123/contact/';
    private readonly CONTACT_CATEGORIES_URL = 'https://localhost:7123/contact/category';
    private readonly CREATE_CONTACT_URL = 'https://localhost:7123/contact';
    private readonly EDIT_CONTACT_URL = 'https://localhost:7123/contact/';
    private readonly DELETE_CONTACT_URL = 'https://localhost:7123/contact/';

    getContacts():Observable<ContactListItem[]>{
        return this._http.get<ContactListItem[]>(this.CONTACTS_URL)
    }

    getContact(id:string):Observable<ContactDetails>{
        return this._http.get<ContactDetails>(this.CONTACT_URL + id)
    }

    getContactCategories():Observable<ContactCategory[]>{
        return this._http.get<ContactCategory[]>(this.CONTACT_CATEGORIES_URL)
    }

    createContact(request: CreateContactRequest):Observable<any>{
        const headers = this.getHeaders()
        return this._http.post(this.CREATE_CONTACT_URL, request, { headers })
    }

    editContact(id: string, request: EditContactRequest):Observable<any>{
        const headers = this.getHeaders()
        return this._http.post(this.EDIT_CONTACT_URL + id, request, { headers } )
    }

    deleteContact(id: string):Observable<any>{
        const headers = this.getHeaders()
        return this._http.delete(this.DELETE_CONTACT_URL + id, { headers } )
    }

    private getHeaders(){
        const auth_token = this._authFacade.getBearerToken();

        return new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${auth_token}`
        })
    }
}