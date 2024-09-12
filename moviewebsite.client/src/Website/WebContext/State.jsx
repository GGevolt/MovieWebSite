/* eslint-disable react/prop-types */
import React, { useReducer } from "react";
import webApi from "../WebApi";
import WebContext from "./Context";
import WebReducer from "./Reducer";
const WebState = (props) => {
  const initialState = {
    film: {},
    films: [],
    categories: [],
    filmEpisodes: []
  };
  const [state, dispatch] = useReducer(WebReducer, initialState);
  const getFilm = async (id) => {
    const response = await webApi.getFilm(id);
    dispatch({
      type: "GET_FILM",
      payload: response,
    });
  };
  const getFilms = async () => {
    const response = await webApi.getFilms();
    dispatch({
      type: "GET_FILMS",
      payload: response,
    });
  };
  const getFilmEps = async (id) => {
    const response = await webApi.getEpisodes(id);
    dispatch({
      type: "GET_FILM_EPS",
      payload: response,
    });
  };
  const getCategories = async () => {
    const response = await webApi.getCategories();
    dispatch({
      type: "GET_CATEGORIES",
      payload: response,
    });
  };
  return (
    // eslint-disable-next-line react/react-in-jsx-scope
    <WebContext.Provider
      value={{
        films: state.films,
        filmEpisodes: state.filmEpisodes,
        categories: state.categories,
        getFilm,
        getFilms,
        getFilmEps,
        getCategories,
      }}
    >
      {props.children}
    </WebContext.Provider>
  );
};

export default WebState;
