import { Button, Container, Form } from "react-bootstrap";
import Header from "../components/Header";
import { ChangeEvent, FormEvent, useState } from "react";

interface FormData {
  username: string;
  password: string;
}

function Login() {
  const [formData, setFormData] = useState<FormData>({
    username: "",
    password: "",
  });

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    var errorMessage = "";

    Object.entries(formData).forEach(([key, value]) => {
      if (value.trim() === "" || value == null) {
        console.log(`${key} değeri boş bırakılamaz!`);
        errorMessage += `${key} değeri boş bırakılamaz!\n`;
      }
    });

    if (!(errorMessage.trim() === "" || errorMessage == null)) {
      alert(errorMessage);
      e.preventDefault();
    } else {
      e.preventDefault();
    }
  };

  return (
    <>
      <Header></Header>
      <Container className="d-flex justify-content-center align-items-center mt-5">
        <Form
          onSubmit={handleSubmit}
          style={{ maxWidth: "500px", width: "100%" }}
        >
          <Form.Group className="mb-3" controlId="username">
            <Form.Label>Username</Form.Label>
            <Form.Control
              type="text"
              name="username"
              placeholder="Enter username"
              value={formData.username}
              onChange={handleChange}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="password">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              name="password"
              placeholder="Password"
              value={formData.password}
              onChange={handleChange}
            />
          </Form.Group>

          <Button variant="primary" type="submit">
            Login
          </Button>
        </Form>
      </Container>
    </>
  );
}

export default Login;
