import {
  GET_FILM,
  GET_FILMS,
  GET_FILM_EPS,
  GET_CATEGORIES,
} from "./type";

export default (state, action) => {
  switch (action.type) {
    case GET_FILM:
      return {
        ...state,
        film: action.payload,
      };
    case GET_FILMS:
      return {
        ...state,
        films: action.payload,
      };
    case GET_FILM_EPS:
      return {
        ...state,
        filmEpisodes: action.payload,
      };
    case GET_CATEGORIES:
      return {
        ...state,
        categories: action.payload,
      };
    default:
      return state;
  }
};
