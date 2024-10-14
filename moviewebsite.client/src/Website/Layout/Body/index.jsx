import { Outlet } from "react-router-dom";
import NavBar from "../NavBar";
import "./index.css";

function Body() {
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
