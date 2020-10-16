import axios from 'axios';
import { IProduct } from '@/types/Product';

export class ProductService {
  API_URL = process.env.VUE_APP_API_URL;

  async archive(id: number) {
    const result = await axios.patch(`${this.API_URL}/product/${id}`);
    return result.data;
  }

  async save(product: IProduct) {
    const result = await axios.post(`${this.API_URL}/product`, product);
    return result.data;
  }
}
