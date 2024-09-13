import axios, { AxiosResponse } from "axios";
import { Product } from "../types/Product";

const API_URL = "https://localhost:6060/";

class CatalogService {
  public static async getProducts(): Promise<Product[]> {
    try {
      const response: AxiosResponse<Product[]> = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      console.error("Error fetching products:", error);
      throw new Error("Unable to fetch products");
    }
  }
}

export default CatalogService;
