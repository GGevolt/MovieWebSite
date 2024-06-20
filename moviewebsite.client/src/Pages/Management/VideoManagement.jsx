import React, { useEffect, useState } from "react";
import { Button, Card, Container, Image } from "react-bootstrap";
import { useLoaderData } from "react-router-dom";
import EpisodeForm from "../../Components/Form/EpisodeForm";
import NavBar from "../../Components/NavBar";
import { getEpisode } from "../../api/serverApi.jsx";
import "./Management.css";

function VideoManagement() {
  const [episodes, setEpisodes] = useState([]);
  const film = useLoaderData();
  useEffect(() => {
    if (film) {
      loadData();
    }
  }, [film]);
  const [isAdd, setIsAdd] = useState(true);
  const [episode, setEp] = useState();
  const loadData = async () => {
    const epdata = await getEpisode(film.id);
    setEpisodes(epdata);
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
      <NavBar />
      <Container fluid className="Vid-manage-container">
        <div className="vid-manage-title">
          <h1>{film.title}</h1>
        </div>
        <div className="vid-manage-img">
          <Image
            src={`/images/${film.filmImg}`}
            thumbnail
            alt="Movie picture"
          />
        </div>
        <div className="vid-manage-control">
          <div className="film-type">
            <strong>Type:</strong> {film.type}
          </div>
          <div className="ep-count">
            <strong>Ep Count:</strong> {episodes.length} Episodes
          </div>
          <EpisodeForm
            filmId={film.id}
            onSuccess={getEpisode}
            episodes={episodes}
            {...(!isAdd && { episode: episode })}
          />
        </div>
        <div className="vid-manage-episode">
          <hr />
          <Card>
            <div className="eps-list-top">
              <Card.Title>Episodes</Card.Title>
              <Button onClick={handeAdd}>Add</Button>
            </div>
            <div className="ep-list">
              {episodes
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
