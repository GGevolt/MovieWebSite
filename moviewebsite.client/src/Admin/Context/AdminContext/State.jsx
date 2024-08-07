/* eslint-disable react/prop-types */
import React, { useReducer } from "react";
import api from "../../api/serverApi";
import AdminContext from "./Context";
import AdminReducer from "./Reducer";
const AdminState = (props) => {
  const initialState = {
    film: {},
    films: [],
    categories: [],
    episodes: [],
    isCateUpdated: false,
  };
  const typeList = ["category", "film", "episode"];
  const [state, dispatch] = useReducer(AdminReducer, initialState);
  const getFilm = async (id) => {
    const response = await api.getFilm(id);
    dispatch({
      type: "GET_FILM",
      payload: response,
    });
  };
  const getFilms = async () => {
    const response = await api.getFilms();
    dispatch({
      type: "GET_FILMS",
      payload: response,
    });
  };
  const getFilmEps = async (id) => {
    const response = await api.getEpisode(id);
    dispatch({
      type: "GET_FILM_EPS",
      payload: response,
    });
  };
  const getCategories = async () => {
    const response = await api.getCategories();
    dispatch({
      type: "GET_CATEGORIES",
      payload: response,
    });
  };
  const Delete = async (type, id) => {
    if (typeList.includes(type) && id > 0) {
      await api.Delete(type, id);
      switch (type) {
        case "category":
          getCategories();
          break;
        case "film":
          getFilms();
          break;
        default:
          break;
      }
    }
  };
  const onFilmCateUpdate = (updateStatus) => {
    dispatch({
      type: "FILMCATE_UPDATE",
      payload: updateStatus,
    });
  };
  return (
    // eslint-disable-next-line react/react-in-jsx-scope
    <AdminContext.Provider
      value={{
        films: state.films,
        episodes: state.episodes,
        categories: state.categories,
        isCateUpdated: state.isCateUpdated,
        getFilm,
        getFilms,
        getFilmEps,
        getCategories,
        Delete,
        onFilmCateUpdate,
      }}
    >
      {props.children}
    </AdminContext.Provider>
  );
};

export default AdminState;
