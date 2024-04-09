import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';
import { FlappyBirbService } from '../services/flappybirb.service';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {

  myScores: Score[] = [];
  publicScores: Score[] = [];
  userIsConnected: boolean = false;

  constructor(private flappyBirbService: FlappyBirbService) { }

  async ngOnInit() {
    this.userIsConnected = sessionStorage.getItem("token") !== null;
    this.userIsConnected = this.flappyBirbService.isAuthenticated;
    if (this.userIsConnected) {
      console.log("Utilisateur connecté : " + localStorage.getItem("username"));
    }
    else {
      console.log("Aucun utilisateur connecté.");
    }

    await this.loadPublicScores();
    if (this.userIsConnected) {
      await this.loadMyScores();
    }
  }

  async changeScoreVisibility(score: Score) {
    try {
      await this.flappyBirbService.ChangeScoreVisibility(score.id);
      console.log("La visibilité du score avec l'id " + score.id + " a été changée");
      setTimeout(() => {
        window.location.reload();
      }, 150);
    } catch (error) {
      console.error("Error changing score visibility", error);
    }
  }
  
  async loadPublicScores() {
    try {
      this.publicScores = await this.flappyBirbService.GetPublicScores();
    } catch (error) {
      console.error("Error loading public scores:", error);
    }
  }

  async loadMyScores() {
    try {
      this.myScores = await this.flappyBirbService.GetMyScores();
    } catch (error) {
      console.error("Error loading my scores:", error);
    }
  }


}
