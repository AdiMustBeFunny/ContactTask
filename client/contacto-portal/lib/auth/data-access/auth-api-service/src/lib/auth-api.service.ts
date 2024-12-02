import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticateResponse } from './auth.response';

@Injectable({providedIn: 'root'})
export class AuthApiService {

    private readonly AUTH_URL = 'https://localhost:7123/auth';
    private readonly _http = inject(HttpClient);

    authenticateUser(username:string, password:string): Observable<AuthenticateResponse>{
        return this._http.post<AuthenticateResponse>(this.AUTH_URL, { userName: username, password: password})
    }

}

