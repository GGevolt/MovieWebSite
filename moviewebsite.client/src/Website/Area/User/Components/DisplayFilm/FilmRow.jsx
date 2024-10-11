import React, { useState, useEffect, useRef } from "react";
import { Container, Button } from "react-bootstrap";
import { ChevronLeft, ChevronRight } from "react-bootstrap-icons";
import PropTypes from "prop-types";
import { motion, AnimatePresence } from "framer-motion";
import FilmImg from "../Img/FilmImg";
import styles from "./FilmRow.module.css";
import { useNavigate } from "react-router-dom";

const FilmRow = ({ films, filmsRowTitle }) => {
  const nav = useNavigate();
  const [currentPage, setCurrentPage] = useState(0);
  const [itemsPerPage, setItemsPerPage] = useState(6);
  const containerRef = useRef(null);

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

  return (
    <Container fluid className={styles.filmContainer}>
      <h2 className={`mb-3 ${styles.rowTitle}`}>{filmsRowTitle}</h2>
      <div className={styles.filmRowContainer} ref={containerRef}>
        <AnimatePresence>
          {currentPage !== 0 && (
            <motion.div
              className={`${styles.buttonContainer} ${styles.leftButton}`}
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
            >
              <Button
                variant="dark"
                className={`${styles.scrollButton} ${styles.scrollLeft}`}
                onClick={() => navigate("prev")}
                aria-label="Previous series"
              >
                <ChevronLeft />
              </Button>
            </motion.div>
          )}
        </AnimatePresence>
        <motion.div
          className={styles.filmRow}
          initial={{ x: 0 }}
          animate={{ x: `-${currentPage * 100}%` }}
          transition={{ type: "tween", ease: "easeInOut", duration: 0.5 }}
        >
          {films.map((film) => (
            <div key={film.id} className={styles.filmCardWrapper}>
              <motion.div
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
                className={styles.tvSeriesCard}
                onClick={() => nav(`/user/detail/${film.id}`)}
                role="button"
                tabIndex={0}
              >
                <FilmImg
                  src={`/api/images/${film.filmPath}`}
                  hash={film.blurHash}
                />
                <div className={styles.filmTitle}>{film.title}</div>
              </motion.div>
            </div>
          ))}
        </motion.div>
        <AnimatePresence>
          {currentPage !== Math.ceil(films.length / itemsPerPage) - 1 && (
            <motion.div
              className={`${styles.buttonContainer} ${styles.rightButton}`}
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
            >
              <Button
                variant="dark"
                className={`${styles.scrollButton} ${styles.scrollRight}`}
                onClick={() => navigate("next")}
                aria-label="Next series"
              >
                <ChevronRight />
              </Button>
            </motion.div>
          )}
        </AnimatePresence>
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
      filmPath: PropTypes.string,
      blurHash: PropTypes.string,
    })
  ).isRequired,
  filmsRowTitle: PropTypes.string,
};

export default FilmRow;