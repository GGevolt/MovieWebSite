import { Container, Row, Col, Button } from "react-bootstrap";
import React, { useState, useEffect } from "react";
import { ChevronLeft, ChevronRight } from "react-bootstrap-icons";
import PropTypes from "prop-types";
import FilmImg from "../Img/FilmImg";
import "./index.css";
import { useNavigate } from "react-router-dom";

const FilmRow = ({ films, filmsRowTitle }) => {
  const nav = useNavigate();
  const [currentPage, setCurrentPage] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(6);
  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth < 576) {
        setItemsPerPage(2);
      } else if (window.innerWidth < 768) {
        setItemsPerPage(3);
      } else if (window.innerWidth < 992) {
        setItemsPerPage(4);
      } else if (window.innerWidth < 1200) {
        setItemsPerPage(5);
      } else {
        setItemsPerPage(6);
      }
    };
    handleResize();
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);
  const navigate = (direction) => {
    if (direction === "prev") {
      setCurrentPage((prev) => Math.max(0, prev - 1));
    } else {
      setCurrentPage((prev) =>
        Math.min(Math.ceil(films.length / itemsPerPage) - 1, prev + 1)
      );
    }
  };
  const currentFilms = films.slice(
    currentPage * itemsPerPage,
    (currentPage + 1) * itemsPerPage
  );
  return (
    <Container fluid className="film-container py-4">
      <h2 className="mb-3">{filmsRowTitle}</h2>
      <div className="film-row-container">
        {currentPage !== 0 && (
          <div className="button-container">
            <Button
              variant="dark"
              className="scroll-button scroll-left"
              onClick={() => navigate("prev")}
              aria-label="Previous series"
            >
              <ChevronLeft />
            </Button>
          </div>
        )}
        <Row className="film-row mx-0">
          {currentFilms.map((film) => (
            <Col
              key={film.id}
              xs={6}
              sm={4}
              md={3}
              lg={2}
              className="mb-4 px-2"
            >
              <div
                className="film-card-wrapper"
                onClick={() => nav(`/user/detail/${film.id}`)}
                role="button"
                tabIndex={0}
              >
                <div className="tv-series-card">
                  <FilmImg
                    src={`/api/images/${film.filmPath}`}
                    hash={film.blurHash}
                  />
                  <div className="film-title">{film.title}</div>
                </div>
              </div>
            </Col>
          ))}
        </Row>
        {currentPage !== Math.ceil(films.length / itemsPerPage) - 1 && (
          <div className="button-container">
            <Button
              variant="dark"
              className="scroll-button scroll-right"
              onClick={() => navigate("next")}
              aria-label="Next series"
            >
              <ChevronRight />
            </Button>
          </div>
        )}
      </div>
    </Container>
  );
};

FilmRow.propTypes = {
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
  filmsRowTitle: PropTypes.string,
};

export default FilmRow;
