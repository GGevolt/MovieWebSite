import { Outlet } from "react-router-dom";
import NavBar from "../NavBar";
import Footer from "../Footer";
import React from "react";
import styles from "./AdminBody.module.css"
function AdminBody() {
  return (
    <div className={styles.body}>
      <NavBar />
        <main>
          <Outlet />
        </main>
      <Footer />
    </div>
  );
}

export default AdminBody;
