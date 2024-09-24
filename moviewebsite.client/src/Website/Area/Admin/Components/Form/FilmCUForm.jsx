import axios from "axios";
import PropTypes from "prop-types";
import React, { useEffect, useState, useContext } from "react";
import {
  Button,
  Col,
  Container,
  FloatingLabel,
  Form,
  Row,
} from "react-bootstrap";
import { XCircleFill } from "react-bootstrap-icons";
import Select from "react-select";
import makeAnimated from "react-select/animated";
import Popup from "reactjs-popup";
import { array, object, string } from "yup";
import WebApi from "../../../../WebApi";
import WebContext from "../../../../WebContext/Context";
import "./Form.css";

function FilmCUForm(props) {
  const webContext = useContext(WebContext);
  const { categories, getCategories, onFilmCateUpdate, getFilms } = webContext;
  const [filmCates, setFilmCates] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState([]);
  const [categoryOption, setcategoryOption] = useState([]);
  const [film, setFilm] = useState({
    id: props.film ? props.film.id : 0,
    title: props.film ? props.film.title : "",
    synopsis: props.film ? props.film.synopsis : "",
    director: props.film ? props.film.director : "",
    type: props.film ? props.film.type : "",
    filmPath: props.film ? props.film.filmPath : "",
  });
  const [openForm, setOpen] = useState(false);
  const animatedComponents = makeAnimated();
  const [errors, setErrors] = useState([]);
  const validateSchema = object({
    film: object({
      title: string()
        .required("Title field is empty, Pls input to proceed!")
        .max(150, "The title is too long!"),
      synopsis: string().required(
        "Synopsis field is empty, Pls input to proceed!"
      ),
      type: string().required(
        "Type field is empty, Pls select a type to proceed!"
      ),
      director: string()
        .required("Director field is empty, Pls input to proceed!")
        .max(100, "The name is too long!"),
    }),
    selectedCategories: array()
      .min(1, "Select atleast 1 category!")
      .required("Select atleast 1 category!"),
  });

  useEffect(() => {
    getCategories();
    if (film.id !== 0) {
      loadFilmCates();
    }
  }, [film]);

  const loadFilmCates = async () => {
    const filmcatesData = await WebApi.getFilmCates(film.id);
    setFilmCates(filmcatesData);
  };

  useEffect(() => {
    setcategoryOption(
      categories.map((category) => ({
        value: category.id,
        label: category.name,
      }))
    );
    if (film.id !== 0) {
      setSelectedCategories(
        categoryOption.filter((option) =>
          filmCates.some((filmCategory) => filmCategory.id === option.value)
        )
      );
    }
  }, [filmCates, categories]);

  const handleChange = (event) => {
    setFilm((prev) => ({ ...prev, [event.target.name]: event.target.value }));
  };

  const resetForm = () => {
    setFilm({
      id: props.film ? props.film.id : 0,
      title: props.film ? props.film.title : "",
      synopsis: props.film ? props.film.synopsis : "",
      director: props.film ? props.film.director : "",
      type: props.film ? props.film.type : "",
      filmPath: props.film ? props.film.filmPath : "",
    });
    setSelectedCategories(null);
    setErrors({});
  };

  const closeForm = () => {
    setOpen(false);
    resetForm();
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    const filmVM = {
      film: film,
      selectedCategories:
        selectedCategories &&
        selectedCategories.map((category) => category.value),
    };
    try {
      await validateSchema.validate(filmVM, { abortEarly: false });
      setErrors({});
    } catch (error) {
      const newErrors = {};
      error.inner.forEach((err) => {
        newErrors[err.path] = err.message;
      });
      setErrors(newErrors);
      return;
    }
    axios
      .post("/api/film", filmVM, {
        headers: {
          "Content-Type": "application/json",
        },
      })
      .then(async () => {
        if (film.id !== 0) {
          let pastFilmCateId = filmCates.map((cate) => cate.id);
          if (
            filmCates.length !== filmVM.selectedCategories.length ||
            JSON.stringify([...pastFilmCateId].sort()) !==
              JSON.stringify([...filmVM.selectedCategories].sort())
          ) {
            onFilmCateUpdate(true);
          }
        }
        await getFilms();
        closeForm();
      })
      .catch((error) => {
        console.error(
          "There has been a problem with the film submission:",
          error
        );
      });
  };

  return (
    <>
      <Button variant="outline-success" onClick={() => setOpen((o) => !o)}>
        {film.id === 0 ? "Create New Film" : "Update Film"}
      </Button>
      <Popup
        open={openForm}
        closeOnDocumentClick
        onClose={closeForm}
        className="form-popup"
      >
        <XCircleFill className="close" onClick={closeForm} />
        <Container className="film-form">
          <h3>{film.id === 0 ? "Create" : "Update"} a Movie</h3>
          <Form onSubmit={handleSubmit} encType="multipart/form-data">
            <Form.Control
              type="hidden"
              value={film.id}
              onChange={handleChange}
              name="id"
            />
            <Form.Control
              type="hidden"
              value={film.filmPath}
              onChange={handleChange}
              name="filmPath"
            />
            <Form.Group className="mb-3">
              <FloatingLabel
                controlId="floating-Title"
                label="Title"
                className="custom-label"
              >
                <Form.Control
                  type="text"
                  value={film.title}
                  onChange={handleChange}
                  placeholder=""
                  name="title"
                  isInvalid={errors["film.title"]}
                />
                <Form.Control.Feedback className="error" type="invalid">
                  {errors["film.title"]}
                </Form.Control.Feedback>
              </FloatingLabel>
            </Form.Group>
            <Row className="mb-3">
              <Form.Group as={Col}>
                <FloatingLabel
                  controlId="floating-Director"
                  label="Director"
                  className="custom-label"
                >
                  <Form.Control
                    type="text"
                    value={film.director}
                    onChange={handleChange}
                    placeholder=""
                    name="director"
                    isInvalid={errors["film.director"]}
                  />
                  <Form.Control.Feedback className="error" type="invalid">
                    {errors["film.director"]}
                  </Form.Control.Feedback>
                </FloatingLabel>
              </Form.Group>
              <Form.Group as={Col}>
                <FloatingLabel
                  controlId="floating-Title"
                  label="Type"
                  className="custom-label"
                >
                  <Form.Select
                    onChange={handleChange}
                    name="type"
                    value={film.type}
                    isInvalid={errors["film.type"]}
                  >
                    <option value="">Pls choose a type</option>
                    <option>TV-Series</option>
                    <option>Movie</option>
                  </Form.Select>
                  <Form.Control.Feedback className="error" type="invalid">
                    {errors["film.type"]}
                  </Form.Control.Feedback>
                </FloatingLabel>
              </Form.Group>
            </Row>
            <Form.Group className="mb-3">
              <FloatingLabel
                controlId="floating-Synopsis"
                label="Synopsis"
                className="custom-label"
              >
                <Form.Control
                  as="textarea"
                  value={film.synopsis}
                  onChange={handleChange}
                  placeholder=""
                  name="synopsis"
                  isInvalid={errors["film.synopsis"]}
                />
                <Form.Control.Feedback className="error" type="invalid">
                  {errors["film.synopsis"]}
                </Form.Control.Feedback>
              </FloatingLabel>
            </Form.Group>
            <Form.Group className="mb-3">
              <Select
                options={categoryOption}
                components={animatedComponents}
                placeholder="Select Categories for the film"
                isMulti
                onChange={(event) => setSelectedCategories(event)}
                value={selectedCategories}
              />
              {errors.selectedCategories && (
                <Form.Text className="error">
                  {errors.selectedCategories}
                </Form.Text>
              )}
            </Form.Group>
            <Button type="submit" className="button">
              {film.id === 0 ? "Create" : "Update"}
            </Button>
          </Form>
        </Container>
      </Popup>
    </>
  );
}

FilmCUForm.propTypes = {
  film: PropTypes.shape({
    id: PropTypes.number,
    title: PropTypes.string,
    synopsis: PropTypes.string,
    director: PropTypes.string,
    type: PropTypes.string,
    filmPath: PropTypes.string,
  }),
};

export default FilmCUForm;
