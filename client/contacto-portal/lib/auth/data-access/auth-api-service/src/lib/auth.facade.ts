import { inject, Injectable } from "@angular/core";
import { AuthApiService } from "./auth-api.service";
import { BehaviorSubject, Observable, of, Subject, switchMap, tap } from "rxjs";
import { AuthenticateResponse } from "./auth.response";
import { JwtHelperService } from "@auth0/angular-jwt";


@Injectable({
    providedIn:'root'
})
export class AuthFacade{
    private readonly _authApiService = inject(AuthApiService);
    private readonly _jwtHelper = inject(JwtHelperService)

    private userAuthenticatedSubject = new BehaviorSubject<boolean>(false)
    public userAuthenticated$ = this.userAuthenticatedSubject.asObservable();

    constructor(){
        this.getBearerToken()
    }

    getBearerToken() : string | null{
        const token = localStorage.getItem("jwt");
        if (token && !this._jwtHelper.isTokenExpired(token)) {
            this.userAuthenticatedSubject.next(true);
            return token
        }
        else {
            this.userAuthenticatedSubject.next(false);
            return null
        }
    }

    public authenticateUser(username:string, password: string) : Observable<boolean>{
        return this._authApiService.authenticateUser(username, password)
        .pipe(
            switchMap(response =>{
                localStorage.setItem('jwt', response.token);
                this.userAuthenticatedSubject.next(true);
                return of(true);
            })
        )
    }

    public logOut(){
        localStorage.removeItem('jwt')
        this.userAuthenticatedSubject.next(false);
    }

    // public isUserAuthenticated(): boolean{
    //     const token = localStorage.getItem("jwt");
    //     if (token && !this._jwtHelper.isTokenExpired(token)) {
    //         this.userAuthenticatedSubject.next(true);
    //         console.log('user authenticated')
    //     }
    //     else {
    //         this.userAuthenticatedSubject.next(false);
    //         console.log('user not authenticated')
    //     }
    // }
}