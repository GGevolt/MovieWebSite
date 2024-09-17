import {
  FILMCATE_UPDATE,
} from "./type";

export default (state, action) => {
  switch (action.type) {
    case FILMCATE_UPDATE:
      return {
        ...state,
        isCateUpdated: action.payload,
      };
    default:
      return state;
  }
};
