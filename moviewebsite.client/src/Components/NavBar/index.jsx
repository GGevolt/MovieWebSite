import Button from 'react-bootstrap/Button';
import InputGroup from 'react-bootstrap/InputGroup';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';
import { Search } from 'react-bootstrap-icons';
import './NavBar.css'


function NavBar() {
    return (
        <header>
          <Navbar collapseOnSelect expand="lg" className="navbar">
            <Container>
              <Navbar.Brand as={Link} to="/Home">Sodoki</Navbar.Brand>
              <Navbar.Toggle aria-controls="responsive-navbar-nav" />
              <Navbar.Collapse id="responsive-navbar-nav">
                <Nav className="me-auto">
                  <Nav.Link href="#features">Features</Nav.Link>
                  <Nav.Link href="#pricing">Pricing</Nav.Link>
                  <NavDropdown menuVariant="dark" title="CRUD Management" id="collapsible-nav-dropdown">
                    <NavDropdown.Item as={Link} to="/Admin/Category-Management">Category</NavDropdown.Item>
                    <NavDropdown.Item as={Link} to="/Admin/Film-Management">Film</NavDropdown.Item>
                  </NavDropdown>
                </Nav>
                {/* <Nav>
                  <Form className="d-flex">
                    <InputGroup>
                      <Form.Control
                      type="search"
                      placeholder="Search"
                      id='search'
                      aria-label="Search"
                      />
                      <Button className='icon'><Search color="royalblue" size={23}/></Button>
                    </InputGroup>
                  </Form>
                </Nav> */}
              </Navbar.Collapse>
            </Container>
          </Navbar>
        </header>
      );
        
}
export default NavBar