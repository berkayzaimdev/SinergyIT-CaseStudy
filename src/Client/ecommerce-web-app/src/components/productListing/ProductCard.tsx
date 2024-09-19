import React, { useState } from "react";
import { Button, Card, Container, Toast } from "react-bootstrap";
import { FaCartPlus } from "react-icons/fa";
import BasketService from "../../services/BasketService";

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
  const [showToast, setShowToast] = useState(false);
  const [toastMessage, setToastMessage] = useState("");

  const handleAddToCart = async () => {
    const token = localStorage.getItem("accessToken");

    if (!token) {
      setToastMessage("Please log in to add items to the cart.");
      setShowToast(true);
      return;
    }

    try {
      await BasketService.addToBasket(id, token);
      setToastMessage("Product added to cart!");
      setShowToast(true);
    } catch (error) {
      setToastMessage("Error adding product to cart.");
      setShowToast(true);
    }
  };

  return (
    <>
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
            <Button variant="primary" onClick={handleAddToCart}>
              <FaCartPlus /> Add to Cart
            </Button>
          </Container>
        </Card.Body>
      </Card>

      <Toast
        style={{ position: "absolute", top: "20px", right: "20px" }}
        onClose={() => setShowToast(false)}
        show={showToast}
        delay={3000}
        autohide
      >
        <Toast.Body>{toastMessage}</Toast.Body>
      </Toast>
    </>
  );
};

export default ProductCard;
