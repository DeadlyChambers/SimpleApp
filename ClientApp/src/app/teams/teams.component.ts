import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html'
})
export class TeamsComponent {
  public teams: ITeam[] = null;
  public createTeam : ITeam = new ITeam();
public httpClient : HttpClient;
public apiUrl : string;
public conferences = [
  {id: 0, name: "AFC"},
  {id: 1, name: "NFC"},
];
public divisions = [
  {id: 0, name: "North"},
  {id: 1, name: "South"},
  {id: 2, name: "East"},
  {id: 3, name: "West"}
];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.httpClient = http;
    this.apiUrl = apiUrl;

    http.get<ITeam[]>(apiUrl + 'teams').subscribe(result => {
      this.teams = result;
    }, error => console.error(error));
  }

  public create(){
   this.httpClient.post<ITeam>(this.apiUrl + '/teams/create', this.createTeam).subscribe(result =>  {
      this.teams.push(result)
   }, error => console.error(error));
  }
}



class ITeam {
  teamid: number = 1;
  name: string = '';
  conference: string = '0';
  division: string = '0';
  location: string ='';
  createdDate: string ='';
}
