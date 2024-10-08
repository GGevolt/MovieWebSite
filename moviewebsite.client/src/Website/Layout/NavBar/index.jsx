import { Container, Nav, Navbar, Row, Col, NavDropdown } from "react-bootstrap";
import { Link } from "react-router-dom";
import { useContext } from "react";
import { User, LogOut } from "lucide-react";
import "./NavBar.css";
import logo from "../../../assets/Image/Logo.png";
import AuthContext from "../../Area/AuthContext/Context";

function NavBar() {
  const authContext = useContext(AuthContext);
  const { isLoggedIn, signOut, roles, userName, redirectToCustomerPortal } =
    authContext;
  const handleLogOut = async () => {
    await signOut();
  };
  const roleMap = {
    User: () => (
      <>
        <Row>
          <Col xs="auto">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
          </Col>
          {roles.length == 1 && (
            <Col>
              <Nav.Link as={Link} to="/user/memberships">
                Memberships
              </Nav.Link>
            </Col>
          )}
        </Row>
        <Navbar.Collapse className="justify-content-end">
          <Row>
            <Col xs="auto">
              <NavDropdown
                title={
                  <>
                    <User size={20} /> <span>{userName}</span>
                  </>
                }
                className="icon-items"
              >
                <NavDropdown.Item as={Link} to="/user/userinfo">
                  User Info
                </NavDropdown.Item>
                {roles.length > 1 && (
                  <NavDropdown.Item onClick={() => redirectToCustomerPortal()}>
                    Payment Portal
                  </NavDropdown.Item>
                )}
              </NavDropdown>
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
    let roleHandler;
    if (roles.includes("UserT0" || "UserT1" || "UserT2")) {
      roleHandler = roleMap["User"];
    } else if (roles.includes("Admin")) {
      roleHandler = roleMap["Admin"];
    } else {
      roleHandler = roleMap[roles] || (() => renderUnAuthLinks());
    }
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
            <img
              alt=""
              src={logo}
              width="30"
              height="30"
              className="d-inline-block align-top"
            />{" "}
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
