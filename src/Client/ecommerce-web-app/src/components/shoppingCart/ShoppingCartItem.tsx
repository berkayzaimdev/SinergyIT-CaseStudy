import { Button, Col, Row } from "react-bootstrap";
import { FaTrash } from "react-icons/fa";
import { ShoppingCartItemProps } from "../../types/ShoppingCartItemProps";
import BasketService from "../../services/BasketService";
import UserService from "../../services/UserService";

const ShoppingCartItem = ({
  id,
  name,
  image,
  quantity,
}: ShoppingCartItemProps) => {
  return (
    <Row
      lg={11}
      className="display-8 mt-4 border-top border-bottom py-1"
      style={{ fontSize: "20px" }}
    >
      <Col lg={6} className="d-flex align-items-center">
        <img
          src={image}
          alt={name}
          style={{ width: "250px", marginRight: "15px" }}
        />
        <div>
          <div>sdasdasdasdasdasd</div>
        </div>
      </Col>
      <Col lg={3} className="d-flex justify-content-center align-items-center">
        <Button
          variant="outline-primary"
          className="rounded-circle"
          style={{ width: "40px", height: "40px" }}
          onClick={async () => {
            const token = await UserService.getToken();

            if (token === null) {
              console.error("check your token");
            } else {
              await BasketService.addToBasket(id, token);
            }
          }}
        >
          +
        </Button>
        <span className="mx-3">{quantity}</span>
        <Button
          variant="outline-primary"
          className="rounded-circle"
          style={{ width: "40px", height: "40px" }}
          onClick={async () => {
            const token = await UserService.getToken();

            if (token === null) {
              console.error("check your token");
            } else {
              await BasketService.removeFromBasket(id, token);
            }
          }}
        >
          -
        </Button>
      </Col>
      <Col lg={1} className="text-center d-flex align-items-center">
        {}
        100 TL
      </Col>
      <Col
        lg={2}
        className="text-center justify-content-center d-flex align-items-center"
      >
        <Button variant="danger" onClick={() => {}}>
          <FaTrash />
        </Button>
      </Col>
    </Row>
  );
};

export default ShoppingCartItem;
