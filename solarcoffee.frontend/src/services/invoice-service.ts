import axios from 'axios';
import { IInvoice } from '@/types/Invoice';

export class InvoiceService {
  API_URL = process.env.VUE_APP_API_URL;

  public async makeNewInvoice(invoice: IInvoice) {
    invoice.createdOn = new Date();
    invoice.updatedOn = new Date();
    const result = await axios.post<boolean>(
      `${this.API_URL}/invoice`,
      invoice,
    );
    return result.data;
  }
}
