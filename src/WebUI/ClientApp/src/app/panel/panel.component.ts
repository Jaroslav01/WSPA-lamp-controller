import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.scss']
})
export class PanelComponent implements OnInit {

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
  }

  public on(){
    this.http.post<any>('/api/mqtt/Publish', {data: "1", topic: "WSPA_ESP01/Relay1"}).subscribe(() => {

    });
  }

  off() {
    this.http.post<any>('/api/mqtt/Publish', {data: "0", topic: "WSPA_ESP01/Relay1"}).subscribe(() => {

    });
  }
}
