import { GET_FILMS } from "../type";

export default (state, action) => {
  switch (action.type) {
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
    default:
      return state;
  }
};
