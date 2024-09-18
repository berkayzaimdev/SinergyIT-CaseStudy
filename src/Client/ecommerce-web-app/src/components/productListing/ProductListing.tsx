import { Col, Pagination, Row } from "react-bootstrap";
import { useEffect, useState } from "react";
import { Product } from "../../types/Product";
import ProductService from "../../services/ProductService";
import ProductCard from "./ProductCard";
import PaginationArea from "./Pagination";

interface ProductListingProps {
  brandId?: string | undefined;
}

function ProductListing({ brandId }: ProductListingProps) {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize] = useState<number>(6);
  const [totalPages, setTotalPages] = useState<number>(0);

  const fetchProducts = async (page: number, pageSize: number) => {
    try {
      let data;

      if (brandId !== undefined) {
        data = await ProductService.getProductsByBrandId(
          brandId,
          page,
          pageSize
        );
      } else {
        data = await ProductService.getProducts(page, pageSize);
      }

      setTotalPages(data.paginationResponse.totalPages);
      setProducts(data.products);
    } catch (err) {
      setError("Error fetching products");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProducts(currentPage, pageSize);
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  const chunkArray = (array: Product[], size: number): Product[][] => {
    const result: Product[][] = [];
    for (let i = 0; i < array.length; i += size) {
      result.push(array.slice(i, i + size));
    }
    return result;
  };

  const productChunks = chunkArray(products, 3);

  return (
    <>
      {productChunks.map((chunk, index) => (
        <Row key={index} className="mt-4">
          {chunk.map((product) => (
            <Col
              key={product.id}
              md={4}
              className="d-flex justify-content-center mb-4"
            >
              <ProductCard
                id={product.id}
                title={product.name}
                price={product.price}
                brandName={product.brandName}
                brandId={product.brandId}
              />
            </Col>
          ))}
        </Row>
      ))}

      {totalPages ? (
        <PaginationArea
          currentPage={currentPage}
          totalPages={totalPages}
          onPageChange={(page: number) => {
            setCurrentPage(page);
            fetchProducts(page, pageSize);
          }}
        />
      ) : (
        <></>
      )}
    </>
  );
}

export default ProductListing;
