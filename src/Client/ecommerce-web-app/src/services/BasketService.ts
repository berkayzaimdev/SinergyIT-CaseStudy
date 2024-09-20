import axios, { AxiosResponse } from "axios";
import { BASE_API_URL } from "../utils/constants";
import { ShoppingCartItemProps } from "../types/ShoppingCartItemProps";

const API_URL = BASE_API_URL + ":6062";

export interface AddToBasketRequest {
  productId: string;
}

export interface RemoveFromBasketRequest {
  productId: string;
}

class BasketService {
  public static async getUserBasket(
    accessToken: string
  ): Promise<ShoppingCartItemProps[]> {
    try {
      const response: AxiosResponse<{ items: ShoppingCartItemProps[] }> =
        await axios.get(`${API_URL}/get-basket`, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        });
      console.log(response.data.items);
      return response.data.items;
    } catch (error) {
      console.error("Error fetching basket:", error);
      throw new Error("Unable to fetch basket");
    }
  }

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
      console.error("Error adding item :", error);
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
      console.error("Error removing item :", error);
      throw new Error("Unable to fetch brand");
    }
  }
}

export default BasketService;
