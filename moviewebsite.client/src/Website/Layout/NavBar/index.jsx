import { Container, Nav, NavDropdown, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";
import React, { useContext } from "react";
import "./NavBar.css";
import AuthContext from "../../User/AuthContext/Context";

function NavBar() {
  const authContext = useContext(AuthContext);
  const { isLoggedIn, signOut, roles } = authContext;
  const logOut = async () => {
    await signOut();
  };
  const roleMap = {
    UserT0: () => (
      <>
        <Nav.Link as={Link} to="/userinfo">
          UserInfo
        </Nav.Link>
        <Nav.Link onClick={logOut}>Logout</Nav.Link>
      </>
    ),
    Admin: () => (
      <>
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
        <Nav.Link onClick={logOut}>Logout</Nav.Link>
      </>
    ),
  };
  const renderAuthLinks = () => {
    const roleHandler = roleMap[roles] || (() => renderUnAuthLinks());
    return roleHandler();
  };
  const renderUnAuthLinks = () => {
    return (
      <>
        <Nav.Link as={Link} to="/login">
          Login
        </Nav.Link>
        <Nav.Link as={Link} to="/register">
          Register
        </Nav.Link>
      </>
    );
  };
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
              <Nav.Link as={Link} to="/">
                Home
              </Nav.Link>
              {isLoggedIn ? renderAuthLinks() : renderUnAuthLinks()}
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </header>
  );
}
export default NavBar;
