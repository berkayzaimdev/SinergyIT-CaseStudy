import React, { useState, ChangeEvent, FormEvent } from "react";
import { Alert, Button, Container, Form } from "react-bootstrap";
import Header from "../components/Header";

interface FormData {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  confirmPassword: string;
}

function Register() {
  const [formData, setFormData] = useState<FormData>({
    username: "",
    email: "",
    firstName: "",
    lastName: "",
    password: "",
    confirmPassword: "",
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

    if (!(formData.password === formData.confirmPassword)) {
      errorMessage += "Şifreler eşleşmiyor!";
    }

    if (!(errorMessage.trim() === "" || errorMessage == null)) {
      alert(errorMessage);
      e.preventDefault();
    } else {
      e.preventDefault();
    }
  };

  return (
    <>
      <Header />
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

          <Form.Group className="mb-3" controlId="email">
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              name="email"
              placeholder="Enter email"
              value={formData.email}
              onChange={handleChange}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="firstName">
            <Form.Label>First Name</Form.Label>
            <Form.Control
              type="text"
              name="firstName"
              placeholder="Enter first name"
              value={formData.firstName}
              onChange={handleChange}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="lastName">
            <Form.Label>Last Name</Form.Label>
            <Form.Control
              type="text"
              name="lastName"
              placeholder="Enter last name"
              value={formData.lastName}
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

          <Form.Group className="mb-3" controlId="confirmPassword">
            <Form.Label>Confirm Password</Form.Label>
            <Form.Control
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              value={formData.confirmPassword}
              onChange={handleChange}
            />
          </Form.Group>

          <Button variant="primary" type="submit">
            Register
          </Button>
        </Form>
      </Container>
    </>
  );
}

export default Register;
