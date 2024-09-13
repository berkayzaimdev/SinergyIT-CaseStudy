import axios, { AxiosResponse } from "axios";
import { Brand } from "../types/Brand";

const API_URL = "https://localhost:6060/";

interface ApiResponse {
  brands: Brand[];
}

class BrandService {
  public static async getBrands(): Promise<Brand[]> {
    try {
      const response: AxiosResponse<ApiResponse> = await axios.get(API_URL);
      return response.data.brands;
    } catch (error) {
      console.error("Error fetching brands:", error);
      throw new Error("Unable to fetch brand");
    }
  }
}

export default BrandService;
