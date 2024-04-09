import { Component } from '@angular/core';
import { FlappyBirbService } from './services/flappybirb.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  toggleLogout : boolean = true;

  constructor(private flappyBirbService: FlappyBirbService) { }

  logout(){

    this.flappyBirbService.isAuthenticated = false;
    // ██ Supprimer le token juste ici ! ██
    localStorage.removeItem('token');
    window.location.reload();


    let darkScreen : HTMLElement | null = document.querySelector("#darkScreen");
    if(darkScreen == null) return;
    darkScreen.style.visibility = this.toggleLogout ? "visible" : "hidden";

    let logoutBox : HTMLElement | null = document.querySelector("#logoutBox");
    if(logoutBox == null) return;
    logoutBox.style.opacity = this.toggleLogout ? "1" : "0";
    logoutBox.style.top = this.toggleLogout ? "50%" : "48%";

    this.toggleLogout = !this.toggleLogout;

    sessionStorage.removeItem("token");
  }

}
