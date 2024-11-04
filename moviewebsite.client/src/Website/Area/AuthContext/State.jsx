import { useReducer, useEffect } from "react";
import AuthContext from "./Context";
import AuthReducer from "./Reducer";
import AuthApi from "../AuthApi";
import PropTypes from "prop-types";
import { loadStripe } from "@stripe/stripe-js";

const AuthState = (props) => {
  const initialState = {
    isLoggedIn: {},
    isUserUpdated: {},
    roles: [],
    userName: {},
  };
  const storedState = JSON.parse(localStorage.getItem("authState"));
  const [state, dispatch] = useReducer(
    AuthReducer,
    storedState || initialState
  );

  useEffect(() => {
    localStorage.setItem("authState", JSON.stringify(state));
  }, [state]);

  const setRoles = (values) => {
    dispatch({ type: "SET_ROLES", payload: values });
  };

  const setIsLoggedIn = (status) => {
    dispatch({ type: "ISLOGIN", payload: status });
  };

  const setIsUserUpdated = (status) => {
    dispatch({ type: "IS_USER_UPDATED", payload: status });
  };

  const setUserName = (values) => {
    dispatch({ type: "SET_USERNAME", payload: values });
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
    if (res.isValid){
      if (res.roles.length > 0) {
        setRoles(res.roles);
      }
    }else {
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
  const createCheckoutSession = async (selectedPlan) => {
    const data = await AuthApi.createCheckOutSession(selectedPlan);
    if (data) {
      setIsUserUpdated(true);
      const stripe = await loadStripe(data.publicKey);
      const result = stripe.redirectToCheckout({
        sessionId: data.sessionId,
      });

      if (result.error) {
        console.log("Rediect to Check out fail: ", result.error);
      }
    }
  };
  const redirectToCustomerPortal = async () => {
    setIsUserUpdated(true);
    await AuthApi.RedirectToCustomerPortal();
  };
  const getUserStatus = async () => {
    const res = await AuthApi.getUserStatus();
    if (res) {
      setRoles(res);
    }
    setIsUserUpdated(false);
  };
  const getUserPlayList = async () =>{
    const res = await AuthApi.getUserPlayList();
    return res;
  }
  return (
    <AuthContext.Provider
      value={{
        isLoggedIn: state.isLoggedIn,
        roles: state.roles,
        userName: state.userName,
        isUserUpdated: state.isUserUpdated,
        signIn,
        register,
        signOut,
        getUserStatus,
        validateUser,
        emailConfirm,
        getUserPlayList,
        createCheckoutSession,
        redirectToCustomerPortal,
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
