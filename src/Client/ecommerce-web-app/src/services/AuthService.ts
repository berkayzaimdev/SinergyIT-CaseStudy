import axios, { AxiosResponse } from "axios";
import { BASE_API_URL } from "../utils/constants";

const API_URL = BASE_API_URL + ":6061";

export interface LoginResponse {
  accessToken: string;
}

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface RegisterRequest {
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
}

class AuthService {
  public static async login(request: LoginRequest): Promise<LoginResponse> {
    try {
      console.log(request);
      const response = await axios.post<LoginResponse>(
        `${API_URL}/auth/login/`,
        request
      );
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error) && error.response) {
        throw new Error(error.response.data);
      } else {
        throw new Error("An unexpected error occurred");
      }
    }
  }
}

export default AuthService;
