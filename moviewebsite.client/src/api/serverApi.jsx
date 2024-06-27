import axios from "axios";

const getEpisode = async (filmId) => {
  const response = await axios.get(`/episode/${filmId}`).catch((error) => {
    console.error(
      "There has been a problem with episode get operation:",
      error
    );
  });
  return response.data;
};

const getCategories = async () => {
  const response = await axios.get("/category").catch((error) => {
    console.error(
      "There has been a problem with categories get operation:",
      error
    );
  });
  return response.data;
};

const getFilm = async (id) => {
  const response = await axios.get(`/film/${id}`).catch((error) => {
    console.error("There has been a problem with film get operation:", error);
  });
  return response.data;
};

const getFilms = async () => {
  const response = await axios.get(`/film`).catch((error) => {
    console.error("There has been a problem with films get operation:", error);
  });
  return response.data;
};

const getFilmCates = async (id) => {
  const response = await axios.get(`/filmCate/${id}`).catch((error) => {
    console.error(
      "There has been a problem with film categories get operation:",
      error
    );
  });
  return response.data;
};

const Delete = async (type, id) => {
  await axios.delete(`/${type}/${id}`).catch((error) => {
    console.error(
      `There has been a problem with ${type} delete operation:'`,
      error
    );
  });
};

const serverApi = {
  getEpisode,
  getCategories,
  getFilm,
  getFilmCates,
  getFilms,
  Delete,
};
export default serverApi;
