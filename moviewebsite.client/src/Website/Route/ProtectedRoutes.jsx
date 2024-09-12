import React, {  useEffect, useContext } from "react";
import { Navigate } from "react-router-dom";
import UserBody from "../User/Layout/UserBody";
import AuthContext from "../User/AuthContext/Context";

function ProtectedRoutes() {
  const authContext = useContext(AuthContext);
  const { validateUser, isLoggedIn } = authContext;
  useEffect(() => {
    hanldeProcess();
  }, []);
  const hanldeProcess = async ()=>{
    if (!isLoggedIn) {
      <Navigate to="/login" />;
    }
    await validateUser();
  }
  return <UserBody />;
}
export default ProtectedRoutes;
