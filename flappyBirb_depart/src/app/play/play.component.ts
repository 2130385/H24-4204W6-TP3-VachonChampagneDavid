import { Component, OnDestroy, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { FlappyBirbService } from '../services/flappybirb.service';
import { Score } from '../models/score';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.css']
})
export class PlayComponent implements OnInit, OnDestroy {

  game: Game | null = null;
  scoreSent: boolean = false;
  isAuthenticated: boolean | undefined;

  constructor(private flappyBirbService: FlappyBirbService) { }

  ngOnDestroy(): void {
    // Ceci est crotté mais ne le retirez pas sinon le jeu bug.
    location.reload();
  }

  ngOnInit() {
    this.game = new Game();
    setTimeout(() => {
      this.isAuthenticated = this.flappyBirbService.isAuthenticated;
      if(this.isAuthenticated){
        console.log("Utilisateur connecté : " + localStorage.getItem("username"));
      } else {
        console.log("Aucun utilisateur connecté.");
      }
    }, 1000);
    
  }

  replay() {
    if (this.game == null) return;
    this.game.prepareGame();
    this.scoreSent = false;
  }

  async sendScore() {
    if (this.scoreSent) return;

    this.scoreSent = true;

    // ██ Appeler une requête pour envoyer le score du joueur ██
    // Le score est dans sessionStorage.getItem("score")
    // Le temps est dans sessionStorage.getItem("time")
    // La date sera choisie par le serveur
    const score = sessionStorage.getItem("score");
    const time = sessionStorage.getItem("time");
    try {
      if (localStorage.getItem("token") !== null) {
        if (score !== null && time !== null) {
          await this.flappyBirbService.PostScore(score, time);
        } else {
          console.error("Score or time is null.");
        }
      }
    } catch (error) {
      console.error("Error posting score", error);
    }


  }

}
