import React, { useReducer, useEffect } from "react";
import AuthContext from "./Context";
import AuthReducer from "./Reducer";
import AuthApi from "../AuthApi";
import PropTypes from "prop-types";

const AuthState = (props) => {
  const initialState = {
    isLoggedIn: {},
    roles: [],
    userName: {},
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

  const setUserName = (status) => {
    dispatch({ type: "SET_USERNAME", payload: status });
  };

  const signIn = async (formData) => {
    const data = await AuthApi.signIn(formData);
    if (data) {
      // localStorage.setItem("token", data.token);
      setRoles(data.userRoles);
      setUserName(data.userName);
      setIsLoggedIn(true);
      return true;
    }
    return false;
  };
  const register = async (formData) => {
    const res = await AuthApi.register(formData);
    return res;
  };
  const signOut = async () => {
    if (await AuthApi.signOut()) {
      // localStorage.clear();
      setIsLoggedIn(false);
      setRoles([]);
      setUserName({});
      localStorage.removeItem("authState");
    }
  };
  const validateUser = async () => {
    const res = await AuthApi.validateUser();
    if (!res) {
      setIsLoggedIn(false);
      setRoles([]);
      setUserName({});
      localStorage.removeItem("authState");
    }
  };
  const emailConfirm = async (formData) => {
    const res = await AuthApi.confirmEmail(formData);
    return res;
  };
  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: state.isLoggedIn,
        roles: state.roles,
        userName: state.userName,
        signIn,
        register,
        signOut,
        validateUser,
        emailConfirm
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
