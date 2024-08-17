import { Form, Container, Button, FloatingLabel } from "react-bootstrap";
import React, { useEffect, useState, useContext } from "react";
import axios from "axios";
import Popup from "reactjs-popup";
import { XCircleFill } from "react-bootstrap-icons";
import "./Form.css";
import PropTypes from "prop-types";
import { object, string } from "yup";
import WebContext from "../../../WebContext/Context";

function CategoryCUForm({ category }) {
  const webContext = useContext(WebContext);
  const { getCategories, onFilmCateUpdate } = webContext;
  const [name, setName] = useState(category ? category.name : "");
  const [id, setId] = useState(category ? category.id : 0);
  const [openForm, setOpen] = useState(false);
  const [errors, setErrors] = useState([]);
  const validateSchema = object({
    name: string()
      .required("Category name field is empty, Pls input to proceed!")
      .max(50, "The name is too long!"),
  });
  const resetForm = () => {
    setName(category ? category.name : "");
    setId(category ? category.id : 0);
  };
  const closeForm = () => {
    setOpen(false);
    resetForm();
  };
  useEffect(() => {
    resetForm();
  }, [category]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    const formData = { name, id };
    try {
      await validateSchema.validate(formData, { abortEarly: false });
      setErrors({});
    } catch (error) {
      const newErrors = {};
      error.inner.forEach((err) => {
        newErrors[err.path] = err.message;
      });
      setErrors(newErrors);
      return;
    }
    if (id === 0) {
      axios
        .post("/api/category", formData)
        .then(() => {
          getCategories();
          if (id !== 0) {
            onFilmCateUpdate(true);
          }
          closeForm();
        })
        .catch((error) => {
          console.error(
            "There has been a problem with category create operation:",
            error
          );
        });
    } else {
      axios
        .post("/api/category", formData)
        .then(() => {
          getCategories();
          closeForm();
        })
        .catch((error) => {
          console.error(
            "There has been a problem with category update operation:",
            error
          );
        });
    }
  };

  return (
    <>
      <Button variant="outline-success" onClick={() => setOpen((o) => !o)}>
        {id === 0 ? "Create category" : "Update"}
      </Button>
      <Popup
        open={openForm}
        closeOnDocumentClick
        onClose={closeForm}
        className="form-popup"
      >
        <Container>
          <XCircleFill className="close" onClick={closeForm} />
          <h3>{id === 0 ? "Create" : "Update"} a category</h3>
          <Form onSubmit={handleSubmit} encType="multipart/form-data">
            <input
              type="hidden"
              value={id}
              onChange={(e) => setId(e.target.value)}
            />
            <Form.Group className="mb-3">
              <FloatingLabel
                controlId="floatingName"
                label="Name"
                className="custom-label"
              >
                <Form.Control
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  placeholder=""
                />
                {errors.name && (
                  <Form.Text className="error">{errors.name}</Form.Text>
                )}
              </FloatingLabel>
            </Form.Group>
            <Button type="submit" className="button">
              {id === 0 ? "Create" : "Update"}
            </Button>
          </Form>
        </Container>
      </Popup>
    </>
  );
}

CategoryCUForm.propTypes = {
  category: PropTypes.shape({
    id: PropTypes.number,
    name: PropTypes.string,
  }),
};
export default CategoryCUForm;
