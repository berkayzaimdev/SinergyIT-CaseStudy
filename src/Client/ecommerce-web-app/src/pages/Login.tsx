import { Button, Container, Form } from "react-bootstrap";
import Header from "../components/Header";
import { ChangeEvent, FormEvent, useState } from "react";
import AuthService, { LoginRequest } from "../services/AuthService";
import { useNavigate } from "react-router-dom";

interface FormData {
  username: string;
  password: string;
}

function Login() {
  const [userName, setUserName] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const navigate = useNavigate();

  const handleSubmit = async () => {
    const request: LoginRequest = {
      userName,
      password,
    };

    try {
      const response = await AuthService.login(request);
      localStorage.setItem("accessToken", response.accessToken);
      alert("Giriş başarılı!");
      navigate("/");
    } catch (err) {
      if (err instanceof Error) {
        alert(err.message);
      }
    }
  };

  return (
    <>
      <Header></Header>
      <Container className="d-flex justify-content-center align-items-center mt-5">
        <Form style={{ maxWidth: "500px", width: "100%" }}>
          <Form.Group className="mb-3" controlId="username">
            <Form.Label>Username</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter username"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
              required
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="password">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </Form.Group>

          <Button variant="primary" type="button" onClick={handleSubmit}>
            Login
          </Button>
        </Form>
      </Container>
    </>
  );
}

export default Login;
