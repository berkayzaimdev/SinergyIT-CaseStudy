import axios, { AxiosResponse } from "axios";
import { BASE_API_URL } from "../utils/constants";

const API_URL = BASE_API_URL + ":6062";

export interface AddToBasketRequest {
  productId: string;
}

export interface RemoveFromBasketRequest {
  productId: string;
}

class BasketService {
  public static async addToBasket(
    productId: string,
    accessToken: string
  ): Promise<void> {
    try {
      const response: AxiosResponse<void> = await axios.post(
        `${API_URL}/add-to-basket/${productId}`,
        null,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );

      console.log(response.data);
    } catch (error) {
      console.error("Error fetching brands:", error);
      throw new Error("Unable to fetch brand");
    }
  }

  public static async removeFromBasket(
    productId: string,
    accessToken: string
  ): Promise<void> {
    try {
      const response: AxiosResponse<void> = await axios.post(
        `${API_URL}/remove-from-basket/`,
        { productId: productId },
        { headers: { Authorization: `Bearer ${accessToken}` } }
      );
    } catch (error) {
      console.error("Error fetching brands:", error);
      throw new Error("Unable to fetch brand");
    }
  }
}

export default BasketService;
