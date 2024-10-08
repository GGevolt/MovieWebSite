import { ISLOGIN, SET_ROLES, SET_USERNAME, IS_USER_UPDATED } from "./type";

// eslint-disable-next-line react-refresh/only-export-components
export default (state, action) => {
  switch (action.type) {
    case ISLOGIN:
      return {
        ...state,
        isLoggedIn: action.payload,
      };
      case IS_USER_UPDATED:
      return {
        ...state,
        isUserUpdated: action.payload,
      };
    case SET_ROLES:
      return {
        ...state,
        roles: action.payload,
      };
    case SET_USERNAME:
      return {
        ...state,
        userName: action.payload,
      };
    default:
      return state;
  }
};
