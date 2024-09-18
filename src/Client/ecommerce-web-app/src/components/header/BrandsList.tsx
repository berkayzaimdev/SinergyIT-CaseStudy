import { useState, useEffect } from "react";
import { NavDropdown } from "react-bootstrap";
import BrandService from "../../services/BrandService";
import { Brand } from "../../types/Brand";

const BrandsList: React.FC = () => {
  const [brands, setBrands] = useState<Brand[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchBrands = async () => {
      try {
        const data = await BrandService.getBrands();
        setBrands(data);
      } catch (err) {
        setError("Error fetching brands");
      } finally {
        setLoading(false);
      }
    };

    fetchBrands();
  }, []);

  return (
    <NavDropdown title="Brands" id="basic-nav-dropdown">
      {brands.map((brand) => (
        <NavDropdown.Item
          className="border"
          key={`${brand.id}`}
          href={`/${brand.id}/`}
        >
          {brand.name}
        </NavDropdown.Item>
      ))}
    </NavDropdown>
  );
};

export default BrandsList;
