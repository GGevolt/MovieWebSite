import { useState, useContext } from "react";
import { Form, Button, FloatingLabel } from "react-bootstrap";
import { Link, useNavigate, Navigate } from "react-router-dom";
import { motion } from "framer-motion";
import { Mail, Key } from "lucide-react";
import AuthContext from "../../../AuthContext/Context";
import styles from "./Login.module.css";
import deco from "../../../../../assets/Image/deco1.jpg"

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
    <div className={styles.container}>
      <motion.div
        initial={{ opacity: 0, x: -50 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ duration: 0.5 }}
        className={styles.decorationContainer}
      >
        <div className={styles.decorationContent}>
          <h2>Welcome Back!</h2>
          <p>Sign in to access your account and enjoy our services.</p>
          <motion.img
            src={deco}
            alt="Decorative illustration"
            className={styles.decorationImage}
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.8, delay: 0.4 }}
          />
        </div>
      </motion.div>
      <motion.div
        initial={{ opacity: 0, x: 50 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ duration: 0.5 }}
        className={styles.formContainer}
      >
        <h1 className={styles.title}>Sign In</h1>
        <Form onSubmit={handleLogin} className={styles.form}>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.2 }}
          >
            <FloatingLabel label="Email" className={styles.inputGroup}>
              <Form.Control
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
                required
              />
              <Mail className={styles.inputIcon} />
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.4 }}
          >
            <FloatingLabel label="Password" className={styles.inputGroup}>
              <Form.Control
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
                required
              />
              <Key className={styles.inputIcon} />
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.6 }}
          >
            <Form.Check
              type="checkbox"
              id="rememberMe"
              className={styles.rememberMe}
              checked={rememberMe}
              onChange={(e) => setRememberMe(e.target.checked)}
              label="Remember me"
            />
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.8 }}
          >
            <Button type="submit" className={styles.submitButton}>
              Sign In
            </Button>
          </motion.div>
        </Form>
        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.5, delay: 1 }}
          className={styles.signupText}
        >
          Don&apos;t have an account? <Link to="/Register">Sign Up</Link>
        </motion.p>
      </motion.div>
    </div>
  );
}

export default Login;