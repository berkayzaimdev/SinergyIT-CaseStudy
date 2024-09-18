import { Container } from "react-bootstrap";
import Header from "../components/header/Header";
import ProductListing from "../components/productListing/ProductListing";

const ProductsPage: React.FC = () => {
  return (
    <>
      <Header></Header>
      <Container>
        <ProductListing />
      </Container>
    </>
  );
};

export default ProductsPage;
