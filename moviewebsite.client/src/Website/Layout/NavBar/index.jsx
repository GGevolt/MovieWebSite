import { useContext, useEffect, useState } from 'react';
import { Container, Nav, Navbar, Row, Col, NavDropdown, Offcanvas } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { User, LogOut, Menu, ChevronDown } from "lucide-react";
import "./NavBar.css";
import logo from "../../../assets/Image/Logo.png";
import SearchBar from '../../Area/User/Components/Form/SearchBar';
import AuthContext from "../../Area/AuthContext/Context";
import WebContext from "../../WebContext/Context";

function NavBar() {
  const webContext = useContext(WebContext);
  const { categories, getCategories } = webContext;
  const authContext = useContext(AuthContext);
  const { isLoggedIn, signOut, roles, userName, redirectToCustomerPortal } = authContext;
  const navigate = useNavigate();
  const [showOffcanvas, setShowOffcanvas] = useState(false);

  useEffect(() => {
    if (!(Array.isArray(categories) && categories.length > 0)) {
      getCategories();
    }
  }, []);

  const handleLogOut = async () => {
    await signOut();
    setShowOffcanvas(false);
  };

  const handleCategorySelect = (categoryId) => {
    navigate(`/user/search?categories=${categoryId}`);
    setShowOffcanvas(false);
  };

  const renderCategories = (isMobile = false) => (
    <NavDropdown 
      title={
        <>
          Categories <ChevronDown size={16} className="dropdown-icon" />
        </>
      } 
      className={`category-dropdown ${isMobile ? 'mobile-dropdown' : ''}`}
    >
      <div className={`Category-Container ${isMobile ? 'mobile-category-container' : ''}`}>
        {categories.map((category) => (
          <NavDropdown.Item 
            key={category.id} 
            onClick={(e) => {
              e.preventDefault();
              handleCategorySelect(category.id);
            }}
          >
            {category.name}
          </NavDropdown.Item>
        ))}
      </div>
    </NavDropdown>
  );

  const renderUserMenu = (isMobile = false) => (
    <NavDropdown
      title={
        <>
          <User size={20} /> <span className="username-text">{userName}</span> <ChevronDown size={16} className="dropdown-icon" />
        </>
      }
      className={`icon-items ${isMobile ? 'mobile-dropdown' : ''}`}
    >
      <NavDropdown.Item as={Link} to="/user/userinfo" onClick={() => setShowOffcanvas(false)}>
        User Info
      </NavDropdown.Item>
      {roles.includes("UserT2") && 
        <NavDropdown.Item as={Link} to="/user/playlist" onClick={() => setShowOffcanvas(false)}>
          Play List
        </NavDropdown.Item>
      }
      {roles.length > 1 && (
        <NavDropdown.Item onClick={() => {
          redirectToCustomerPortal();
          setShowOffcanvas(false);
        }}>
          Payment Portal
        </NavDropdown.Item>
      )}
    </NavDropdown>
  );

  const roleMap = {
    User: () => (
      <>
        <Row>
          <Col xs="auto">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
          </Col>
          {roles.length === 1 && (
            <Col>
              <Nav.Link as={Link} to="/user/memberships">
                Memberships
              </Nav.Link>
            </Col>
          )}
          <Col>
            {renderCategories()}
          </Col>
        </Row>
        <Navbar.Collapse className="justify-content-end">
          <Row className='navbar-right'>
            <Col xs="auto" className="search-col">
              <SearchBar/>
            </Col>
            <Col xs="auto">
              {renderUserMenu()}
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
              {renderUserMenu()}
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
    if (roles.includes("UserT0") || roles.includes("UserT1") || roles.includes("UserT2")) {
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

  const renderMobileMenu = () => (
    <Nav className="flex-column mobile-nav">
      {isLoggedIn ? (
        roles.includes("Admin") ? (
          <>
            <Nav.Link as={Link} to="/" onClick={() => setShowOffcanvas(false)}>Home</Nav.Link>
            <Nav.Link as={Link} to="/Admin/Category-Management" onClick={() => setShowOffcanvas(false)}>Category</Nav.Link>
            <Nav.Link as={Link} to="/Admin/Film-Management" onClick={() => setShowOffcanvas(false)}>Film</Nav.Link>
            {renderUserMenu(true)}
            <Nav.Link onClick={handleLogOut}>Logout</Nav.Link>
          </>
        ) : (
          <>
            <Nav.Link as={Link} to="/" onClick={() => setShowOffcanvas(false)}>Home</Nav.Link>
            {roles.length === 1 && (
              <Nav.Link as={Link} to="/user/memberships" onClick={() => setShowOffcanvas(false)}>Memberships</Nav.Link>
            )}
            {renderCategories(true)}
            {renderUserMenu(true)}
            <Nav.Link onClick={handleLogOut}>Logout</Nav.Link>
          </>
        )
      ) : (
        <>
          <Nav.Link as={Link} to="/login" onClick={() => setShowOffcanvas(false)}>Login</Nav.Link>
          <Nav.Link as={Link} to="/register" onClick={() => setShowOffcanvas(false)}>Register</Nav.Link>
        </>
      )}
    </Nav>
  );

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
          <Navbar.Toggle aria-controls="responsive-navbar-nav" className="d-lg-none" onClick={() => setShowOffcanvas(true)}>
            <Menu size={24} />
          </Navbar.Toggle>
          <Navbar.Collapse id="responsive-navbar-nav" className="d-none d-lg-flex">
            {isLoggedIn ? renderAuthLinks() : renderUnAuthLinks()}
          </Navbar.Collapse>
        </Container>
      </Navbar>

      <Offcanvas show={showOffcanvas} onHide={() => setShowOffcanvas(false)} placement="end" className="navbar-offcanvas">
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>Menu</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          {renderMobileMenu()}
          {isLoggedIn && !roles.includes("Admin") && (
            <div className="mobile-search-wrapper">
              <SearchBar />
            </div>
          )}
        </Offcanvas.Body>
      </Offcanvas>
    </header>
  );
}

export default NavBar;