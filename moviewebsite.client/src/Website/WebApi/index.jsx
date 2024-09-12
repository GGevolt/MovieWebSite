import axios from "axios";

const createGetRequest = (url, errorMessage='') => async () => {
  try {
    const response = await axios.get(url);
    return response.data;
  } catch (error) {
    console.error(`${errorMessage}: ${error.message}`);
    throw error;
  }
};

const WebApi = {
  getEpisodes: (filmId) => createGetRequest(`/api/episode/${filmId}, , 'Error fetching episodes'`),
  getCategories: createGetRequest("/api/category", 'Error fetching categories'),
  getFilm: (id) => createGetRequest(`/api/film/${id}`, 'Error fetching film '),
  getFilms: createGetRequest("/api/film", 'Error fetching films'),
  getFilmCates: (id) => createGetRequest(`/api/filmCate/${id}`, 'Error fetching film categories ')
};
export default WebApi;
