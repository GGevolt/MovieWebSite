import { useReducer } from "react";
import api from "../../api/serverApi";
import AdminContext from "./Context";
import AdminReducer from "./Reducer";
const AdminState = (props) => {
  const initialState = {
    films: [],
    categories: [],
  };
  const [state, dispatch] = useReducer(AdminReducer, initialState);
  const getFilms = async () => {
    const response = await api.getFilms();
    dispatch({
      type: "GET_FILMS",
      payload: response.data.items,
    });
  };
  const getCategories = async () => {
    const response = await api.getCategories();
    dispatch({
      type: "GET_CATEGORIES",
      payload: response.data.items,
    });
  };

  const getvid = () => {};
  return (
    <AdminContext.Provider
      value={{
        films: state.films,
        categories: state.categories,
        getFilms,
        getCategories,
      }}
    >
      {props.children}
    </AdminContext.Provider>
  );
};

export default AdminState;
