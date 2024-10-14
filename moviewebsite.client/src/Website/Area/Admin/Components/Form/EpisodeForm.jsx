import axios from "axios";
import { useRef, useState, useContext, useEffect } from "react";
import { Button, Card, Form, Container, Row, Col } from "react-bootstrap";
import ReactPlayer from "react-player";
import { mixed, number, object } from "yup";
import PropTypes from "prop-types";
import WebContext from "../../../../WebContext/Context";
import AdminContext from "../../AminContext/Context";
import { Upload, Trash, Play } from "react-bootstrap-icons";
import styles from "./EpisodeForm.module.css";

function EpisodeCForm({ filmId, passEp }) {
  const webContext = useContext(WebContext);
  const { filmEpisodes, getFilmEps } = webContext;
  const adminContext = useContext(AdminContext);
  const { Delete } = adminContext;
  const [episode, setEpisode] = useState({
    id: 0,
    episodeNumber: 0,
    filmId: filmId,
    videoFile: null,
    vidName: null,
  });

  useEffect(() => {
    setEpisode(() => ({
      id: passEp ? passEp.id : 0,
      episodeNumber: passEp ? passEp.episodeNumber : 0,
      filmId: filmId,
      videoFile: null,
      vidName: passEp ? passEp.vidName : null,
    }));
  }, [passEp, filmId]);

  const [tempVid, setTempVid] = useState([]);
  const [errors, setErrors] = useState([]);
  const fileInputRef = useRef(null);
  const [isDragging, setIsDragging] = useState(false);
  const [upMesg, setUpMesg] = useState(null);
  const [progress, setProgress] = useState({ started: false, pc: 0 });

  const validateSchema = passEp
    ? object({
        episodeNumber: number()
          .required("Select an episode number!")
          .positive("Episode number must be positive")
          .integer("Episode number must be an integer!")
          .notOneOf(
            filmEpisodes
              .map((ep) => ep.episodeNumber)
              .filter((item) => item !== passEp.episodeNumber),
            "This episode already exists!"
          ),
      })
    : object({
        episodeNumber: number()
          .required("Select an episode number!")
          .positive("Episode number must be positive")
          .integer("Episode number must be an integer!")
          .notOneOf(
            filmEpisodes.map((ep) => ep.episodeNumber),
            "This episode already exists!"
          ),
        videoFile: mixed().required("Please choose a video to upload."),
      });

  const selectFile = () => {
    fileInputRef.current.click();
  };

  const excludeFile = () => {
    setTempVid([]);
    setEpisode((prev) => ({ ...prev, videoFile: null }));
  };

  const handleChange = (event) => {
    setEpisode((prev) => ({
      ...prev,
      [event.target.name]: event.target.value,
    }));
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
    if (!file || file.type.split("/")[0] !== "video") {
      return;
    }
    setTempVid(() => [
      {
        name: file.name,
        url: URL.createObjectURL(file),
      },
    ]);
    setEpisode((prev) => ({ ...prev, videoFile: file }));
  };

  const handleFileSelect = (event) => {
    const file = event.target.files[0];
    if (!file || file.type.split("/")[0] !== "video") {
      return;
    }
    setTempVid(() => [
      {
        name: file.name,
        url: URL.createObjectURL(file),
      },
    ]);
    setEpisode((prev) => ({ ...prev, videoFile: file }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      await validateSchema.validate(episode, { abortEarly: false });
      setErrors({});
    } catch (error) {
      const newErrors = {};
      error.inner.forEach((err) => {
        newErrors[err.path] = err.message;
      });
      setErrors(newErrors);
      return;
    }
    const epData = new FormData();
    epData.append("id", episode.id);
    epData.append("episodeNumber", episode.episodeNumber);
    epData.append("filmId", episode.filmId);
    if (episode.vidName) {
      epData.append("vidName", episode.vidName);
    }
    epData.append("videoFile", episode.videoFile ? episode.videoFile : null);
    setUpMesg("Uploading...");
    setProgress((prev) => {
      return { ...prev, started: true };
    });
    await axios
      .post("/api/episode", epData, {
        onUploadProgress: (e) => {
          setProgress((prev) => {
            return { ...prev, pc: e.progress * 100 };
          });
        },
      })
      .then(async () => {
        setUpMesg("Success");
        await new Promise((resolve) => setTimeout(resolve, 5000));
        setProgress({ started: false, pc: 0 });
        setUpMesg(null);
        await getFilmEps(episode.filmId);
        resetForm();
      })
      .catch(async (error) => {
        setUpMesg("Failed");
        await new Promise((resolve) => setTimeout(resolve, 5000));
        setProgress({ started: false, pc: 0 });
        setUpMesg(null);
        console.error("There has been a problem adding a new episode:", error);
      });
  };

  const resetForm = () => {
    setTempVid([]);
    setEpisode({
      id: 0,
      episodeNumber: 0,
      filmId: filmId,
      videoFile: null,
      vidName: null,
    });
  };

  const handleDelete = async () => {
    let type = "episode";
    let id = episode.id;
    await Delete(type, id);
    await getFilmEps(filmId);
    resetForm();
  };

  return (
    <Container className={styles.episodeForm}>
      <Form onSubmit={handleSubmit} encType="multipart/form-data">
        <Row>
          <Col md={6}>
            <Form.Group className="mb-3">
              <Form.Label className={styles.label}>Episode number:</Form.Label>
              <Form.Control
                type="number"
                min="0"
                value={episode.episodeNumber}
                name="episodeNumber"
                onChange={handleChange}
                isInvalid={errors.episodeNumber}
                className={styles.input}
              />
              <Form.Control.Feedback className={styles.error} type="invalid">
                {errors.episodeNumber}
              </Form.Control.Feedback>
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form.Group className={`mb-3 ${styles.videoUpload}`}>
              <Form.Label className={styles.label}>Upload video:</Form.Label>
              <Card className={styles.uploadCard}>
                <Card.Body>
                  <div
                    className={`${styles.dragArea} ${
                      isDragging ? styles.dragging : ""
                    }`}
                    onDragOver={handleDragOver}
                    onDragLeave={handleDragLeave}
                    onDrop={handleDrop}
                  >
                    {isDragging ? (
                      <span className={styles.dropText}>Drop Video Here</span>
                    ) : tempVid.length === 0 ? (
                      <>
                        <Upload className={`me-2 ${styles.uploadIcon}`} />
                        <span className={styles.dropText}>
                          Drop & Drag Video Here or{" "}
                          <span
                            className={styles.browseText}
                            role="button"
                            onClick={selectFile}
                          >
                            Browse file
                          </span>
                        </span>
                      </>
                    ) : (
                      <p className={styles.fileName}>
                        {tempVid[0].name}{" "}
                        <span
                          className={styles.excludeVid}
                          role="button"
                          onClick={excludeFile}
                        >
                          <Trash />
                        </span>
                      </p>
                    )}
                  </div>
                  <Form.Control
                    type="file"
                    className={styles.fileInput}
                    name="file"
                    ref={fileInputRef}
                    onChange={handleFileSelect}
                    accept="video/*"
                  />
                </Card.Body>
              </Card>
              {errors.videoFile && (
                <Form.Text className={styles.error}>
                  {errors.videoFile}
                </Form.Text>
              )}
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col>
            {tempVid.length !== 0 ? (
              <div className={styles.videoPreview}>
                <ReactPlayer
                  url={tempVid[0].url}
                  controls
                  width="100%"
                  height="100%"
                />
              </div>
            ) : (
              episode.vidName && (
                <div className={styles.videoName}>
                  <Play className={styles.playIcon} />
                  Video Name: {episode.vidName}
                </div>
              )
            )}
          </Col>
        </Row>
        <Row className="mt-3">
          <Col>
            {progress.started ? (
              <div className={styles.progressContainer}>
                <div
                  className={styles.progressBar}
                  style={{ width: `${progress.pc}%` }}
                ></div>
                <span className={styles.progressText}>{upMesg}</span>
              </div>
            ) : (
              <>
                {!passEp ? (
                  <Button type="submit" className={styles.submitButton}>
                    Add new episode
                  </Button>
                ) : (
                  <>
                    <Button type="submit" className={styles.submitButton}>
                      Update episode
                    </Button>
                    <Button
                      variant="outline-danger"
                      onClick={handleDelete}
                      className={styles.deleteButton}
                    >
                      Delete
                    </Button>
                  </>
                )}
              </>
            )}
          </Col>
        </Row>
      </Form>
    </Container>
  );
}

EpisodeCForm.propTypes = {
  filmId: PropTypes.number.isRequired,
  passEp: PropTypes.shape({
    id: PropTypes.number,
    episodeNumber: PropTypes.number,
    filmId: PropTypes.number,
    vidName: PropTypes.string,
  }),
};

export default EpisodeCForm;
