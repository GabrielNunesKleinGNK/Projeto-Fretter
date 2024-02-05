import { AuthenticationService } from '../../../../../core/auth/authentication.service';
import { ChangeDetectionStrategy, Component, ElementRef, HostBinding, Input, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { UsuarioService } from '../../../../../core/services/usuario.service';
import { Usuario } from '../../../../../core/models/usuario';

@Component({
	selector: 'm-user-profile',
	templateUrl: './user-profile.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserProfileComponent implements OnInit {
	@HostBinding('class')
	classes = 'm-nav__item m-topbar__user-profile m-topbar__user-profile--img m-dropdown m-dropdown--medium m-dropdown--arrow m-dropdown--header-bg-fill m-dropdown--align-right m-dropdown--mobile-full-width m-dropdown--skin-light';
	@HostBinding('attr.m-dropdown-toggle') attrDropdownToggle = 'click';
	@Input() avatarBg: SafeStyle = '';
	@ViewChild('mProfileDropdown') mProfileDropdown: ElementRef;

	model: Usuario;
	constructor (
		private router: Router,
		private cdr: ChangeDetectorRef,
		private authService: AuthenticationService,
		private sanitizer: DomSanitizer,
		private _userService : UsuarioService
	) {}

	ngOnInit (): void {
		this.model = new Usuario();
		this.model.avatar = "./../../../../../../assets/app/media/img/users/user.png";
		if (!this.avatarBg) {
			this.avatarBg = this.sanitizer.bypassSecurityTrustStyle('url(./assets/app/media/img/misc/user_profile_bg.jpg)');
		}
		
		// this._userService.getPerfil().subscribe(data => {
		// 	this.model = data;
		// 	this.cdr.detectChanges();
		// });
	}

	public logout () {
		this.authService.logout(true);
	}
}
