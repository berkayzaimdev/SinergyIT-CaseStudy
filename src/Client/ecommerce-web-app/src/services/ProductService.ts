import axios, { AxiosResponse } from "axios";
import { Product } from "../types/Product";
import { BASE_API_URL } from "../utils/constants";
import { PaginationResponse } from "../types/PaginationResponse";

const API_URL = BASE_API_URL + ":6060/";

interface ApiResponse {
  products: Product[];
  paginationResponse: PaginationResponse;
}

class ProductService {
  public static async getProducts(
    page: number,
    pageSize: number
  ): Promise<ApiResponse> {
    try {
      const response: AxiosResponse<ApiResponse> = await axios.get(
        `${API_URL}?pageNumber=${page}&pageSize=${pageSize}`
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching products:", error);
      throw new Error("Unable to fetch products");
    }
  }

  public static async getProductsByBrandId(
    brandId: string,
    page: number,
    pageSize: number
  ): Promise<ApiResponse> {
    try {
      console.log("brandid:", brandId);
      const response: AxiosResponse<ApiResponse> = await axios.get(
        `${API_URL}brands/${brandId}/products?pageNumber=${page}&pageSize=${pageSize}`
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching products:", error);
      throw new Error("Unable to fetch products");
    }
  }
}

export default ProductService;
