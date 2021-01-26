import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html'
})
export class TeamsComponent {
  public teams: ITeam[] = null;
  public createTeam : ITeam = new ITeam();
  public pagedRecords : PageList = new PageList();
public httpClient : HttpClient;
public apiUrl : string;
public conferences = [
  {id: 0, name: "AFC"},
  {id: 1, name: "NFC"}
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
    this.load(this.pagedRecords.currentPage);
  }
  

  public clickprev(){
    if(this.pagedRecords.hasPreviousPage)
      this.load(this.pagedRecords.currentPage - 1);
  }
  
  public clicknext(){
    if(this.pagedRecords.hasNextPage)
      this.load(this.pagedRecords.currentPage + 1);
  }

public create(){
   this.httpClient.post<ITeam>(this.apiUrl + 'teams', this.createTeam).subscribe(result =>  {
      this.teams.push(result)
      this.pagedRecords.totalRecords++;
   }, error => console.error(error));
}

public load(pageId: number){
  if(pageId == 0) {
    pageId = 1;
  }
  
  this.httpClient.get<PageList>(this.apiUrl + 'teams?id='+pageId).subscribe(result => {
    this.pagedRecords = result;
      this.teams = result.items;
    }, error => console.error(error));
}

  public deleteRow(id: number){
    this.httpClient.delete(this.apiUrl+'teams/'+id).subscribe(result => {
        const index = this.teams.findIndex(team => team.id === id);
        this.teams.slice(index, 1);
        this.pagedRecords.totalRecords--;
        if(this.pagedRecords.totalRecords % this.pagedRecords.pageSize === 0) {
            this.clickprev();
        }        
    }, error => console.error(error));
  }

  public getDivis(id: number) : string {
    return this.divisions[id].name;
  }

  public getConf(id: number) : string {
    return this.conferences[id].name;
  }
}

class PageList{
hasPreviousPage: boolean = false;
hasNextPage: boolean = false;
pageSize: number = 4;
currentPage: number = 1;
totalRecords: number = 0;
totalPages:number = 1;
items: ITeam[];
}

class ITeam {
  id: number = 0;
  name: string = '';
  conference: string = '0';
  division: string = '0';
  location: string ='';
  createdDate: Date = new Date();
}
