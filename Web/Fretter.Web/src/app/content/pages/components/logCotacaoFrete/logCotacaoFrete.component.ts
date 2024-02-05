import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../core/services/layout-config.service';

@Component({
    selector: 'm-log-cotacao-frete',
    templateUrl: './logCotacaoFrete.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LogCotacaoFreteComponent implements OnInit {

    public config: any;

    constructor(
        private router: Router,
        private layoutConfigService: LayoutConfigService
    ) {
    }

    ngOnInit(): void { }
}
