import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';
import { FlappyBirbService } from '../services/flappybirb.service';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {

  myScores : Score[] = [];
  publicScores : Score[] = [];
  userIsConnected : boolean = false;

  constructor(private flappyBirbService: FlappyBirbService) { }

  async ngOnInit() {
    this.userIsConnected = sessionStorage.getItem("token") !== null;
    console.log(localStorage.getItem("token"));
    this.userIsConnected = this.flappyBirbService.isAuthenticated;

    await this.loadPublicScores();
    await this.loadMyScores();
  }

  async changeScoreVisibility(score : Score){
    try {
      await this.flappyBirbService.ChangeScoreVisibility(score.id);
    } catch (error) {
      console.error("Error changing score visibility", error);
    }
    window.location.reload();
  }


  async loadPublicScores() {
    try {
      this.publicScores = await this.flappyBirbService.GetPublicScores();
      console.log(this.publicScores);
    } catch (error) {
      console.error("Error loading public scores:", error);
    }
  }

  async loadMyScores() {
    try {
      this.myScores = await this.flappyBirbService.GetMyScores();
      console.log(this.myScores);
    } catch (error) {
      console.error("Error loading my scores:", error);
    }
  }


}
