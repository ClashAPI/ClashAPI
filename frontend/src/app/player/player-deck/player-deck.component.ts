import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-player-deck',
  templateUrl: './player-deck.component.html',
  styleUrls: ['./player-deck.component.css']
})
export class PlayerDeckComponent implements OnInit {
  @Input() playerDeck: any;
  object = Object;

  constructor() { }

  ngOnInit() {
  }

  getMaxCardCountForLevel(level: number, rarity: string) {
    if (rarity === 'legendary') {
      if (level === 1) {
        return 2;
      }
      if (level === 2) {
        return 4;
      }
      if (level === 3) {
        return 10;
      }
      if (level === 4) {
        return 20;
      }
    }
    if (rarity === 'epic') {
      if (level === 1) {
        return 2;
      }
      if (level === 2) {
        return 4;
      }
      if (level === 3) {
        return 10;
      }
      if (level === 4) {
        return 20;
      }
      if (level === 5) {
        return 50;
      }
      if (level === 6) {
        return 100;
      }
      if (level === 7) {
        return 200;
      }
    }
    if (rarity === 'rare') {
      if (level === 1) {
        return 2;
      }
      if (level === 2) {
        return 4;
      }
      if (level === 3) {
        return 10;
      }
      if (level === 4) {
        return 20;
      }
      if (level === 5) {
        return 50;
      }
      if (level === 6) {
        return 100;
      }
      if (level === 7) {
        return 200;
      }
      if (level === 8) {
        return 400;
      }
      if (level === 9) {
        return 800;
      }
      if (level === 10) {
        return 1000;
      }
    }
    if (rarity === 'common') {
      if (level === 1) {
        return 2;
      }
      if (level === 2) {
        return 4;
      }
      if (level === 3) {
        return 10;
      }
      if (level === 4) {
        return 20;
      }
      if (level === 5) {
        return 50;
      }
      if (level === 6) {
        return 100;
      }
      if (level === 7) {
        return 200;
      }
      if (level === 8) {
        return 400;
      }
      if (level === 9) {
        return 800;
      }
      if (level === 10) {
        return 1000;
      }
      if (level === 11) {
        return 2000;
      }
      if (level === 12) {
        return 5000;
      }
    }
  }

  isReadyForUpgrade(level: number, count: number, rarity: string) {
    return count >= this.getMaxCardCountForLevel(level, rarity);
  }
}
