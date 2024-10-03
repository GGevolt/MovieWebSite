import axios from "axios";
import React, { useRef, useState, useContext, useEffect } from "react";
import { Button, Card, Form } from "react-bootstrap";
import ReactPlayer from "react-player";
import { mixed, number, object } from "yup";
import PropTypes from "prop-types";
import WebContext from "../../../../WebContext/Context";
import AdminContext from "../../AminContext/Context";
import "./Form.css";

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
  }, [passEp]);
  const [tempVid, setTempVid] = useState([]);
  const [errors, setErrors] = useState([]);
  const fileInputRef = useRef(null);
  const [isDragging, setIsDragging] = useState(false);
  const [upMesg, setUpMesg] = useState(null);
  const [progress, setProgress] = useState({ started: false, pc: 0 });
  const validateSchema =
    passEp && passEp.episodeNumber === episode.episodeNumber
      ? object({
          episodeNumber: number()
            .required("Select an episode number!")
            .positive("Episode number must be positive")
            .integer("Episode number must be an integer!"),
          videoFile: mixed().required("Pls, Choose a video to upload."),
        })
      : object({
          episodeNumber: number()
            .required("Select an episode number!")
            .positive("Episode number must be positive")
            .integer("Episode number must be an integer!")
            .notOneOf(
              filmEpisodes.map((ep) => ep.episodeNumber),
              "This episode is already exist!"
            ),
          videoFile: mixed().required("Pls, Choose a video to upload."),
        });

  const selectFile = () => {
    fileInputRef.current.click();
  };

  const excludeFile = () => {
    setTempVid([]);
    setEpisode((prev) => ({ ...prev, videoFile: [] }));
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
    epData.append("videoFile", episode.videoFile);
    setUpMesg("Uploading....");
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
        setUpMesg("Successs");
        await new Promise((resolve) => setTimeout(resolve, 5000));
        setProgress({ started: false, pc: 0 });
        setUpMesg(null);
        await getFilmEps(episode.filmId);
        resetForm();
      })
      .catch(async (error) => {
        setUpMesg("Fail");
        await new Promise((resolve) => setTimeout(resolve, 5000));
        setProgress({ started: false, pc: 0 });
        setUpMesg(null);
        console.error("There has been a problem with add new episode:", error);
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
    <Form onSubmit={handleSubmit} encType="multipart/form-data">
      <Form.Group className="mb-2">
        <Form.Label>Episode number:</Form.Label>
        <Form.Control
          type="number"
          min="0"
          value={episode.episodeNumber}
          name="episodeNumber"
          onChange={handleChange}
          isInvalid={errors.episodeNumber}
        />
        <Form.Control.Feedback className="error" type="invalid">
          {errors.episodeNumber}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-2 vid-upload">
        <Form.Label>Upload video:</Form.Label>
        {tempVid.length !== 0 ? (
          <div className="temp-vid">
            <ReactPlayer
              url={tempVid[0].url}
              controls
              width="100%"
              height="100%"
            />
          </div>
        ) : (
          episode.vidName && (
            <div className="temp-vid">Video Name: {episode.vidName}</div>
          )
        )}
        <Card className="upload-card">
          <Card.Title className="top">Drop & Drag video</Card.Title>
          <div className="vid-up-section">
            <div
              className="drag-area"
              onDragOver={handleDragOver}
              onDragLeave={handleDragLeave}
              onDrop={(event) => handleDrop(event)}
            >
              {isDragging ? (
                <span className="select">Drop Video Here</span>
              ) : tempVid.length === 0 ? (
                <>
                  Drop & Drag Video Here or {""}
                  <span className="select" role="button" onClick={selectFile}>
                    Browse file
                  </span>
                </>
              ) : (
                <p>
                  {tempVid[0].name}.{" "}
                  <span
                    className="exclude-vid"
                    role="button"
                    onClick={excludeFile}
                  >
                    {" "}
                    Exclude video
                  </span>
                </p>
              )}
            </div>
          </div>
          <Form.Control
            type="file"
            className="file"
            name="file"
            ref={fileInputRef}
            onChange={(event) => handleFileSelect(event)}
          />
        </Card>
        {errors.videoFile && (
          <Form.Text className="error">{errors.videoFile}</Form.Text>
        )}
      </Form.Group>
      {progress.started ? (
        <div>
          <progress max="100" value={progress.pc}></progress>
          <span>{upMesg}</span>
        </div>
      ) : (
        <>
          {!passEp ? (
            <Button type="submit">Add new episode</Button>
          ) : (
            <>
              <Button type="submit">Update episode</Button>
              <Button variant="outline-danger" onClick={() => handleDelete()}>
                Delete
              </Button>
            </>
          )}
        </>
      )}
    </Form>
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
