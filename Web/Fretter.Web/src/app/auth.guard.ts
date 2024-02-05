import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenStorage } from './core/auth/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
    private tokenStorage: TokenStorage) { }
    
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    
    return this.checkUserLogin(next,state.url);
  }

  checkUserLogin(route: ActivatedRouteSnapshot, url: any): boolean {
    var paths = <any>this.tokenStorage.getPathPermission();
    if (paths.value !=null && paths.value.indexOf(url) === -1) {
      this.router.navigate(['/404']);
      return false;
    }
    return true;
   
  }
}
