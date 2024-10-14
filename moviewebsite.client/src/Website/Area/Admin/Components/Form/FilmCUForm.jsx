import axios from "axios";
import PropTypes from "prop-types";
import { useEffect, useState, useContext, useRef } from "react";
import {
  Button,
  Col,
  Container,
  FloatingLabel,
  Form,
  Row,
  Card,
  Image,
} from "react-bootstrap";
import {
  XCircleFill,
  Film,
  Upload,
  Trash,
  PersonFill,
  FileEarmarkText,
  TagsFill,
  FileFontFill,
} from "react-bootstrap-icons";
import Select from "react-select";
import makeAnimated from "react-select/animated";
import Popup from "reactjs-popup";
import { array, object, string, mixed } from "yup";
import WebApi from "../../../../WebApi";
import WebContext from "../../../../WebContext/Context";
import AdminContext from "../../AminContext/Context";
import styles from "./FilmCUForm.module.css";

function FilmCUForm(props) {
  const webContext = useContext(WebContext);
  const { categories, getCategories, getFilms } = webContext;
  const adminContext = useContext(AdminContext);
  const { onFilmCateUpdate } = adminContext;
  const [filmCates, setFilmCates] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState([]);
  const [categoryOption, setcategoryOption] = useState([]);
  const [film, setFilm] = useState({
    id: props.film ? props.film.id : 0,
    title: props.film ? props.film.title : "",
    synopsis: props.film ? props.film.synopsis : "",
    director: props.film ? props.film.director : "",
    type: props.film ? props.film.type : "",
    blurHash: props.film ? props.film.blurHash : "",
    filmPath: props.film ? props.film.filmPath : "",
  });
  const [openForm, setOpen] = useState(false);
  const animatedComponents = makeAnimated();
  const [errors, setErrors] = useState([]);
  const [image, setImage] = useState([]);
  const [imageFile, setImageFile] = useState(null);
  const [isDragging, setIsDragging] = useState(false);
  const fileInputRef = useRef(null);

  const selectFile = () => {
    fileInputRef.current.click();
  };

  const handleFileSelect = (event) => {
    const file = event.target.files[0];
    if (!file || file.type.split("/")[0] !== "image") {
      return;
    }
    setImage(() => [
      {
        name: file.name,
        url: URL.createObjectURL(file),
      },
    ]);
    setImageFile(file);
  };

  const handleDragOver = (event) => {
    event.preventDefault();
    setIsDragging(true);
    event.dataTransfer.dropEffect = "copy";
  };

  const handleDragLeave = (event) => {
    event.preventDefault();
    if (!event.currentTarget.contains(event.relatedTarget)) {
      setIsDragging(false);
    }
  };

  const handleDrop = (event) => {
    event.preventDefault();
    setIsDragging(false);
    const file = event.dataTransfer.files[0];
    if (file.type.split("/")[0] !== "image") {
      return;
    }
    setImage(() => [
      {
        name: file.name,
        url: URL.createObjectURL(file),
      },
    ]);
    setImageFile(file);
  };

  const handleDeleteImg = () => {
    setImage([]);
    setImageFile(null);
  };

  const validateSchema = !props.film
    ? object({
        film: object({
          title: string()
            .required("Title field is empty, Please input to proceed!")
            .max(150, "The title is too long!"),
          synopsis: string().required(
            "Synopsis field is empty, Please input to proceed!"
          ),
          type: string().required(
            "Type field is empty, Please select a type to proceed!"
          ),
          director: string()
            .required("Director field is empty, Please input to proceed!")
            .max(100, "The name is too long!"),
        }),
        selectedCategories: array()
          .min(1, "Select at least 1 category!")
          .required("Select at least 1 category!"),
        imageFile: mixed().required("Please choose an image to upload."),
      })
    : object({
        film: object({
          title: string()
            .required("Title field is empty, Please input to proceed!")
            .max(150, "The title is too long!"),
          synopsis: string().required(
            "Synopsis field is empty, Please input to proceed!"
          ),
          type: string().required(
            "Type field is empty, Please select a type to proceed!"
          ),
          director: string()
            .required("Director field is empty, Please input to proceed!")
            .max(100, "The name is too long!"),
        }),
        selectedCategories: array()
          .min(1, "Select at least 1 category!")
          .required("Select at least 1 category!"),
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
      blurHash: props.film ? props.film.blurHash : "",
      filmPath: props.film ? props.film.filmPath : "",
    });
    setSelectedCategories([]);
    setErrors({});
    setImage([]);
    setImageFile(null);
  };

  const closeForm = () => {
    setOpen(false);
    resetForm();
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    const filmVM = {
      film: film,
      selectedCategories: selectedCategories
        ? selectedCategories.map((category) => category.value)
        : [],
      imageFile: imageFile,
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
    const formData = new FormData();
    const filmCateDTO = {
      film: film,
      selectedCategories: filmVM.selectedCategories,
    };
    formData.append("filmCateDTO", JSON.stringify(filmCateDTO));
    if (imageFile) {
      formData.append("imageFile", imageFile);
    }
    axios
      .post("/api/film", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
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
          "Error submitting form:",
          error.response?.data || error.message
        );
      });
  };

  return (
    <>
      <Button
        variant="outline-primary"
        onClick={() => setOpen((o) => !o)}
        className={styles.openButton}
      >
        <Film className="me-2" />
        {film.id === 0 ? "Create New Film" : "Update Film"}
      </Button>
      <Popup
        open={openForm}
        closeOnDocumentClick
        onClose={closeForm}
        className={styles.formPopup}
        overlayStyle={{ background: "rgba(0,0,0,0.7)" }}
      >
        <div className={styles.popupContent}>
          <div className={styles.popupHeader}>
            <h2 className={styles.formTitle}>
              {film.id === 0 ? "Create" : "Update"} a Movie
            </h2>
            <XCircleFill className={styles.close} onClick={closeForm} />
          </div>
          <Container className={styles.filmForm}>
            <Form onSubmit={handleSubmit} encType="multipart/form-data">
              <Card className={styles.uploadCard}>
                <Card.Body>
                  <Card.Title className={styles.cardTitle}>
                    <Upload className="me-2" />
                    Drop & Drag Movie Image
                  </Card.Title>
                  <div
                    className={`${styles.dragArea} ${
                      isDragging ? styles.dragging : ""
                    }`}
                    onDragOver={handleDragOver}
                    onDragLeave={handleDragLeave}
                    onDrop={handleDrop}
                  >
                    {isDragging ? (
                      <span className={styles.dropText}>Drop Image Here</span>
                    ) : image.length === 0 ? (
                      <>
                        Drop & Drag Image Here or{" "}
                        <span
                          className={styles.browseText}
                          role="button"
                          onClick={selectFile}
                          tabIndex={0}
                          onKeyPress={(e) => {
                            if (e.key === "Enter") selectFile();
                          }}
                        >
                          Browse file
                        </span>
                      </>
                    ) : (
                      <div className={styles.imagePreview}>
                        <span
                          className={styles.deleteImage}
                          onClick={handleDeleteImg}
                          role="button"
                          tabIndex={0}
                          onKeyPress={(e) => {
                            if (e.key === "Enter") handleDeleteImg();
                          }}
                        >
                          <Trash />
                        </span>
                        <Image
                          src={image[0].url}
                          alt={image[0].name}
                          className={styles.previewImage}
                        />
                      </div>
                    )}
                    <input
                      type="file"
                      className={styles.fileInput}
                      name="file"
                      ref={fileInputRef}
                      onChange={handleFileSelect}
                      accept="image/*"
                    />
                  </div>
                  {errors.imageFile && (
                    <Form.Text className={styles.error}>
                      {errors.imageFile}
                    </Form.Text>
                  )}
                </Card.Body>
              </Card>
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
                  label={
                    <>
                      <FileFontFill className="me-2" />
                      Title
                    </>
                  }
                  className={styles.customLabel}
                >
                  <Form.Control
                    type="text"
                    value={film.title}
                    onChange={handleChange}
                    placeholder="Enter film title"
                    name="title"
                    isInvalid={errors["film.title"]}
                    className={styles.formInput}
                  />
                  <Form.Control.Feedback type="invalid">
                    {errors["film.title"]}
                  </Form.Control.Feedback>
                </FloatingLabel>
              </Form.Group>
              <Row className="mb-3">
                <Col md={6}>
                  <FloatingLabel
                    controlId="floating-Director"
                    label={
                      <>
                        <PersonFill className="me-2" />
                        Director
                      </>
                    }
                    className={styles.customLabel}
                  >
                    <Form.Control
                      type="text"
                      value={film.director}
                      onChange={handleChange}
                      placeholder="Enter director name"
                      name="director"
                      isInvalid={errors["film.director"]}
                      className={styles.formInput}
                    />
                    <Form.Control.Feedback type="invalid">
                      {errors["film.director"]}
                    </Form.Control.Feedback>
                  </FloatingLabel>
                </Col>
                <Col md={6}>
                  <FloatingLabel
                    controlId="floating-Type"
                    label={
                      <>
                        <Film className="me-2" />
                        Type
                      </>
                    }
                    className={styles.customLabel}
                  >
                    <Form.Select
                      onChange={handleChange}
                      name="type"
                      value={film.type}
                      isInvalid={errors["film.type"]}
                      className={styles.formSelect}
                    >
                      <option value="">Please choose a type</option>
                      <option>TV-Series</option>
                      <option>Movie</option>
                    </Form.Select>
                    <Form.Control.Feedback type="invalid">
                      {errors["film.type"]}
                    </Form.Control.Feedback>
                  </FloatingLabel>
                </Col>
              </Row>
              <Form.Group className="mb-3">
                <FloatingLabel
                  controlId="floating-Synopsis"
                  label={
                    <>
                      <FileEarmarkText className="me-2" />
                      Synopsis
                    </>
                  }
                  className={styles.customLabel}
                >
                  <Form.Control
                    as="textarea"
                    value={film.synopsis}
                    onChange={handleChange}
                    placeholder="Enter film synopsis"
                    name="synopsis"
                    isInvalid={errors["film.synopsis"]}
                    className={styles.formTextarea}
                    style={{ height: "100px" }}
                  />
                  <Form.Control.Feedback type="invalid">
                    {errors["film.synopsis"]}
                  </Form.Control.Feedback>
                </FloatingLabel>
              </Form.Group>
              <Form.Group className="mb-3">
                <label className={styles.selectLabel}>
                  <TagsFill className="me-2" />
                  Categories
                </label>
                <div className={styles.selectWrapper}>
                  <Select
                    options={categoryOption}
                    components={animatedComponents}
                    placeholder="Select Categories for the film"
                    isMulti
                    onChange={(event) => setSelectedCategories(event)}
                    value={selectedCategories}
                    className={styles.categorySelect}
                    styles={{
                      control: (provided, state) => ({
                        ...provided,
                        borderColor: errors.selectedCategories
                          ? "#dc3545"
                          : state.isFocused
                          ? "#3498db"
                          : "#e0e0e0",
                        boxShadow: state.isFocused
                          ? "0 0 0 2px rgba(52, 152, 219, 0.2)"
                          : "0 2px 4px rgba(0, 0, 0, 0.1)",
                        "&:hover": {
                          borderColor: "#3498db",
                        },
                        transition: "all 0.3s ease",
                      }),
                      menu: (provided) => ({
                        ...provided,
                        zIndex: 9999,
                      }),
                    }}
                  />
                </div>
                {errors.selectedCategories && (
                  <Form.Text className={styles.error}>
                    {errors.selectedCategories}
                  </Form.Text>
                )}
              </Form.Group>
              <Button type="submit" className={styles.submitButton}>
                {film.id === 0 ? "Create" : "Update"}
              </Button>
            </Form>
          </Container>
        </div>
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
    blurHash: PropTypes.string,
    filmPath: PropTypes.string,
  }),
};

export default FilmCUForm;
