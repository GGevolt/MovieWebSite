import { useContext, useEffect, useState } from "react";
import { Button, Card, Container, Spinner } from "react-bootstrap";
import { Link } from "react-router-dom";
import FilmCUForm from "../../Components/Form/FilmCUForm";
import Pagination from "../../Components/Pagination";
import DeleteButton from "../../Components/Utility/DeleteButton";
import LoadFilmCategories from "../../Components/Utility/LoadFilmCategories";
import WebContext from "../../../../WebContext/Context";
import FilmImg from "../../../User/Components/Img/FilmImg";
import styles from "./FilmManagement.module.css";
import { JournalRichtext } from "react-bootstrap-icons";

function FilmManagement() {
  document.title = "Sodoki-Admin";
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
    <Container className={styles.filmContainer}>
      <Container className={styles.firstLine}>
          <h1>Manage Films</h1>
          <FilmCUForm />
        </Container>
      {currentItems.map((film) => (
        <Card className={styles.filmCard} key={film.id}>
          <Card.Title className={styles.filmTitle}>{film.title}</Card.Title>
          <div className={styles.filmContent}>
            <div className={styles.filmImage}>
              <FilmImg
                src={`/api/images/${film.filmPath}`}
                hash={film.blurHash}
              />
            </div>
            <div className={styles.filmDetails}>
              <div className={styles.filmInfo}>
                <div className={styles.filmInfoDirector}>
                  <strong className="me-2">Director:</strong>{film.director}
                </div>
                <div className={styles.filmInfoCategories}>
                  <strong className="me-2">Categories:</strong>
                  <LoadFilmCategories filmId={film.id} />
                </div>
                <div className={styles.filmInfoType}>
                  <strong className="me-2">Type:</strong>{film.type}
                </div>
              </div>
              <div className={styles.filmFunctions}>
                <strong>Functionalities: </strong>
                <div className={styles.filmFunctionsContent}>
                  <FilmCUForm film={film} />
                  <Button
                    variant="outline-success"
                    as={Link}
                    to={`/Admin/Video-Management/${film.id}`}
                    className={styles.funcBtn}
                  >
                    <JournalRichtext className="me-2"/> Manage Video
                  </Button>
                  <DeleteButton type="film" id={film.id} name={styles.funcBtn} />
                </div>
              </div>
            </div>
          </div>
          <div className={styles.filmSynopsis}>
            <h5>Synopsis</h5>
            {film.synopsis}
          </div>
        </Card>
      ))}
      <div className={styles.pagination}>
        <Pagination
          data={films}
          onPageChange={handlePageChange}
          itemsPerPage={itemsPerPage}
        />
      </div>
    </Container>
  );

  return (
    <div className={styles.body}>
      <Container fluid>
        {isLoading ? (
          <div className={styles.spinnerContainer}>
            <Spinner animation="border" />
          </div>
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