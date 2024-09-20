import { Button, Col, Container, Row } from "react-bootstrap";
import ShoppingCartItem from "../components/shoppingCart/ShoppingCartItem";
import Header from "../components/header/Header";
import { useEffect, useState } from "react";
import { ShoppingCartItemProps } from "../types/ShoppingCartItemProps";
import BasketService from "../services/BasketService";
import UserService from "../services/UserService";

const cartItems = [
  {
    id: "1",
    name: "Product 1",
    image: "https://placehold.co/100x100",
    quantity: 1,
  },
  {
    id: "2",
    name: "Product 2",
    image: "https://placehold.co/100x100",
    quantity: 2,
  },
];

function ShoppingCart() {
  const [cartItems, setCartItems] = useState<ShoppingCartItemProps[]>([]);

  useEffect(() => {
    const fetchCartItems = async () => {
      const token = await UserService.getToken();

      if (!token) {
        throw new Error("Unable to fetch basket");
      }

      const items = await BasketService.getUserBasket(token);
      setCartItems(items);
    };

    fetchCartItems();
  }, []);

  return (
    <>
      <Header />
      <Container>
        <Row className="mt-4">
          <Col lg={8}>
            {cartItems.map((item) => (
              <ShoppingCartItem
                key={item.id}
                id={item.id}
                name={item.name}
                image={item.image}
                quantity={item.quantity}
              />
            ))}
          </Col>
          <Col
            lg={4}
            className="justify-content-center d-flex flex-column align-items-center"
            style={{ height: "550px" }}
          >
            <h2>Toplam Fiyat: {5}$</h2>
            <Button
              variant="primary"
              size="lg"
              onClick={() => {
                /* Siparişi tamla */
              }}
            >
              Set Order
            </Button>
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default ShoppingCart;
