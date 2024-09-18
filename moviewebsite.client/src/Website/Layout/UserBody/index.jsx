import React from "react";
import { Outlet } from "react-router-dom";
import NavBar from "../NavBar";

function UserBody() {
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
