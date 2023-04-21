import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  postPassword(data: any){
    return this.http.post<any>("https://localhost:7163/api/Password",data);
  }
  getAllPasswords(){
    return this.http.get<any>("https://localhost:7163/api/Password/all");
  }
  getPasswordById(id:number,decrypt: number){
    return this.http.get<any>(`https://localhost:7163/api/Password/one?id=${id}&decrypt=${decrypt}`);
  }
  getPasswordsByUsername(username:string,decrypt: number){
    return this.http.get<any>(`https://localhost:7163/api/Password/user?username=${username}&decrypt=${decrypt}`);
  }
  updatePassword(id:number, data:any){
    return this.http.put<any>(`https://localhost:7163/api/Password/?id=${id}`,data);
  }
  deletePassword(id:number){
    return this.http.delete<any>(`https://localhost:7163/api/Password/?id=${id}`);
  }
}
