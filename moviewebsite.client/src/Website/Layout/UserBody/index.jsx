import React, { useContext } from "react";
import { Outlet } from "react-router-dom";
import NavBar from "../NavBar";
import AuthContext from "../../User/AuthContext/Context";

function UserBody() {
  const authContext = useContext(AuthContext);
  const { roles } = authContext;
  return (
    <div>
      <NavBar />
      <main>
        <Outlet />
      </main>
    </div>
  );
}
export default UserBody;
