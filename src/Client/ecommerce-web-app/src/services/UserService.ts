import { jwtDecode } from "jwt-decode";

interface DecodedToken {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"?: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"?: string;
  exp?: number;
  iss?: string;
  aud?: string;
}

class UserService {
  public static async getUserName(accessToken: string): Promise<string | null> {
    try {
      const decoded = jwtDecode<DecodedToken>(accessToken);

      if (
        decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
      ) {
        return decoded[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      } else {
        console.warn("Name not found in token");
      }
    } catch (error) {
      console.error("Error decoding token:", error);
    }
    return null;
  }
}

export default UserService;
