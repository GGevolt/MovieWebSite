import PropTypes from "prop-types";
import { useNavigate } from "react-router-dom";
import FilmImg from "../../Components/Img/FilmImg";
import { Carousel } from "react-bootstrap";
import "./Carousel.css";

export default function CarouselDisplay({ films }) {
  const navigate = useNavigate();
  return (
    <div className="film-carousel-container">
      <Carousel interval={null}>
        {films.map((film) => (
          <Carousel.Item key={film.id} onClick={() => navigate(`/user/detail/${film.id}`)}>
            <div className="row">
              <div className="col film-info">
                <h2>{film.title}</h2>
                <h3>Directed by {film.director}</h3>
                <p>{film.synopsis}</p>
              </div>
              <div className="col film-image">
                <FilmImg
                  src={`/api/images/${film.filmPath}`}
                  hash={film.blurHash}
                />
              </div>
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
      filmImg: PropTypes.string,
    })
  ).isRequired,
};
