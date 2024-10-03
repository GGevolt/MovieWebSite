import { Container, Nav, Navbar, Row, Col } from "react-bootstrap";
import { Link } from "react-router-dom";
import React, { useContext } from "react";
import { Search, User, LogOut } from "lucide-react";
import "./NavBar.css";
import AuthContext from "../../Area/AuthContext/Context";

function NavBar() {
  const authContext = useContext(AuthContext);
  const { isLoggedIn, signOut, roles, userName } = authContext;
  const handleLogOut = async () => {
    await signOut();
  };
  const roleMap = {
    UserT0: () => (
      <>
        <Row>
          <Col xs="auto">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
          </Col>
          <Col>
            <Nav.Link as={Link} to="/user/memberships">
            Memberships
            </Nav.Link>
          </Col>
        </Row>
        <Navbar.Collapse className="justify-content-end">
          <Row>
            <Col xs="auto">
              <Nav.Link as={Link} to="/user/userinfo" className="icon-items">
                  <User size={20} /> <span>{userName}</span>
              </Nav.Link>
            </Col>
            <Col xs="auto">
              <Nav.Link onClick={handleLogOut} className="icon-items">
                  <LogOut size={20} /> <span>Logout</span>
              </Nav.Link>
            </Col>
          </Row>
        </Navbar.Collapse>
      </>
    ),
    Admin: () => (
      <>
        <Row>
          <Col xs="auto">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
          </Col>
          <Col xs="auto">
            <Nav.Link as={Link} to="/Admin/Category-Management">
              Category
            </Nav.Link>
          </Col>
          <Col xs="auto">
            <Nav.Link as={Link} to="/Admin/Film-Management">
              Film
            </Nav.Link>
          </Col>
        </Row>
        <Navbar.Collapse className="justify-content-end">
          <Row>
            <Col xs="auto">
              <Nav.Link as={Link} to="/user/userinfo" className="icon-items">
                  <User size={20} /> <span>{userName}</span>
              </Nav.Link>
            </Col>
            <Col xs="auto">
              <Nav.Link onClick={handleLogOut} className="icon-items">
                  <LogOut size={20} /> <span> Logout</span>
              </Nav.Link>
            </Col>
          </Row>
        </Navbar.Collapse>
      </>
    ),
  };
  const renderAuthLinks = () => {
    const roleHandler = roleMap[roles] || (() => renderUnAuthLinks());
    return roleHandler();
  };
  const renderUnAuthLinks = () => {
    return (
      <Navbar.Collapse className="justify-content-end">
        <Row>
          <Col xs="auto">
            <Nav.Link as={Link} to="/login" className="login-button">
              Login
            </Nav.Link>
          </Col>
          <Col xs="auto">
            <Nav.Link as={Link} to="/register" className="register-button">
              Register
            </Nav.Link>
          </Col>
        </Row>
      </Navbar.Collapse>
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
            {isLoggedIn ? renderAuthLinks() : renderUnAuthLinks()}
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </header>
  );
}
export default NavBar;
