import { Button, Col, Row } from "react-bootstrap";
import { FaTrash } from "react-icons/fa";
import { ShoppingCartItemProps } from "../../types/ShoppingCartItemProps";

const ShoppingCartItem = ({
  id,
  name,
  brandName,
  image,
  quantity,
}: ShoppingCartItemProps) => {
  return (
    <Row
      lg={10}
      className="display-7 mt-4 border-top border-bottom py-1"
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
      <Col lg={2} className="text-center d-flex align-items-center">
        <Button
          variant="outline-primary"
          className="rounded-circle"
          style={{ width: "40px", height: "40px" }} // Genişlik ve yükseklik ayarı
          onClick={() => {}}
        >
          -
        </Button>
        <span className="mx-3">{quantity}</span>
        <Button
          variant="outline-primary"
          className="rounded-circle"
          style={{ width: "40px", height: "40px" }} // Genişlik ve yükseklik ayarı
          onClick={() => {}}
        >
          +
        </Button>
      </Col>
      <Col lg={2} className="text-center d-flex align-items-center">
        {}
        100 TL
      </Col>
      <Col lg={2} className="text-center d-flex align-items-center">
        <Button
          variant="danger"
          onClick={() => {
            /* Ürünü sil */
          }}
        >
          <FaTrash />
        </Button>
      </Col>
    </Row>
  );
};

export default ShoppingCartItem;
