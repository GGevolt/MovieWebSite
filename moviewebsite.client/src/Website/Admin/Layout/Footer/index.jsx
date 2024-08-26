import React from "react";
import styles from "./Footer.module.css"

function Footer() {
  return (
    <footer className={styles.Footer}>
      <hr className={styles.Horizontal} />
      <p className={styles.p}>&copy; 2024 Sodoki</p>
    </footer>
  );
}

export default Footer;
