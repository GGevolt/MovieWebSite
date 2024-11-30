import React, { useReducer, useContext } from "react";
import api from "../AdminApi/adminApi";
import AdminContext from "./Context";
import WebContext from "../../../WebContext/Context";
import AdminReducer from "./Reducer";
import PropTypes from "prop-types";
import adminApi from "../AdminApi/adminApi";
const AdminState = (props) => {
  const webContext = useContext(WebContext);
  const { getCategories, getFilms } = webContext;
  const initialState = {
    isCateUpdated: false,
    userList: [],
  };
  const typeList = ["category", "film", "episode", "account"];
  const [state, dispatch] = useReducer(AdminReducer, initialState);
  const getUsers = async () => {
    const response = await adminApi.getUsers();
    dispatch({
      type: "GET_USERS",
      payload: response,
    });
  };
  const Delete = async (type, id) => {
    if (typeList.includes(type) && id !==null) {
      await api.Delete(type, id);
      switch (type) {
        case "category":
          await getCategories();
          break;
        case "film":
          await getFilms();
          break;
        case "account":
          await getUsers();
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
    <AdminContext.Provider
      value={{
        isCateUpdated: state.isCateUpdated,
        userList: state.userList,
        Delete,
        onFilmCateUpdate,
        getUsers
      }}
    >
      {props.children}
    </AdminContext.Provider>
  );
};

AdminState.propTypes = {
  children: PropTypes.node.isRequired,
};

export default AdminState;
