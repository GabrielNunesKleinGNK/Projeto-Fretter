import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { LoaderService } from '../../../../core/services/loader.service';
@Component({
  selector: 'm-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent {
  color = 'primary';
  mode = 'indeterminate';
  value = 20;
  isLoading: Subject<boolean> = new Subject<boolean>();
  constructor(private loaderService: LoaderService)
  {
    setTimeout(() => {
      this.loaderService.isLoading;
    }, 500);

  }

}