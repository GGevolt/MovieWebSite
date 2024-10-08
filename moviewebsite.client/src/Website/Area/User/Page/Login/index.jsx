import { useState, useContext } from "react";
import { Form, Button, FloatingLabel } from "react-bootstrap";
import styles from "./Login.module.css";
import { Link, useNavigate, Navigate } from "react-router-dom";
import AuthContext from "../../../AuthContext/Context";

function Login() {
  document.title = "Login";
  const navigate = useNavigate();
  const authContext = useContext(AuthContext);
  const { signIn, isLoggedIn, roles } = authContext;
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const handleLogin = async (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append("email", email);
    formData.append("password", password);
    formData.append("rememberMe", rememberMe);
    if (await signIn(formData)) {
      const path = roleMap[roles[0]];
      if (path) {
        navigate(path);
      }
    }
  };
  const roleMap = {
    UserT0: "/user",
    UserT1: "/user",
    UserT2: "/user",
    Admin: "/admin",
  };
  if (isLoggedIn && roles.length !== 0) {
    const path = roleMap[roles[0]];
    if (path) {
      return <Navigate to={path} />;
    }
  }
  return (
    <div className={styles.Container}>
      <div className={styles.Banner} />
      <div className={styles.Log_in_form}>
        <h1>Sign In</h1>
        <Form
          onSubmit={handleLogin}
          encType="multipart/form-data"
          className={styles.Form}
        >
          <br />
          <FloatingLabel label="Email" className={styles.label}>
            <Form.Control
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </FloatingLabel>
          <br />
          <FloatingLabel label="Password" className={styles.label}>
            <Form.Control
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </FloatingLabel>
          <br />
          <Form.Check
            type="checkbox"
            value={rememberMe}
            onChange={(e) => setRememberMe(e.target.checked)}
            label="Remember Password?"
          />
          <br />
          <Button type="submit" className={styles.btn}>
            Sign In
          </Button>
        </Form>
        <hr className={styles.hr} />
        <p>
          Don&apos;t have account? <Link to="/Register">Sign Up</Link>
        </p>
      </div>
    </div>
  );
}

export default Login;
