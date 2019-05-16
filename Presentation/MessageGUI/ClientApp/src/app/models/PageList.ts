
import { Message } from '../models';

export class PageList {
  items: Message[];
  page: number;
  tageSize: number;
  totalCount: number;
  totalPages: number;
}
