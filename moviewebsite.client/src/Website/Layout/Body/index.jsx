import React, { useContext } from "react";
import { Outlet } from "react-router-dom";
import NavBar from "../NavBar";
import AuthContext from "../../Area/AuthContext/Context";
import adminStyle from "./AdminBody.module.css";
import userStyle from "./UserBody.module.css";
import "./index.css";

function Body() {
  const authContext = useContext(AuthContext);
  const { isLoggedIn, roles } = authContext;
  if (isLoggedIn && roles.length !== 0) {
    switch (roles[0]) {
      case "Admin":  
        return (
          <div className={adminStyle.body}>
            <NavBar />
            <main>
              <Outlet />
            </main>
          </div>
        );
      case "UserT0" || "UserT1":
        return (
          <div className={userStyle.body}>
            <NavBar />
            <main>
              <Outlet />
            </main>
          </div>
        );
      default:
        break;
    }
  }
  return (
    <div>
      <NavBar />
      <main>
        <Outlet />
      </main>
    </div>
  );
}
export default Body;
