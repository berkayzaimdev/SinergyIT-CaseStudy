import React from "react";
import { Col, Container, Row, Table } from "react-bootstrap";
import ShoppingCartItem from "../components/shoppingCart/ShoppingCartItem";
import Header from "../components/header/Header";

const cartItems = [
  {
    id: "1",
    name: "Product 1",
    brandName: "Brand A",
    image: "https://placehold.co/100x100",
    quantity: 1,
  },
  {
    id: "2",
    name: "Product 2",
    brandName: "Brand B",
    image: "https://placehold.co/100x100",
    quantity: 2,
  },
];

const ShoppingCart = () => {
  return (
    <>
      <Header />
      <Container>
        <Row>
          <Col>Image</Col>
          <Col>Image</Col>
          <Col>Image</Col>
          <Col>Image</Col>
          <Col>Image</Col>
        </Row>
        {cartItems.map((item) => (
          <ShoppingCartItem
            key={item.id}
            id={item.id}
            name={item.name}
            brandName={item.brandName}
            image={item.image}
            quantity={item.quantity}
          />
        ))}
      </Container>
    </>
  );
};

export default ShoppingCart;
