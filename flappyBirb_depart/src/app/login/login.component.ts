import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FlappyBirbService } from '../services/flappybirb.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide = true;

  registerUsername: string = "";
  registerEmail: string = "";
  registerPassword: string = "";
  registerPasswordConfirm: string = "";

  loginUsername: string = "";
  loginPassword: string = "";

  constructor(private flappyBirbService: FlappyBirbService, public route: Router) { }

  ngOnInit() {
  }

  async login() {
    try {
      await this.flappyBirbService.Login(this.loginUsername, this.loginPassword);
      // Redirection si la connexion a réussi :
      this.route.navigate(["/play"]);
    } catch (error) {
      console.error('login failed', error)
    }

  }

  async register() {
    try {
      await this.flappyBirbService.Register(this.registerUsername, this.registerEmail, this.registerPassword, this.registerPasswordConfirm)
      this.route.navigate(["/play"]);
    } catch (error) {
      console.error('registration failed', error)
    }

  }

}
