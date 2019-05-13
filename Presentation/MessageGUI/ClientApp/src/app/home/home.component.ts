import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public messages: Message[];

  constructor(http: HttpClient, @Inject('API_URL') apiHost: string) {

    http.get<Message[]>(apiHost+'api/message').subscribe(result => {
      this.messages = result;
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
