import React, { useState } from "react";
import { Form, Button, Col, Row } from "react-bootstrap";
import { object, string, date } from "yup";
import styles from "./Register.module.css";

function Register() {
  const [userInfo, setUserInfo] = useState({
    firstName: "",
    lastName: "",
    dob: null,
    email: "",
    userName: "",
    password: "",
  });
  const today = new Date().toISOString().split("T")[0];
  const oneHundredYearsAgo = new Date();
  oneHundredYearsAgo.setFullYear(oneHundredYearsAgo.getFullYear() - 100);
  const minDate = oneHundredYearsAgo.toISOString().split("T")[0];
  const [errors, setErrors] = useState([]);
  const validateSchema = object({
    firstName: string()
      .required("First Name is missing!")
      .matches(
        /^([A-Za-z]+(([',. -][A-Za-z ])?[A-Za-z]*)*)*$/,
        "Invalid first name format."
      ),
    lastName: string()
      .required("Last Name is missing!")
      .matches(
        /^([A-Za-z]+(([',. -][A-Za-z ])?[A-Za-z]*)*)*$/,
        "Invalid last name format."
      ),
    userName: string()
      .required("User name is missing!")
      .matches(
        /^[A-Za-z0-9 ]*$/,
        "Invalid, user name, don't allow special character!"
      )
      .max(16, "User name is too long!"),
    email: string()
      .required("Email is missing!")
      .matches(
        /^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})?$/,
        "Invalid email format!"
      ),
    password: string()
      .required("Password is missing!")
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
  };
  return (
    <div className={styles.body}>
      <div className={styles.Container}>
        <div className={styles.content}>
          <h1>Create an Account</h1>
          <Form
            onSubmit={handleRegister}
            encType="multipart/form-data"
            className={styles.Form}
          >
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2}>
                First Name:
              </Form.Label>
              <Col sm={4}>
                <Form.Control
                  type="text"
                  placeholder="First Name"
                  value={userInfo.firstName}
                  name="firstName"
                  onChange={handleChange}
                  isInvalid={errors.firstName}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.firstName}
                </Form.Control.Feedback>
              </Col>
              <Form.Label column sm={2}>
                Last Name:
              </Form.Label>
              <Col sm={4}>
                <Form.Control
                  type="text"
                  placeholder="Last Name"
                  value={userInfo.lastName}
                  name="lastName"
                  onChange={handleChange}
                  isInvalid={errors.lastName}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.lastName}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2}>
                Email:
              </Form.Label>
              <Col sm={8}>
                <Form.Control
                  type="string"
                  placeholder="Email"
                  value={userInfo.email}
                  name="email"
                  onChange={handleChange}
                  isInvalid={errors.email}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.email}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2}>
                Password:
              </Form.Label>
              <Col sm={5}>
                <Form.Control
                  type="password"
                  placeholder="Password"
                  value={userInfo.password}
                  name="password"
                  onChange={handleChange}
                  isInvalid={errors.password}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.password}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm={2}>
                User Name:
              </Form.Label>
              <Col sm={4}>
                <Form.Control
                  type="text"
                  placeholder="User Name"
                  value={userInfo.userName}
                  name="userName"
                  onChange={handleChange}
                  isInvalid={errors.userName}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.userName}
                </Form.Control.Feedback>
              </Col>
              <Form.Label column sm={3}>
                Date of Birth:
              </Form.Label>
              <Col sm={3}>
                <Form.Control
                  type="date"
                  value={userInfo.dob || ""}
                  name="dob"
                  onChange={handleChange}
                  max={today}
                  min={minDate}
                  isInvalid={errors.dob}
                />
                <Form.Control.Feedback type="invalid" className={styles.error}>
                  {errors.dob}
                </Form.Control.Feedback>
              </Col>
            </Form.Group>
            <Button type="submit">Register</Button>
          </Form>
        </div>
      </div>
    </div>
  );
}
export default Register;
