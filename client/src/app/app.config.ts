import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient() //Gör HttpClient tillgänglig, måste inkluderas i Appcomponent i app.component.ts via inject(HttpClient). Antagligen liknande för andra Providers
  ]
};
