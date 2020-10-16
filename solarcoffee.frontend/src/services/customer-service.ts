import axios from 'axios';
import { ICustomer } from '@/types/Customer';
import { IServiceResponse } from '@/types/ServiceResponse';

export class CustomerService {
  API_URL = process.env.VUE_APP_API_URL;

  public async getCustomers() {
    const result = await axios.get<ICustomer[]>(`${this.API_URL}/customer`);
    return result.data;
  }

  public async addCustomer(customer: ICustomer) {
    const result = await axios.post<IServiceResponse<ICustomer>>(
      `${this.API_URL}/customer`,
      customer,
    );
    return result.data;
  }

  public async deleteCustomer(id: number) {
    const result = await axios.delete<IServiceResponse<boolean>>(
      `${this.API_URL}/customer/${id}`,
    );
    return result.data;
  }
}
