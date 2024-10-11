import { useState, useEffect, useContext } from 'react';
import { Container, Row, Col, Button, Spinner } from 'react-bootstrap';
import { useNavigate, Navigate } from 'react-router-dom';
import { motion } from 'framer-motion';
import { Shuffle } from 'lucide-react';
import styles from './Playlist.module.css';
import AuthContext from '../../../AuthContext/Context';
import FilmImg from '../../Components/Img/FilmImg';

function Playlist() {
    const authContext = useContext(AuthContext);
    const { getUserPlayList, roles } = authContext;
    const [playlist, setPlaylist] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
  
    useEffect(() => {
      fetchPlaylist();
    }, []);
  
    const fetchPlaylist = async () => {
      setPlaylist(await getUserPlayList());
      setLoading(false);
    };
  
    const handleRandomFilm = () => {
      if (playlist.length > 0) {
        const randomIndex = Math.floor(Math.random() * playlist.length);
        const randomFilm = playlist[randomIndex];
        navigate(`/user/detail/${randomFilm.id}`);
      }
    };
  
    const handleFilmClick = (filmId) => {
      navigate(`/user/detail/${filmId}`);
    };
  
    if (loading) {
      return (
        <Container className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
          <Spinner animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        </Container>
      );
    }
  
    if (!roles.includes("UserT2")) {
      return <Navigate to="/" />;
    }
  
    return (
      <Container fluid className={styles.playlistContainer}>
        <motion.h1
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          className={styles.title}
        >
          Your Playlist
        </motion.h1>
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.5, delay: 0.2 }}
          className="d-flex justify-content-center mb-4"
        >
          <Button
            variant="primary"
            onClick={handleRandomFilm}
            disabled={playlist.length === 0}
            className={styles.randomButton}
          >
            <Shuffle className="me-2" size={20} />
            Play Random Film
          </Button>
        </motion.div>
        {playlist.length === 0 ? (
          <p className={styles.emptyMessage}>Your playlist is empty.</p>
        ) : (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ duration: 0.5, delay: 0.4 }}
            className={styles.resultsContainer}
          >
            <Row className={styles.filmRow}>
              {playlist.map((film) => (
                <Col key={film.id} xs={6} sm={4} md={3} lg={2} className="mb-4 px-2">
                  <div 
                    className={styles.filmCardWrapper}
                    onClick={() => handleFilmClick(film.id)}
                    role="button"
                    tabIndex={0}
                  >
                    <div className={styles.tvSeriesCard}>
                      <FilmImg
                        src={`/api/images/${film.filmPath}`}
                        hash={film.blurHash}
                      />
                      <div className={styles.filmTitle}>{film.title}</div>
                    </div>
                  </div>
                </Col>
              ))}
            </Row>
          </motion.div>
        )}
      </Container>
    );
}

export default Playlist;