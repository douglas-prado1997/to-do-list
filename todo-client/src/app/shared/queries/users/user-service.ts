import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import * as jwtDecodeModule from 'jwt-decode';
import { IUser } from './models/user';
import { API_URL } from '../../../app.config';

interface CustomJwtPayload {
  iss?: string;
  sub?: string;
  exp?: number;
  iat?: number;
  
  name?: string;
  idUser?: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = API_URL + '';

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    const body = { email, password };

    return this.http.post<any>(this.apiUrl+'/users/login', body).pipe(
      map(response => {
        if (response?.token) {
          localStorage.setItem('authToken', response.token);

          const decodedToken = jwtDecodeModule.jwtDecode<CustomJwtPayload>(response.token);

          if (decodedToken) {
            const username = decodedToken.name;
            const userId = decodedToken.idUser;

            if (username && userId) {
              localStorage.setItem('username', username);
              localStorage.setItem('userId', userId);
            }
          }
        }
        return response; 
      })
    );
  }

    addUser(user: IUser): Observable<void> {
      return this.http.post<void>(this.apiUrl+'/users/create', user);
    }
}
