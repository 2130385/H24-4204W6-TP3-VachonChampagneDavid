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
    await this.loadPublicScores();
  }

  async changeScoreVisibility(score : Score){

  }


  async loadPublicScores() {
    try {
      this.publicScores = await this.flappyBirbService.GetPublicScores();
      console.log(this.publicScores);
    } catch (error) {
      console.error("Error loading public scores:", error);
    }
  }


}
