import { Button, Col, Row } from "react-bootstrap";
import { FaTrash } from "react-icons/fa";
import { ShoppingCartItemProps } from "../../types/ShoppingCartItemProps";

const ShoppingCartItem = (item: ShoppingCartItemProps) => {
  return (
    <Row className="mt-4">
      <Col>
        <img src={item.image} alt={item.name} style={{ width: "100px" }} />
      </Col>
      <Col>{item.name}</Col>
      <Col>{item.name}</Col>
      <Col>
        {" "}
        <Button variant="secondary" onClick={() => {}}>
          -
        </Button>
        <span className="mx-2">{item.quantity}</span>
        <Button variant="secondary" onClick={() => {}}>
          +
        </Button>
      </Col>
      <Col>
        {" "}
        <Button variant="danger" onClick={() => {}}>
          <FaTrash />
        </Button>
      </Col>
    </Row>
  );
};

export default ShoppingCartItem;
