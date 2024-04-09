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
    this.Login(username, password);
  }

  async Login(username: string, password: string): Promise<void> {
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Users/Login", { username, password }));
    localStorage.setItem('token', x.token);
    localStorage.setItem('username', username);
    this.isAuthenticated = true;
    console.log(x);
  }

  async GetPublicScores(): Promise<Score[]> {
    const response = await this.http.get<any>(this.baseUrl + "Scores/GetPublicScores").toPromise();
    const sortedScores = response.sort((a : Score, b :  Score) => b.scoreValue - a.scoreValue);
    const top10Scores = sortedScores.slice(0, 10);
    return top10Scores;
  }

  async GetMyScores(): Promise<Score[]> {
    const response = await this.http.get<any>(this.baseUrl + "Scores/GetMyScores").toPromise();
    return response;
  }
  
  async ChangeScoreVisibility(scoreid : number): Promise<void> {
    let x = await lastValueFrom(this.http.put(this.baseUrl + "Scores/ChangeScoreVisibility/" + scoreid, { scoreid }));
  }

  async PostScore(scoreValue: string, timeInSeconds: string): Promise<void> {
    let x = await lastValueFrom(this.http.post<any>(this.baseUrl + "Scores/PostScore", { scoreValue, timeInSeconds }));
    console.log("Score de " + scoreValue + " ajouté au nom de l'utilisateur courant : " + localStorage.getItem("username"));
  }
}
