import React, { useEffect, useState, useContext } from "react";
import { Button, Card, Container, Image } from "react-bootstrap";
import { useLoaderData } from "react-router-dom";
import EpisodeForm from "../../Components/Form/EpisodeForm.jsx";
import WebContext from "../../../../WebContext/Context";
import "./Management.css";

function VideoManagement() {
  const webContext = useContext(WebContext);
  const { filmEpisodes, getFilmEps } = webContext;
  const film = useLoaderData();
  useEffect(() => {
    if (film) {
      loadData();
    }
  }, [film]);
  const [isAdd, setIsAdd] = useState(true);
  const [episode, setEp] = useState(null);
  const loadData = async () => {
    await getFilmEps(film.id);
  };
  const handeAdd = () => {
    setIsAdd(true);
  };
  const handleEdit = (ep) => {
    setIsAdd(false);
    setEp(ep);
  };
  return (
    <div className="body">
      <Container fluid className="Vid-manage-container">
        <div className="vid-manage-title">
          <h1>{film.title}</h1>
        </div>
        <div className="vid-manage-img">
          <Image
            src={`/api/images/${film.filmPath}`}
            thumbnail
            alt="Movie picture"
          />
        </div>
        <div className="vid-manage-control">
          <div className="film-type">
            <strong>Type:</strong> {film.type}
          </div>
          <div className="ep-count">
            <strong>Ep Count:</strong> {filmEpisodes.length} Episodes
          </div>
          {!isAdd ? (
            <EpisodeForm filmId={film.id} passEp={episode} />
          ) : (
            <EpisodeForm filmId={film.id} />
          )}
        </div>
        <div className="vid-manage-episode">
          <hr />
          <Card>
            <div className="eps-list-top">
              <Card.Title>Episodes</Card.Title>
              <Button onClick={handeAdd}>Add</Button>
            </div>
            <div className="ep-list">
              {filmEpisodes
                .sort((a, b) => a.episodeNumber - b.episodeNumber)
                .map((ep) => (
                  <button key={ep.id} onClick={() => handleEdit(ep)}>
                    Ep {ep.episodeNumber}
                  </button>
                ))}
            </div>
          </Card>
        </div>
      </Container>
    </div>
  );
}

export default VideoManagement;
