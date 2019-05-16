import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Message, PageList } from '../../models';
import { first } from 'rxjs/operators';
import { MessageService } from '../../services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public messages: Message[];

  constructor(http: HttpClient, @Inject('API_URL') apiHost: string, private messageService: MessageService) {
    messageService.getMessages(1, 10).subscribe(result => {
      this.messages = result.items;
    }, error => console.error(error));
  }
}
