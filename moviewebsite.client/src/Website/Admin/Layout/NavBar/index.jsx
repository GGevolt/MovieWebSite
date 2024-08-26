import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import { Link } from "react-router-dom";
import React from "react";
import "./NavBar.css";

function NavBar() {
  return (
    <header>
      <Navbar collapseOnSelect expand="lg" className="navbar">
        <Container>
          <Navbar.Brand as={Link} to="/">
            Sodoki
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="responsive-navbar-nav" />
          <Navbar.Collapse id="responsive-navbar-nav">
            <Nav className="me-auto">
              <NavDropdown
                menuVariant="dark"
                title="CRUD Management"
                id="collapsible-nav-dropdown"
              >
                <NavDropdown.Item as={Link} to="/Admin/Category-Management">
                  Category
                </NavDropdown.Item>
                <NavDropdown.Item as={Link} to="/Admin/Film-Management">
                  Film
                </NavDropdown.Item>
              </NavDropdown>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </header>
  );
}
export default NavBar;
