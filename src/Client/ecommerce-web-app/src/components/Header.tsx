import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import { Brand } from "../types/Brand";
import BrandService from "../services/BrandService";
import { useState, useEffect } from "react";
import {
  FaLock,
  FaSign,
  FaSignInAlt,
  FaUser,
  FaUserPlus,
} from "react-icons/fa";

const Header: React.FC = () => {
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
    <Navbar expand="lg" bg="dark" variant="dark">
      <Container>
        <Navbar.Brand href="/">e-Commerce</Navbar.Brand>

        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <NavDropdown title="Brands" id="basic-nav-dropdown">
              {brands.map((brand) => (
                <>
                  <NavDropdown.Divider />
                  <NavDropdown.Item href={`/${brand.id}/`}>
                    {brand.name}
                  </NavDropdown.Item>
                </>
              ))}
            </NavDropdown>
            <Nav.Link href="#link">Cart</Nav.Link>
          </Nav>
        </Navbar.Collapse>

        <Navbar.Collapse className="justify-content-end">
          <Nav>
            <Nav.Link href="/login">
              Login
              <FaSignInAlt className="mx-1" />
            </Nav.Link>

            <Nav.Link href="/register" className="mx-1">
              Register
              <FaUserPlus className="mx-1" />
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Header;
