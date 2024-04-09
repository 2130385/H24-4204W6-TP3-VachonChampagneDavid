import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, lastValueFrom, tap } from 'rxjs';
import { Score } from '../models/score';

@Injectable({
  providedIn: 'root'
})
export class FlappyBirbService {
  baseUrl: string = "http://localhost:7065/api/";
  isAuthenticated: boolean = false;

  constructor(private http: HttpClient) { 
    this.isAuthenticated = !!localStorage.getItem('token');
  }

  async Register(username: string, email: string, password: string, passwordConfirm : string): Promise<void> {
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Users/Register", { username, email, password, passwordConfirm }));
    this.isAuthenticated = true;
    console.log(x);
  }

  async Login(username: string, password: string): Promise<void> {
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Users/Login", { username, password }, this.getHttpOptions()));
    localStorage.setItem('token', x.token);
    this.isAuthenticated = true;
    console.log(x);
  }

  async GetPublicScores(): Promise<Score[]> {
    const response = await this.http.get<any>(this.baseUrl + "Scores/GetPublicScores", this.getHttpOptions()).toPromise();
    return response;
  }

  async GetMyScores(): Promise<Score[]> {
    const response = await this.http.get<any>(this.baseUrl + "Scores/GetMyScores", this.getHttpOptions()).toPromise();
    return response;
  }
  
  async ChangeScoreVisibility(scoreid : number): Promise<void> {
    let x = await lastValueFrom(this.http.put(this.baseUrl + "Scores/ChangeScoreVisibility/" + scoreid, { scoreid }, this.getHttpOptions()));
    console.log("La visibilité du score avec l'id " + scoreid + " a été changée");
  }

  async PostScore(scoreValue: string, timeInSeconds: string): Promise<void> {
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Scores/PostScore", { scoreValue, timeInSeconds }, this.getHttpOptions()));
    console.log("Score de " + scoreValue + " ajouté au nom de l'utilisateur courant");
  }

  private getHttpOptions(): { headers: HttpHeaders } {
    const token = localStorage.getItem("token");
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    });
    return { headers };
  }
}
