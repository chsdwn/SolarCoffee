import axios from 'axios';

export class ProductService {
  API_URL = process.env.VUE_APP_API_URL;

  async archive(id: number) {
    const result = await axios.patch(`${this.API_URL}/product/${id}`);
    return result.data;
  }
}
