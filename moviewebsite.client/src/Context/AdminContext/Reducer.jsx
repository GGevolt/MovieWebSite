import { GET_FILM, GET_FILMS, GET_CATEGORIES, FILMCATE_UPDATE } from "../type";

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
    case GET_CATEGORIES:
      return {
        ...state,
        categories: action.payload,
      };
    case FILMCATE_UPDATE:
      return {
        ...state,
        isCateUpdated: action.payload,
      };
    default:
      return state;
  }
};
