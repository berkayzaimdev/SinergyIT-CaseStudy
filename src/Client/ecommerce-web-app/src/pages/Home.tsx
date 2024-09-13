import { Col, Container, Row } from "react-bootstrap";
import Header from "../components/Header";
import ProductCard from "../components/ProductCard";

const Home: React.FC = () => {
  return (
    <div className="home-container">
      <Header></Header>
      <Container>
        <Row className="mt-4">
          <Col>
            <ProductCard />
          </Col>
          <Col>
            <ProductCard />
          </Col>
          <Col>
            <ProductCard />
          </Col>
        </Row>
        <Row className="mt-4">
          <Col>
            <ProductCard />
          </Col>
          <Col>
            <ProductCard />
          </Col>
          <Col>
            <ProductCard />
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default Home;
