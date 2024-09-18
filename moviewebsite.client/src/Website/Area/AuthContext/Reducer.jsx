import { ISLOGIN, SET_ROLES, SET_USERNAME } from "./type";

export default (state, action) => {
  switch (action.type) {
    case ISLOGIN:
      return {
        ...state,
        isLoggedIn: action.payload,
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
