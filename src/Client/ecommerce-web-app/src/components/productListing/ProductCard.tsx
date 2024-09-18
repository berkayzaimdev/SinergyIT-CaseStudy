import React from "react";
import { Button, Card, Container } from "react-bootstrap";
import { FaCartPlus } from "react-icons/fa";

interface ProductCardProps {
  id: string;
  title: string;
  price: number;
  brandName: string;
  brandId: string;
}

const ProductCard: React.FC<ProductCardProps> = ({
  id,
  title,
  price,
  brandName,
  brandId,
}) => {
  return (
    <Card style={{ width: "20rem" }}>
      <Card.Img variant="top" src="https://placehold.co/180x120" />
      <Card.Body>
        <Card.Title className="d-flex justify-content-center">
          {title}
        </Card.Title>
        <Card.Text>
          <b>Price:</b> ${price.toFixed(2)}
          <br />
          <b>Brand:</b> {brandName}
        </Card.Text>
        <Container className="d-flex justify-content-center mt-4">
          <Button variant="primary">
            <FaCartPlus /> Add to Cart
          </Button>
        </Container>
      </Card.Body>
    </Card>
  );
};

export default ProductCard;
