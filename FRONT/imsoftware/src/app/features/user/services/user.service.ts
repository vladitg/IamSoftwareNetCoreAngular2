import { Injectable } from '@angular/core';
import { AddUserRequest } from '../models/add-user-request.model';
import { User } from '../models/user.model';
import { ResponseApi } from '../models/response-api.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl:string = 'http://localhost:5202/api/Users';

  constructor(private http: HttpClient) { }

  getList():Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.apiUrl}`);
  }

  addUser(model: AddUserRequest): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.apiUrl}/add`, model);
  }

  update(model: User): Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.apiUrl}/update`, model);
  }

  delete(id: string): Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.apiUrl}/delete/${id}`);
  }
}
