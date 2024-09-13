import axios, { AxiosResponse } from "axios";
import { Product } from "../types/Product";

const API_URL = "https://localhost:6060/";

interface ApiResponse {
  products: Product[];
  // Diğer yanıt alanlarını buraya ekleyin
}

class CatalogService {
  public static async getProducts(): Promise<Product[]> {
    try {
      const response: AxiosResponse<ApiResponse> = await axios.get(API_URL);
      // response.data.products, ApiResponse içindeki products alanını ifade eder
      return response.data.products;
    } catch (error) {
      console.error("Error fetching products:", error);
      throw new Error("Unable to fetch products");
    }
  }
}

export default CatalogService;
