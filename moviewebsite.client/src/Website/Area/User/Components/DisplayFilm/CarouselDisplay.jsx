import PropTypes from "prop-types";
import { useNavigate } from "react-router-dom";
import FilmImg from "../../Components/Img/FilmImg";
import { Carousel } from "react-bootstrap";
import styles from './CarouselDisplay.module.css';

export default function CarouselDisplay({ films }) {
  const navigate = useNavigate();
  return (
    <div className={styles.filmCarouselContainer}>
      <Carousel interval={null}>
        {films.map((film) => (
          <Carousel.Item 
            key={film.id} 
            onClick={() => navigate(`/user/detail/${film.id}`)}
            className={styles.carouselItem}
          >
            <div className={styles.filmImage}>
              <FilmImg
                src={`/api/images/${film.filmPath}`}
                hash={film.blurHash}
              />
            </div>
            <div className={styles.filmInfo}>
              <h2>{film.title}</h2>
              <h3>Directed by {film.director}</h3>
              <p className={styles.description}>{film.synopsis}</p>
            </div>
          </Carousel.Item>
        ))}
      </Carousel>
    </div>
  );
}

CarouselDisplay.propTypes = {
  films: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number,
      title: PropTypes.string,
      synopsis: PropTypes.string,
      director: PropTypes.string,
      type: PropTypes.string,
      filmPath: PropTypes.string,
      blurHash: PropTypes.string,
    })
  ).isRequired,
};

