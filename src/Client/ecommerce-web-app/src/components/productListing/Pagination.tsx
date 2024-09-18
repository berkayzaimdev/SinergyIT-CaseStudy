import React from "react";
import { Container, Pagination } from "react-bootstrap";

interface PaginationAreaProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

const PaginationArea: React.FC<PaginationAreaProps> = ({
  currentPage,
  totalPages,
  onPageChange,
}) => {
  const handlePageChange = (page: number) => {
    onPageChange(page);
  };

  const items = [];
  for (let number = 1; number <= totalPages; number++) {
    items.push(
      <Pagination.Item
        key={number}
        active={number === currentPage}
        onClick={() => handlePageChange(number)}
      >
        {number}
      </Pagination.Item>
    );
  }

  return (
    <Container className="d-flex justify-content-center mt-4">
      <Pagination size="lg">{items}</Pagination>
    </Container>
  );
};

export default PaginationArea;
