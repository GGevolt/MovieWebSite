import axios from "axios";

const getEpisodes = async (filmId) => {
  const response = await axios.get(`/api/episode/${filmId}`).catch((error) => {
    console.error(
      "There has been a problem with episode get operation:",
      error
    );
  });
  return response.data;
};

const getCategories = async () => {
  const response = await axios.get("/api/category").catch((error) => {
    console.error(
      "There has been a problem with categories get operation:",
      error
    );
  });
  return response.data;
};

const getFilm = async (id) => {
  const response = await axios.get(`/api/film/${id}`).catch((error) => {
    console.error("There has been a problem with film get operation:", error);
  });
  return response.data;
};

const getFilms = async () => {
  const response = await axios.get(`/api/film`).catch((error) => {
    console.error("There has been a problem with films get operation:", error);
  });
  return response.data;
};

const getFilmCates = async (id) => {
  const response = await axios.get(`/api/filmCate/${id}`).catch((error) => {
    console.error(
      "There has been a problem with film categories get operation:",
      error
    );
  });
  return response.data;
};

const getRelatedFilms = async (id) => {
  const response = await axios
    .get(`/api/film/relatefilms/${id}`)
    .catch((error) => {
      console.error(
        "There has been a problem with get operation of get related films:",
        error
      );
    });
  return response.data;
};
const WebApi = {
  getEpisodes,
  getCategories,
  getFilm,
  getFilmCates,
  getFilms,
  getRelatedFilms,
};
export default WebApi;
