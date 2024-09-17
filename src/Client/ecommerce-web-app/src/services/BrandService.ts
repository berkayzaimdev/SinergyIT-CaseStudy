import axios, { AxiosResponse } from "axios";
import { Brand } from "../types/Brand";
import { BASE_API_URL } from "../utils/constants";

const API_URL = BASE_API_URL + ":6060/";

interface ApiResponse {
  brands: Brand[];
}

class BrandService {
  public static async getBrands(): Promise<Brand[]> {
    try {
      const response: AxiosResponse<ApiResponse> = await axios.get(
        API_URL + "brands/"
      );
      return response.data.brands;
    } catch (error) {
      console.error("Error fetching brands:", error);
      throw new Error("Unable to fetch brand");
    }
  }
}

export default BrandService;
