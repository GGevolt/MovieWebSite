import { useState, useContext } from "react";
import { Form, Col, Row, Button, Spinner } from "react-bootstrap";
import { object, string, date } from "yup";
import styles from "./Register.module.css";
import AuthContext from "../../../AuthContext/Context";
import PopUp from "../../Components/Popup";
import { EnvelopeAt } from "react-bootstrap-icons";
import { Navigate } from "react-router-dom";

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
  const [errors, setErrors] = useState([]);
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
    if (await register(formData)) {
      setIsPopupOpen(true);
    }
    setUploading(false);
  };
  const popUpContent = () => {
    return (
      <div>
        <h3 className={styles.pop_header}>
          Check Your Email <EnvelopeAt />
        </h3>
        <p className={styles.pop_content}>
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
    <div className={styles.page}>
      {
        <PopUp isOpen={isPopupOpen} handleClose={handleClosePop}>
          {popUpContent()}
        </PopUp>
      }
      <div className={styles.Container}>
        <div className={styles.content}>
          <h1 className={styles.h1}>Create an Account</h1>
          <br />
          <Form
            onSubmit={handleRegister}
            encType="multipart/form-data"
            className={styles.Form}
          >
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2} className={styles.label}>
                Full Name
              </Form.Label>
              <Col sm={10}>
                <Form.Control
                  type="text"
                  placeholder="Full Name"
                  value={userInfo.fullName}
                  name="fullName"
                  onChange={handleChange}
                  isInvalid={errors.fullName}
                  className={styles.input}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.fullName}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2} className={styles.label}>
                Gender
              </Form.Label>
              <Col sm={4}>
                <Form.Control
                  as="select"
                  value={userInfo.gender}
                  name="gender"
                  onChange={handleChange}
                  isInvalid={errors.gender}
                  className={styles.input}
                >
                  <option value="">Select</option>
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                </Form.Control>
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.gender}
                </Form.Control.Feedback>
              </Col>
              <Form.Label
                column
                className={styles.label}
                id={styles.date_label}
              >
                Date of Birth
              </Form.Label>
              <Col sm={4}>
                <Form.Control
                  type="date"
                  value={userInfo.dob || ""}
                  name="dob"
                  onChange={handleChange}
                  max={today}
                  min={minDate}
                  isInvalid={errors.dob}
                  className={styles.input}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.dob}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2} className={styles.label}>
                Username
              </Form.Label>
              <Col sm={10}>
                <Form.Control
                  type="text"
                  placeholder="Username"
                  value={userInfo.username}
                  name="username"
                  onChange={handleChange}
                  isInvalid={errors.username}
                  className={styles.input}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.username}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2} className={styles.label}>
                Email
              </Form.Label>
              <Col sm={10}>
                <Form.Control
                  type="string"
                  placeholder="Email"
                  value={userInfo.email}
                  name="email"
                  onChange={handleChange}
                  isInvalid={errors.email}
                  className={styles.input}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.email}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2} className={styles.label}>
                Password
              </Form.Label>
              <Col sm={10} className={styles.label}>
                <Form.Control
                  type="password"
                  placeholder="Password"
                  value={userInfo.password}
                  name="password"
                  onChange={handleChange}
                  isInvalid={errors.password}
                  className={styles.input}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.password}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label className={styles.label}>
                Terms and Conditions
              </Form.Label>
              <Form.Check
                type="checkbox"
                value={agreeToTerm}
                onChange={(e) => {
                  setAgreeToTerm(e.target.checked);
                }}
                className={styles.check}
                label="I accept the terms and conditions for signing up to this service, and hereby confirm I have read the privacy policy."
              />
            </Form.Group>
            <Button
              type="submit"
              className={styles.btn}
              disabled={!agreeToTerm || uploading}
            >
              {uploading ? <Spinner animation="border" /> : "Register"}
            </Button>
          </Form>
        </div>
      </div>
    </div>
  );
}
export default Register;
