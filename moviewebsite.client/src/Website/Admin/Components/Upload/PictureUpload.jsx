import Popup from "reactjs-popup";
import React, { useState, useRef, useContext } from "react";
import { Card, Button, Container, Image } from "react-bootstrap";
import PropTypes from "prop-types";
import axios from "axios";
import WebContext from "../../../WebContext/Context";
import "./Upload.css";

function PictureUpload({ id }) {
  const webContext = useContext(WebContext);
  const { getFilms } = webContext;
  const [image, setImage] = useState([]);
  const [imageFile, setImageFile] = useState(null);
  const [isDragging, setIsDragging] = useState(false);
  const fileInputRef = useRef(null);
  const [open, setOpen] = useState(false);
  const closeForm = () => {
    setOpen(false);
    setImage([]);
    setImageFile(null);
  };
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
  const handleDelete = () => {
    setImage([]);
    setImageFile(null);
  };
  const handleUploadFile = async () => {
    if (imageFile && id > 0) {
      const filmPicData = new FormData();
      filmPicData.append("filmId", id);
      filmPicData.append("imageFile", imageFile);
      await axios
        .post("/images", filmPicData)
        .then(async () => {
          await getFilms();
          closeForm();
        })
        .catch((error) => {
          console.error(
            "There has been a problem with the film picture submission:",
            error
          );
        });
    }
  };
  return (
    <>
      <Button variant="outline-success" onClick={() => setOpen((o) => !o)}>
        Upload Image
      </Button>
      <Popup open={open} closeOnDocumentClick onClose={closeForm}>
        <Card className="upload-card">
          <Card.Title className="top">Drop & Drag Movie Image</Card.Title>
          <div
            className="drag-area"
            onDragOver={handleDragOver}
            onDragLeave={handleDragLeave}
            onDrop={handleDrop}
          >
            {isDragging ? (
              <span className="select">Drop Image Here</span>
            ) : image.length === 0 ? (
              <>
                Drop & Drag Image Here or {""}
                <span className="select" role="button" onClick={selectFile}>
                  Browse file
                </span>
              </>
            ) : (
              <Container>
                <div className="image">
                  <span className="delete" onClick={handleDelete}>
                    &times;
                  </span>
                  <Image src={image[0].url} alt={image[0].name} />
                </div>
              </Container>
            )}
            <input
              type="file"
              className="file"
              name="file"
              ref={fileInputRef}
              onChange={handleFileSelect}
            />
          </div>
          <Button onClick={() => handleUploadFile()}>Upload</Button>
        </Card>
      </Popup>
    </>
  );
}

PictureUpload.propTypes = {
  id: PropTypes.number.isRequired,
};
export default PictureUpload;
