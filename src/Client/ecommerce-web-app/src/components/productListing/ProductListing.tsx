import { Col, Row } from "react-bootstrap";
import { useEffect, useState } from "react";
import { Product } from "../../types/Product";
import ProductService from "../../services/ProductService";
import ProductCard from "./ProductCard";

interface ProductListingProps {
  brandId?: string | undefined;
}

function ProductListing({ brandId }: ProductListingProps) {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        let data;
        if (brandId !== undefined) {
          console.log("brandid:", brandId);
          data = await ProductService.getProductsByBrandId(brandId);
        } else {
          data = await ProductService.getProducts();
        }

        setProducts(data);
      } catch (err) {
        setError("Error fetching products");
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
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
            <Col key={product.id} md={4} className="mb-4">
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
    </>
  );
}

export default ProductListing;
