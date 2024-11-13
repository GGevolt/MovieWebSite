import {
  FILMCATE_UPDATE,
  GET_USERS
} from "./type";

// eslint-disable-next-line react-refresh/only-export-components
export default (state, action) => {
  switch (action.type) {
    case FILMCATE_UPDATE:
      return {
        ...state,
        isCateUpdated: action.payload,
      };
    case GET_USERS:
      return {
        ...state,
        userList: action.payload,
      };
    default:
      return state;
  }
};
