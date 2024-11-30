import { useState, useEffect, useContext } from "react";
import styles from "./FilmDetail.module.css";
import FilmImg from "../../Components/Img/FilmImg";
import { Play, Clipboard2Check, Clipboard2X, Star, StarFill, StarHalf } from "react-bootstrap-icons";
import { Container, Row, Col, Badge } from "react-bootstrap";
import { useLoaderData, useNavigate } from "react-router-dom";
import FilmRow from "../../Components/DisplayFilm/FilmRow";
import AuthContext from "../../../AuthContext/Context";
import AuthApi from "../../../AuthApi";
import WebApi from "../../../../WebApi";

function Detail() {
  const {film, relatedFilms, filmCates, filmScore} = useLoaderData();
  const authContext = useContext(AuthContext);
  const { roles } = authContext;
  const [isAdded, setIsAdded] = useState(false);
  const [rating, setRating] = useState(-1);
  const [hover, setHover] = useState(0);
  const navigate = useNavigate();
  const [avarageScore, setAvargeScore] = useState(-1);

  useEffect(() => {
    if(film){
      loadData();
    }
  }, [film]);

  const loadData = async () => {
    if (roles.includes("UserT2") || roles.includes("UserT1")) {
      const res = await AuthApi.userFilmLogic({filmId: film.id});
      setIsAdded(res.playListAdded);
      setRating(res.rating);
    } else {
      setIsAdded(false);
      setRating(-1);
    }
    setAvargeScore(filmScore);
    setHover(0);
  };

  const handleAddToPlayList = async () => {
    if (isAdded) {
      const res = await AuthApi.userFilmLogic({filmId: film.id, isRemoveFromPlayList: true});
      setIsAdded(res.playListAdded);
      setRating(res.rating);
    } else {
      const res = await AuthApi.userFilmLogic({filmId: film.id, isAddPlayList: true});
      setIsAdded(res.playListAdded);
      setRating(res.rating);
    }
  };

  const handleRating = async (value) => {
    const res = await AuthApi.userFilmLogic({filmId: film.id, filmRatting: value});
    setIsAdded(res.playListAdded);
    setRating(res.rating);
    setHover(0); 
    setAvargeScore(await WebApi.getFilmsScore(film.id));
  }

  const hanldeNavigate = (destination) => {
    if (roles.length <= 1) {
      navigate("/user/memberships");
      return;
    }
    navigate(destination);
  };

  const renderStar = (index) => {
    const fillValue = Math.min(Math.max((hover || rating) - (index - 1) * 2, 0), 2);
    if (fillValue >= 2) return <StarFill className={styles.star} />;
    if (fillValue >= 1) return <StarHalf className={styles.star} />;
    return <Star className={styles.star} />;
  };

  const displayRatingSelect= () =>{
    return (
      <div className={styles.ratingContainer}>
        <strong className="me-2">Your Rating:</strong>
        <div className={styles.starRating}>
          {[1, 2, 3, 4, 5].map((index) => (
            <div key={index} className={styles.starContainer}>
              <button
                type="button"
                className={`${styles.starButton} ${index * 2 <= (hover || rating) ? styles.on : styles.off}`}
                onClick={() => handleRating(index * 2)}
                onMouseEnter={() => setHover(index * 2)}
                onMouseLeave={() => setHover(0)}
              >
                {renderStar(index)}
              </button>
              <button
                type="button"
                className={`${styles.halfStarButton} ${index * 2 - 1 <= (hover || rating) ? styles.on : styles.off}`}
                onClick={() => handleRating(index * 2 - 1)}
                onMouseEnter={() => setHover(index * 2 - 1)}
                onMouseLeave={() => setHover(0)}
              >
                <div className={styles.halfStarOverlay}>{renderStar(index)}</div>
              </button>
            </div>
          ))}
        </div>
        <span className={styles.ratingText}>
          {rating === -1 ? "No rating" : `${rating} out of 10`}
        </span>
      </div>
    );
  }

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
              {filmCates.map((category, index) => (
                <Badge key={index} bg="secondary" className="ms-2">
                  {category.name}
                </Badge>
              ))}
            </div>
            <p className={styles.synopsis}>{film.synopsis}</p>
            <p className={styles.score}>
              <strong>Avarage score:</strong> {avarageScore === -1 ? "There no rating yet!😥" : avarageScore }
            </p>
            <p className={styles.director}>
              <strong>Director:</strong> {film.director}
            </p>
            { roles.length > 1 && displayRatingSelect() }
            <div className={styles.actions}>
              <button
                className={styles.playButton}
                onClick={() => hanldeNavigate(`/user/watchfilm/${film.id}`)}
              >
                <Play size={20} /> Play
              </button>
              {roles.includes("UserT2") && (
                <button
                  className={isAdded ? styles.removeButton : styles.addButton}
                  onClick={() => handleAddToPlayList()}
                >
                  {isAdded ? (
                    <><Clipboard2X size={20} /> Remove from playlist</>
                  ) : (
                    <><Clipboard2Check size={20} /> Add to play list</>
                  )}
                </button>
              )}
            </div>
          </Col>
        </Row>
      </Container>
      <Container fluid className={styles.relatedContent}>
        {relatedFilms.length !== 0 && (
          <FilmRow films={relatedFilms} filmsRowTitle={"More Like This"} />
        )}
      </Container>
    </div>
  );
}

export default Detail;