import { useState, useContext } from "react";
import { Form, Button, FloatingLabel, Spinner } from "react-bootstrap";
import { Link, Navigate } from "react-router-dom";
import { motion } from "framer-motion";
import { User, Mail, Key, Calendar, UserCheck } from "lucide-react";
import { object, string, date } from "yup";
import AuthContext from "../../../AuthContext/Context";
import PopUp from "../../Components/Popup";
import styles from "./Register.module.css";
import deco from "../../../../../assets/Image/deco2.jpeg"

function Register() {
  document.title = "Register";
  const authContext = useContext(AuthContext);
  const [uploading, setUploading] = useState(false);
  const { register, isLoggedIn, roles } = authContext;
  const [isPopupOpen, setIsPopupOpen] = useState(false);
  const [userInfo, setUserInfo] = useState({
    fullName: "",
    gender: "",
    dob: null,
    email: "",
    username: "",
    password: "",
  });
  const [agreeToTerm, setAgreeToTerm] = useState(false);
  const today = new Date().toISOString().split("T")[0];
  const oneHundredYearsAgo = new Date();
  oneHundredYearsAgo.setFullYear(oneHundredYearsAgo.getFullYear() - 100);
  const minDate = oneHundredYearsAgo.toISOString().split("T")[0];
  const [errors, setErrors] = useState({});

  const validateSchema = object({
    fullName: string()
      .required("Full Name is missing!")
      .matches(
        /^([A-Za-z]+(([',. -][A-Za-z ])?[A-Za-z]*)*)*$/,
        "Invalid full name format."
      )
      .max(100, "Name too long!"),
    gender: string().required("Gender is missing!"),
    username: string()
      .required("Username is missing!")
      .matches(
        /^[A-Za-z0-9 ]*$/,
        "Invalid, username, don't allow special character!"
      )
      .max(20, "Username is too long!"),
    email: string()
      .required("Email is missing!")
      .matches(
        /^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})?$/,
        "Invalid email format!"
      ),
    password: string()
      .required("Password is missing!")
      .matches(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/,
        "At least 8 characters, 1 uppercase, 1 lowercase, 1 digit, 1 special character"
      )
      .max(30, "Password is too long!"),
    dob: date()
      .required("The Date of Birth is missing!")
      .min(minDate, `Must be from ${minDate} onward!`)
      .max(today, `Must be today or earlier `),
  });

  const handleChange = (event) => {
    setUserInfo((prev) => ({
      ...prev,
      [event.target.name]: event.target.value,
    }));
  };

  const handleRegister = async (event) => {
    event.preventDefault();
    try {
      await validateSchema.validate(userInfo, { abortEarly: false });
      setErrors({});
    } catch (error) {
      const newErrors = {};
      error.inner.forEach((err) => {
        newErrors[err.path] = err.message;
      });
      setErrors(newErrors);
      return;
    }
    setUploading(true);
    const formData = new FormData();
    formData.append("fullName", userInfo.fullName.trim());
    formData.append("gender", userInfo.gender);
    formData.append("email", userInfo.email.trim());
    formData.append("dob", userInfo.dob);
    formData.append("passwordHash", userInfo.password);
    formData.append("username", userInfo.username.trim());
    formData.append("emailConfirnUrl", `${window.location.origin}/emailconfirm`);
    if (await register(formData)) {
      setIsPopupOpen(true);
    }
    setUploading(false);
  };

  const popUpContent = () => {
    return (
      <div>
        <h3 className={styles.popHeader}>
          Check Your Email <Mail />
        </h3>
        <p className={styles.popContent}>
          We&apos;ve sent a confirmation link to your email address. Please
          check your inbox and click the link to verify your account.
        </p>
      </div>
    );
  };

  const handleClosePop = () => {
    setIsPopupOpen(false);
    document.location = "/login";
  };

  if (isLoggedIn && roles.length !== 0) {
    return <Navigate to="/login" />;
  }

  return (
    <div className={styles.container}>
      <PopUp isOpen={isPopupOpen} handleClose={handleClosePop}>
        {popUpContent()}
      </PopUp>
      <motion.div
        initial={{ opacity: 0, x: -50 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ duration: 0.5 }}
        className={styles.decorationContainer}
      >
        <div className={styles.decorationContent}>
          <h2>Join Our Community</h2>
          <p>Create an account to access exclusive features and content.</p>
          <motion.img
            src={deco}
            alt="Decorative illustration"
            className={styles.decorationImage}
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration:  0.8, delay: 0.4 }}
          />
        </div>
      </motion.div>
      <motion.div
        initial={{ opacity: 0, x: 50 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ duration: 0.5 }}
        className={styles.formContainer}
      >
        <h1 className={styles.title}>Create an Account</h1>
        <Form onSubmit={handleRegister} className={styles.form}>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.2 }}
          >
            <FloatingLabel label="Full Name" className={styles.inputGroup}>
              <Form.Control
                type="text"
                placeholder="Full Name"
                name="fullName"
                value={userInfo.fullName}
                onChange={handleChange}
                isInvalid={errors.fullName}
              />
              <User className={styles.inputIcon} />
              <Form.Control.Feedback type="invalid">
                {errors.fullName}
              </Form.Control.Feedback>
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.3 }}
          >
            <FloatingLabel label="Username" className={styles.inputGroup}>
              <Form.Control
                type="text"
                placeholder="Username"
                name="username"
                value={userInfo.username}
                onChange={handleChange}
                isInvalid={errors.username}
              />
              <UserCheck className={styles.inputIcon} />
              <Form.Control.Feedback type="invalid">
                {errors.username}
              </Form.Control.Feedback>
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.4 }}
          >
            <FloatingLabel label="Email" className={styles.inputGroup}>
              <Form.Control
                type="email"
                placeholder="Email"
                name="email"
                value={userInfo.email}
                onChange={handleChange}
                isInvalid={errors.email}
              />
              <Mail className={styles.inputIcon} />
              <Form.Control.Feedback type="invalid">
                {errors.email}
              </Form.Control.Feedback>
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.5 }}
          >
            <FloatingLabel label="Password" className={styles.inputGroup}>
              <Form.Control
                type="password"
                placeholder="Password"
                name="password"
                value={userInfo.password}
                onChange={handleChange}
                isInvalid={errors.password}
              />
              <Key className={styles.inputIcon} />
              <Form.Control.Feedback type="invalid">
                {errors.password}
              </Form.Control.Feedback>
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.6 }}
            className={styles.inlineInputs}
          >
            <FloatingLabel label="Gender" className={styles.inputGroup}>
              <Form.Control
                as="select"
                name="gender"
                value={userInfo.gender}
                onChange={handleChange}
                isInvalid={errors.gender}
              >
                <option value="">Select</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </Form.Control>
              <Form.Control.Feedback type="invalid">
                {errors.gender}
              </Form.Control.Feedback>
            </FloatingLabel>
            <FloatingLabel label="Date of Birth" className={styles.inputGroup}>
              <Form.Control
                type="date"
                name="dob"
                value={userInfo.dob || ""}
                onChange={handleChange}
                max={today}
                min={minDate}
                isInvalid={errors.dob}
              />
              <Calendar className={styles.inputIcon} />
              <Form.Control.Feedback type="invalid">
                {errors.dob}
              </Form.Control.Feedback>
            </FloatingLabel>
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.7 }}
          >
            <Form.Check
              type="checkbox"
              id="agreeToTerm"
              className={styles.checkBox}
              checked={agreeToTerm}
              onChange={(e) => setAgreeToTerm(e.target.checked)}
              label="I accept the terms and conditions for signing up to this service, and hereby confirm I have read the privacy policy."
            />
          </motion.div>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5, delay: 0.8 }}
          >
            <Button
              type="submit"
              className={styles.submitButton}
              disabled={!agreeToTerm || uploading}
            >
              {uploading ? <Spinner animation="border" size="sm" /> : "Register"}
            </Button>
          </motion.div>
        </Form>
        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.5, delay: 1 }}
          className={styles.loginText}
        >
          Already have an account? <Link to="/login">Sign In</Link>
        </motion.p>
      </motion.div>
    </div>
  );
}

export default Register;