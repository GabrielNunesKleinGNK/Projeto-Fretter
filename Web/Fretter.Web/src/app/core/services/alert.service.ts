import { Injectable } from '@angular/core';

// import Swal, { SweetAlertType } from 'sweetalert2';
import Swal from 'sweetalert2'
// import * as _swal from 'sweetalert';


@Injectable()
export class AlertService {
  constructor() { }

  show(title: string, description: string, status: any) {
    return this.showWithConfiguration({
      title: title,
      text: description,
      type: status,
      showCloseButton: true
    });
  }

  showWithConfiguration(configuration) {
    return Swal.fire(configuration);
  }

  confirmationMessage(
    title: string,
    description: string,
    confirmButtonText: string = 'Confirmar',
    cancelButtonText: string = 'Cancelar'
  ) {
    return this.showWithConfiguration({
      title: title,
      text: description,
      type: 'warning',
      showCloseButton: true,
      showCancelButton: true,
      confirmButtonText: confirmButtonText,
      cancelButtonText: cancelButtonText
    });
  }
}
