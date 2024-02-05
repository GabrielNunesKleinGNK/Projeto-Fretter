import { Component, ChangeDetectorRef, ViewChild, TemplateRef } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { NgxLoadingComponent, ngxLoadingAnimationTypes } from "ngx-loading";

@Component({
    selector: 'app-root',
    template: ``
})
export class LoadingComponent { 
    @ViewChild('ngxLoading') ngxLoadingComponent: NgxLoadingComponent;
	@ViewChild('customLoadingTemplate') customLoadingTemplate: TemplateRef<any>;

    public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
    public loading = true;
    public primaryColour = "green";
    public secondaryColour = "red";
    public coloursEnabled = false;
    public loadingTemplate: TemplateRef<any>;
    public config = { animationType: ngxLoadingAnimationTypes.none, primaryColour: this.primaryColour, secondaryColour: this.secondaryColour, tertiaryColour: this.primaryColour, backdropBorderRadius: '3px' };
    constructor(private translate: TranslateService, private cd: ChangeDetectorRef) {
    }

    ngAfterViewInit() {
        
        this.cd.detectChanges();
    }

    Exibir(){
        this.loading = true;
    }

    Ocultar(){
        this.loading = false;
    }
}