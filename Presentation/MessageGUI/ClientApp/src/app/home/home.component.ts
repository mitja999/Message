import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public messages: Message[];

  constructor(http: HttpClient, @Inject('API_URL') apiHost: string) {

    http.get<PageList>(apiHost + 'api/message?page=0&pageSize=10').subscribe(result => {
      this.messages = result.items;
    }, error => console.error(error));
  }
}
interface Message {
  id: number;
  name: string;
  template: string;
  type: string;
  state: string;
}

interface PageList {
  items: Message[],
  page: number,
  tageSize: number,
  totalCount: number,
  totalPages: number
}
