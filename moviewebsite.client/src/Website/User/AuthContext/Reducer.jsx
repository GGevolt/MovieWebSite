import { ISLOGIN, SET_ROLES } from "./type";

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
    default:
      return state;
  }
};
