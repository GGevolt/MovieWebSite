import { useReducer, useEffect } from "react";
import AuthContext from "./Context";
import AuthReducer from "./Reducer";
import AuthApi from "../AuthApi";
import PropTypes from "prop-types";
import { loadStripe } from "@stripe/stripe-js";

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
  const createCheckoutSession = async (selectedPlan) => {
    const data = await AuthApi.createCheckOutSession(selectedPlan);
    if (data) {
      const stripe = await loadStripe(data.publicKey);
      const result = stripe.redirectToCheckout({
        sessionId: data.sessionId,
      });

      if (result.error) {
        console.log("Rediect to Check out fail: ", result.error);
      }
    }
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
        emailConfirm,
        createCheckoutSession,
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
