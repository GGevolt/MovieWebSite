import React, { useEffect, useState, useContext } from "react";
import { Button, Card, Container, Row, Col } from "react-bootstrap";
import { useLoaderData } from "react-router-dom";
import EpisodeForm from "../../Components/Form/EpisodeForm.jsx";
import WebContext from "../../../../WebContext/Context";
import { Plus, PencilSquare } from "react-bootstrap-icons";
import styles from "./VideoManagement.module.css";

function VideoManagement() {
  const webContext = useContext(WebContext);
  const { filmEpisodes, getFilmEps } = webContext;
  const film = useLoaderData();
  const [isAdd, setIsAdd] = useState(true);
  const [episode, setEp] = useState(null);

  useEffect(() => {
    if (film) {
      loadData();
    }
  }, [film]);

  const loadData = async () => {
    await getFilmEps(film.id);
  };

  const handleAdd = () => {
    setIsAdd(true);
    setEp(null);
  };

  const handleEdit = (ep) => {
    setIsAdd(false);
    setEp(ep);
  };
  return (
    <div className={styles.videoManagement}>
      <Container fluid>
        <Row className={styles.header}>
          <Col>
            <h1 className={styles.title}>{film.title}</h1>
          </Col>
        </Row>
        <Row>
          <Col lg={3} className={styles.sidebar}>
            <Card className={styles.filmCard}>
              <Card.Img
                variant="top"
                src={`/api/images/${film.filmPath}`}
                alt="Movie poster"
                className={styles.poster}
              />
              <Card.Body>
                <Card.Title className={styles.filmTitle}>
                  {film.title}
                </Card.Title>
                <Card.Text className={styles.filmInfo}>
                  <span className={styles.infoLabel}>Type:</span> {film.type}
                </Card.Text>
                <Card.Text className={styles.filmInfo}>
                  <span className={styles.infoLabel}>Episodes:</span>{" "}
                  {filmEpisodes.length}
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
          <Col lg={9}>
            <Card className={styles.mainCard}>
              <Card.Body>
                <div className={styles.episodeHeader}>
                  <h2 className={styles.episodeTitle}>Episodes</h2>
                  <Button onClick={handleAdd} className={styles.addButton}>
                    <Plus size={20} /> Add New Episode
                  </Button>
                </div>
                <div className={styles.episodeGrid}>
                  {filmEpisodes
                    .sort((a, b) => a.episodeNumber - b.episodeNumber)
                    .map((ep) => (
                      <Card
                        key={ep.id}
                        className={styles.episodeCard}
                        onClick={() => handleEdit(ep)}
                      >
                        <Card.Body>
                          <div className={styles.episodeNumber}>
                            Ep {ep.episodeNumber}
                          </div>
                        </Card.Body>
                      </Card>
                    ))}
                </div>
              </Card.Body>
            </Card>
            <Card className={styles.formCard}>
              <Card.Body>
                <h2 className={styles.formTitle}>
                  {isAdd ? "Add New Episode" : "Edit Episode"}
                </h2>
                <EpisodeForm filmId={film.id} passEp={episode} />
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default VideoManagement;
