import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../models/app.config';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService extends AppConfig {

  constructor(private http: HttpClient) {
    super();
  }

  // This function needs to return a promise
  load() {
    return this.http.get<AppConfig>('../../../assets/config.json')
      .toPromise()
      .then(data => {
        this.baseUrl = data.baseUrl;
      })
      .catch(() => {
        console.error('Could not load configuration');
      });
  }
}
