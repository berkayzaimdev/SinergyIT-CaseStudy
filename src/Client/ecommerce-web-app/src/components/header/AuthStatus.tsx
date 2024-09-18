import { useState, useEffect } from "react";
import { Button, Nav } from "react-bootstrap";
import { FaUser, FaSignOutAlt, FaSignInAlt, FaUserPlus } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import UserService from "../../services/UserService";

const AuthStatus: React.FC = () => {
  const [login, setLogin] = useState<boolean>(false);
  const [userName, setuserName] = useState<string | null>(null);

  const navigate = useNavigate();

  async function checkLoginStatus() {
    const token = localStorage.getItem("accessToken");

    if (token) {
      setLogin(true);
      try {
        setuserName(await UserService.getUserName(token));
        console.log("User name:", userName);
      } catch (error) {
        console.error("Error fetching user name:", error);
      }
    }
  }

  useEffect(() => {
    checkLoginStatus();
  }, []);

  return (
    <Nav>
      {login ? (
        <>
          <Nav.Link href="/">
            <label>{userName}</label>
            <FaUser className="mx-1" />
          </Nav.Link>

          <Nav.Link
            onClick={() => {
              localStorage.removeItem("accessToken");
              navigate(0);
            }}
            className="mx-1"
          >
            Logout
            <FaSignOutAlt className="mx-1" />
          </Nav.Link>
        </>
      ) : (
        <>
          <Nav.Link href="/login">
            Login
            <FaSignInAlt className="mx-1" />
          </Nav.Link>

          <Nav.Link href="/register" className="mx-1">
            Register
            <FaUserPlus className="mx-1" />
          </Nav.Link>
        </>
      )}
    </Nav>
  );
};

export default AuthStatus;
