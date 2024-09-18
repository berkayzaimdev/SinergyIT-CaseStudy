import { Container } from "react-bootstrap";
import Pagination from "react-bootstrap/Pagination";

const PaginationArea: React.FC = () => {
  return (
    <Container className="d-flex justify-content-center mt-4">
      <Pagination size="lg">
        <Pagination.Item active>{1}</Pagination.Item>
        <Pagination.Item>{2}</Pagination.Item>
        <Pagination.Item>{3}</Pagination.Item>
      </Pagination>
    </Container>
  );
};

export default PaginationArea;
