import {Injectable} from '@angular/core';
// @ts-ignore
import Noty from 'noty';

@Injectable({
  providedIn: 'root'
})
export class NotyService {

  constructor() { }

  success(message: string = 'A kérés sikeresen végrehajtva') {
    return new Noty({
      type: 'success',
      layout: 'bottomRight',
      theme: 'relax',
      text: `<i class="fa fa-check-circle mr-2" style="font-size: 125%;"></i><strong> Sikeres:</strong> ${message}.`,
      timeout: '4000',
      progressBar: true,
      closeWith: ['click']
    }).show();
  }

  error(message: string = 'A kérés végrehajtása sikertelen') {
    return new Noty({
      type: 'error',
      layout: 'bottomRight',
      theme: 'relax',
      text: `<i class="fa fa-times-circle mr-2" style="font-size: 125%;"></i><strong>Sikertelen:</strong> ${message}.`,
      timeout: '4000',
      progressBar: true,
      closeWith: ['click']
    }).show();
  }

  warning(message: string) {
    return new Noty({
      type: 'warning',
      layout: 'bottomRight',
      theme: 'relax',
      text: `<i class="fa fa-exclamation-circle mr-2" style="font-size: 125%;"></i><strong>Figyelmeztetés:</strong> ${message}.`,
      timeout: '4000',
      progressBar: true,
      closeWith: ['click']
    }).show();
  }

  info(message: string) {
    return new Noty({
      type: 'info',
      layout: 'bottomRight',
      theme: 'relax',
      text: `<i class="fa fa-info-circle mr-2" style="font-size: 125%;"></i><strong>Információ:</strong> ${message}.`,
      timeout: '4000',
      progressBar: true,
      closeWith: ['click']
    }).show();
  }

  alert(message: string) {
    return new Noty({
      type: 'alert',
      layout: 'bottomRight',
      theme: 'relax',
      text: `${message}.`,
      timeout: '4000',
      progressBar: true,
      closeWith: ['click']
    }).show();
  }
}
