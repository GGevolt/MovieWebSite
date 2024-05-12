import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import {ListColumnsReverse} from 'react-bootstrap-icons'
import { Link } from 'react-router-dom';
import { Sidebar, Menu, MenuItem, SubMenu } from 'react-pro-sidebar';
import './SideBar.css'
import React,{useState} from 'react';


function SideBar() {
  const [toggled, setToggled] = React.useState(false);
    return (
      <>
      <div className='sidebar'>
        <Sidebar onBackdropClick={() => setToggled(false)} toggled={toggled} breakPoint="always">
          <Menu>
            <SubMenu label="CRUD Management">
              <MenuItem component={<Link to="/Admin/Category-Management" />}> Category </MenuItem>
              <MenuItem component={<Link to="/Admin/Film-Management" />}> Film </MenuItem>
            </SubMenu>
            <MenuItem> Documentation </MenuItem>
            <MenuItem> Calendar </MenuItem>
          </Menu>
        </Sidebar>
      </div>
      <div className='admin-body'><ListColumnsReverse onClick={() => setToggled(!toggled)} /></div>
      </>
      );
        
}
export default SideBar;