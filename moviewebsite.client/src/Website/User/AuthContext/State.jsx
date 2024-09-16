import React, { useReducer, useEffect } from "react";
import AuthContext from "./Context";
import AuthReducer from "./Reducer";
import AuthApi from "../AuthApi";
import PropTypes from "prop-types";

const AuthState = (props) => {
  const initialState = {
    isLoggedIn: {},
    roles: [],
  };

  const storedState = JSON.parse(localStorage.getItem("authState"));
  const [state, dispatch] = useReducer(
    AuthReducer,
    storedState || initialState
  );

  useEffect(() => {
    // Save state to localStorage whenever it changes
    localStorage.setItem("authState", JSON.stringify(state));
  }, [state]);

  const setRoles = (values) => {
    dispatch({ type: "SET_ROLES", payload: values });
  };

  const setIsLoggedIn = (status) => {
    dispatch({ type: "ISLOGIN", payload: status });
  };

  const signIn = async (formData) => {
    const data = await AuthApi.signIn(formData);
    if (data) {
      // localStorage.setItem("token", data.token);
      setRoles(data.userRoles);
      setIsLoggedIn(true);
      // document.location = "/";
    }
  };
  const register = async (formData) => {
    const res = await AuthApi.register(formData);
    if (res) {
      document.location = "/login";
    }
  };
  const signOut = async () => {
    if (await AuthApi.signOut()) {
      // localStorage.clear();
      setIsLoggedIn(false);
      setRoles([]);
      localStorage.removeItem("authState");
    }
  };
  const validateUser = async () => {
    const res = await AuthApi.validateUser();
    if (!res) {
      setIsLoggedIn(false);
      setRoles([]);
      localStorage.removeItem("authState");
    }
  };
  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: state.isLoggedIn,
        roles: state.roles,
        signIn,
        register,
        signOut,
        validateUser,
      }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

AuthState.propTypes = {
  children: PropTypes.node.isRequired,
};

export default AuthState;
