import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { AuthFacade } from "./auth.facade";

export const tokenRequiredFunction: CanActivateFn = (
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) => {
        const authFacade = inject(AuthFacade)
        const router = inject(Router)
        const token = authFacade.getBearerToken();
        if(token !== null){
            return true
        } else{
            router.navigate([''])
            return false
        }
  }