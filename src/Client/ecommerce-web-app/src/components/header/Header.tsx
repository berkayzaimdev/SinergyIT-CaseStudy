import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import BrandsList from "./BrandsList";
import AuthStatus from "./AuthStatus";

const Header: React.FC = () => {
  return (
    <Navbar expand="lg" bg="dark" variant="dark">
      <Container>
        <Navbar.Brand href="/">e-Commerce</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <BrandsList></BrandsList>
            <Nav.Link href="/mycart">Cart</Nav.Link>
          </Nav>
        </Navbar.Collapse>

        <Navbar.Collapse className="justify-content-end">
          <AuthStatus></AuthStatus>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Header;
