import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { PageList } from '../models';

@Injectable({ providedIn: 'root' })
export class MessageService {
  private apiHost: string;

  constructor(private http: HttpClient, @Inject('API_URL') apiHost: string) {
    this.apiHost = apiHost;
  }

  getMessages(page, pageSize) {
    return this.http.get<PageList>(this.apiHost + 'api/Message?page=' + page + '&pageSize=' + pageSize);
  }
}
