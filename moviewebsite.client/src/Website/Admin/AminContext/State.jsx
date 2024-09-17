/* eslint-disable react/prop-types */
import React, { useReducer, useContext } from "react";
import api from "../AdminApi/adminApi";
import AdminContext from "./Context";
import WebContext from "../../WebContext/Context";
import AdminReducer from "./Reducer";
const AdminState = (props) => {
  const webContext = useContext(WebContext);
  const { getCategories, getFilms} = webContext;
  const initialState = {
    isCateUpdated: false,
  };
  const typeList = ["category", "film", "episode"];
  const [state, dispatch] = useReducer(AdminReducer, initialState);
  const Delete = async (type, id) => {
    if (typeList.includes(type) && id > 0) {
      await api.Delete(type, id);
      switch (type) {
        case "category":
          await getCategories();
          break;
        case "film":
          await getFilms();
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
        isCateUpdated: state.isCateUpdated,
        Delete,
        onFilmCateUpdate,
      }}
    >
      {props.children}
    </AdminContext.Provider>
  );
};

export default AdminState;
