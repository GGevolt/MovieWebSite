import { useState, useEffect, useContext } from "react";
import styles from "./FilmDetail.module.css";
import FilmImg from "../../Components/Img/FilmImg";
import { Play, Clipboard2Check, Clipboard2X } from "react-bootstrap-icons";
import { Container, Row, Col, Badge } from "react-bootstrap";
import { useLoaderData, useNavigate } from "react-router-dom";
import WebApi from "../../../../WebApi";
import FilmRow from "../../Components/DisplayFilm/FilmRow";
import AuthContext from "../../../AuthContext/Context";

function Detail() {
  const film = useLoaderData();
  const authContext = useContext(AuthContext);
  const { roles, addToPlayList } = authContext;
  const [categories, setCategories] = useState([]);
  const [relatedFilms, setRelatedFilms] = useState([]);
  const [isAdded, setIsAdded] = useState(false);
  const navigate = useNavigate();
  useEffect(() => {
    if (film) {
      loadData();
    }
  }, []);
  const loadData = async () => {
    setCategories(await WebApi.getFilmCates(film.id));
    setRelatedFilms(await WebApi.getRelatedFilms(film.id));
    if (roles.includes("UserT2")) {
      setIsAdded(await addToPlayList(film.id, false, false));
      console.log(isAdded);
    }
  };
  const handleAddToPlayList = async () => {
    if (isAdded) {
      setIsAdded(await addToPlayList(film.id, false, true));
    } else {
      setIsAdded(await addToPlayList(film.id, true, false));
    }
  };
  const hanldeNavigate = (destination) => {
    if (roles.length <= 1) {
      navigate("/user/memberships");
      return;
    }
    navigate(destination);
  };
  return (
    <div className={styles.filmDetail}>
      <div
        className={styles.backdrop}
        style={{ backgroundImage: `url(/api/images/${film.filmPath})` }}
      >
        <div className={styles.gradientOverlay}></div>
      </div>
      <Container fluid className={styles.content}>
        <Row>
          <Col md={6} lg={4}>
            <FilmImg
              src={`/api/images/${film.filmPath}`}
              hash={film.blurHash}
              name={styles.poster}
            />
          </Col>
          <Col md={6} lg={8}>
            <h1 className={styles.title}>{film.title}</h1>
            <div className={styles.metadata}>
              <Badge bg="danger">{film.type}</Badge>
              {categories.map((category, index) => (
                <Badge key={index} bg="secondary" className="ms-2">
                  {category.name}
                </Badge>
              ))}
            </div>
            <p className={styles.synopsis}>{film.synopsis}</p>
            <p className={styles.director}>
              <strong>Director:</strong> {film.director}
            </p>
            <div className={styles.actions}>
              <button
                className={styles.playButton}
                onClick={() => hanldeNavigate(`/user/watchfilm/${film.id}`)}
              >
                <Play size={20} /> Play
              </button>
              {roles.includes("UserT2") && isAdded ? (
                <button
                  className={styles.removeButton}
                  onClick={() => handleAddToPlayList()}
                >
                  <Clipboard2X size={20} /> Remove from playlist
                </button>
              ) : (
                <button
                  className={styles.addButton}
                  onClick={() => handleAddToPlayList()}
                >
                  <Clipboard2Check size={20} /> Add to play list
                </button>
              )}
            </div>
          </Col>
        </Row>
      </Container>
      <Container fluid className={styles.relatedContent}>
        {relatedFilms.length != 0 && (
          <FilmRow films={relatedFilms} filmsRowTitle={"More Like This"} />
        )}
      </Container>
    </div>
  );
}

export default Detail;
