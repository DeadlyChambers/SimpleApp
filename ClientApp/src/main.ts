import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  //return 'https://soinshane.com/';
  return 'http://localhost:4200/';
 // return document.getElementsByTagName('base')[0].href;
}
export function getApiUrl(){
   //return 'https://api.soinshane.com/';
return 'https://localhost:44341/api/';
  //return 'http://localhost:27819/';
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  {provide: 'API_URL', useFactory: getApiUrl, deps: []}
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
