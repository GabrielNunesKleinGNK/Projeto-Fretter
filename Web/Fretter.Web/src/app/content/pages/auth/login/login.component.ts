import {
	Component,
	OnInit,
	Output,
	Input,
	ViewChild,
	OnDestroy,
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	HostBinding
} from '@angular/core';
import { AuthenticationService } from '../../../../core/auth/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AuthNoticeService } from '../../../../core/auth/auth-notice.service';
import { NgForm } from '@angular/forms';
import { objectPath } from 'object-path';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'm-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit, OnDestroy {
	public model: any =
	{
		username: '',
		password: '',
		email: ''
	};
	@HostBinding('class') classes: string = 'm-login__signin';
	@Output() actionChange = new Subject<string>();
	public loading = false;

	@Input() action: string;

	@ViewChild('f') f: NgForm;
	errors: any = [];

	constructor(
		private authService: AuthenticationService,
		private router: Router,
		private activatedRoute: ActivatedRoute,
		public authNoticeService: AuthNoticeService,
		private translate: TranslateService,
		private cdr: ChangeDetectorRef
	) {		
		this.authService.clearLocalStorage();
		this.activatedRoute.queryParams
			.subscribe(params => {
				let refresh_token = params['refresh_token'];
				let route = params['route'];

				if (refresh_token)
					this.login(refresh_token, route)
				
			});
	}

	submit() {
		if (this.validate(this.f)) {
			this.authService.login(this.model)
				.subscribe(
					response => {
						if (typeof response !== 'undefined') {
							this.router.navigate(['/']);
						} else {
							this.authNoticeService.setNotice(this.translate.instant('AUTH.VALIDATION.INVALID_LOGIN'), 'error');
						}


						this.cdr.detectChanges();
					},
					err => {
						this.authNoticeService.setNotice(this.translate.instant('AUTH.VALIDATION.INVALID_LOGIN'), 'error');

						this.cdr.detectChanges();
					},
					() => {

					}
				);
			// .subscribe(response => {
			// 	debugger;
			// });
		}
	}

	ngOnInit(): void {
		// demo message to show
		if (!this.authNoticeService.onNoticeChanged$.getValue()) {
			const initialNotice = ``;
			this.authNoticeService.setNotice(initialNotice, 'success');
		}
	}

	ngOnDestroy(): void {
		this.authNoticeService.setNotice(null);
	}

	validate(f: NgForm) {
		if (f.form.status === 'VALID') {
			return true;
		}

		this.errors = [];
		if (objectPath.get(f, 'form.controls.email.errors.email')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.INVALID', { name: this.translate.instant('AUTH.INPUT.EMAIL') }));
		}
		if (objectPath.get(f, 'form.controls.email.errors.required')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.REQUIRED', { name: this.translate.instant('AUTH.INPUT.EMAIL') }));
		}

		if (objectPath.get(f, 'form.controls.password.errors.required')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.INVALID', { name: this.translate.instant('AUTH.INPUT.PASSWORD') }));
		}
		if (objectPath.get(f, 'form.controls.password.errors.minlength')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.MIN_LENGTH', { name: this.translate.instant('AUTH.INPUT.PASSWORD') }));
		}

		if (this.errors.length > 0) {
			this.authNoticeService.setNotice(this.errors.join('<br/>'), 'error');
		}

		return false;
	}

	forgotPasswordPage(event: Event) {
		this.action = 'forgot-password';
		this.actionChange.next(this.action);
	}

	login(refresh_token: string = null, routeName: string = null) {
		this.authService.refreshTokenImpersonate(refresh_token).subscribe(
			response => {
				this.authService.saveUserMenu();
				if (typeof response !== 'undefined') {
					if(routeName)
						this.router.navigate([`/${routeName}`]);	
					else
						this.router.navigate(['/']);
				} else {
					this.authNoticeService.setNotice(this.translate.instant('AUTH.VALIDATION.INVALID_LOGIN'), 'error');
				}
				this.cdr.detectChanges();
			},
			err => {
				this.authNoticeService.setNotice(this.translate.instant('AUTH.VALIDATION.INVALID_LOGIN'), 'error');
				this.cdr.detectChanges();
			},
			() => {

			}
		);
	}

	register(event: Event) {
		this.action = 'register';
		this.actionChange.next(this.action);
	}
}
