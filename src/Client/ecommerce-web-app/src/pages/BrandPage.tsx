import { Container } from "react-bootstrap";
import { useParams } from "react-router-dom";
import Header from "../components/header/Header";
import ProductListing from "../components/productListing/ProductListing";

const BrandPage: React.FC = () => {
  const { brandId } = useParams();
  return (
    <>
      <Header></Header>
      <Container>
        <h1>{brandId}</h1>
        <ProductListing brandId={brandId}></ProductListing>
      </Container>
    </>
  );
};

export default BrandPage;
