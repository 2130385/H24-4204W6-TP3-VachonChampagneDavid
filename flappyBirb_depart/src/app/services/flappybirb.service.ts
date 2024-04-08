import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, lastValueFrom, tap } from 'rxjs';
import { Score } from '../models/score';

@Injectable({
  providedIn: 'root'
})
export class FlappyBirbService {
  baseUrl: string = "http://localhost:7065/api/";

  constructor(private http: HttpClient) { }

  async Register(username: string, email: string, password: string, passwordConfirm : string): Promise<void> {
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Users/Register", { username, email, password, passwordConfirm }));
    console.log(x);
  }

  async Login(username: string, password: string): Promise<void> {
    let token = localStorage.getItem("token");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Users/Login", { username, password }, httpOptions));
    localStorage.setItem('token', x.token);
    console.log(x);
  }

  async GetPublicScores(): Promise<Score[]> {
    return await lastValueFrom(this.http.get<any>(this.baseUrl + "Scores/GetPublicScores"));
  }

  async GetMyScores(): Promise<Score[]> {
    return await lastValueFrom(this.http.get<any>(this.baseUrl + "Scores/GetMyScores"));
  }
  
  async ChangeScoreVisibility(scoreid : number): Promise<Observable<any>> {
    return this.http.put(this.baseUrl + "Scores/ChangeScoreVisibility/" + scoreid, { scoreid })
  }

  async PostScore(score : string, time: string): Promise<void> {
    let x = await this.http.post<any>(this.baseUrl + "Scores/PostScore", { score, time });
    console.log(x);
  }

}
