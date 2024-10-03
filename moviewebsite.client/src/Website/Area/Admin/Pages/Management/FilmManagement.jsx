import React, { useContext, useEffect, useState } from "react";
import { Button, Card, Container, Spinner } from "react-bootstrap";
import { Link } from "react-router-dom";
import FilmCUForm from "../../Components/Form/FilmCUForm";
import Pagination from "../../Components/Pagination";
import PictureUpload from "../../Components/Upload/PictureUpload";
import DeleteButton from "../../Components/Utility/DeleteButton";
import LoadFilmCategories from "../../Components/Utility/LoadFilmCategories";
import WebContext from "../../../../WebContext/Context";
import FilmImg from "../../../User/Components/Img/FilmImg";
import "./Management.css";

function FilmManagement() {
  const [isLoading, setIsLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 6;
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const webContext = useContext(WebContext);
  const { films, getFilms } = webContext;
  const currentItems = films
    ? films.slice(indexOfFirstItem, indexOfLastItem)
    : 0;
  useEffect(() => {
    loadFilmsData();
  }, []);
  const handlePageChange = (page) => {
    setCurrentPage(page);
  };
  const loadFilmsData = async () => {
    await getFilms();
    setIsLoading(false);
  };

  const displayFilms = (
    <Container className="film-container">
      {currentItems.map((film) => (
        <Card className="film-card" key={film.id}>
          <Card.Title className="film-title">{film.title}</Card.Title>
          <div className="film-image">
            <FilmImg
              src={`/api/images/${film.filmPath}`}
              hash={film.blurHash}
            />
          </div>
          <div className="film-info">
            <div className="film-info-director">
              <strong>Director:</strong> {film.director}
            </div>
            <div className="film-info-categories">
              <strong>Categories:</strong>{" "}
              <LoadFilmCategories filmId={film.id} />
            </div>
            <div className="film-info-type">
              <strong>Type:</strong> {film.type}
            </div>
          </div>
          <div className="film-functions">
            <strong>Functionalities: </strong>
            <div className="film-functions-content">
              <PictureUpload id={film.id} />
              <FilmCUForm film={film} />
              <Button
                variant="outline-success"
                as={Link}
                to={`/Admin/Video-Management/${film.id}`}
              >
                Manage Video
              </Button>
              <DeleteButton type="film" id={film.id} />
            </div>
          </div>
          <div className="film-synopsis">
            <h5>Synopsis</h5>
            {film.synopsis}
          </div>
        </Card>
      ))}
      <div className="Pag">
        <Pagination
          data={films}
          onPageChange={handlePageChange}
          itemsPerPage={itemsPerPage}
        />
      </div>
    </Container>
  );

  return (
    <div className="body">
      <Container fluid>
        <Container className="firstLine">
          <h1>Manage Films</h1>
          <FilmCUForm />
        </Container>
        {isLoading ? (
          <Spinner animation="border" />
        ) : films.length > 0 ? (
          displayFilms
        ) : (
          <h4>No Movies found.</h4>
        )}
      </Container>
    </div>
  );
}
export default FilmManagement;
