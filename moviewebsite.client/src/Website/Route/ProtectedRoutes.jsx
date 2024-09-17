import React, { useEffect, useContext, useState } from "react";
import { Navigate, Outlet } from "react-router-dom";
import AuthContext from "../User/AuthContext/Context";
import PropTypes from "prop-types";

function ProtectedRoutes({ allowedRoles }) {
  const authContext = useContext(AuthContext);
  const { validateUser, isLoggedIn, roles } = authContext;
  useEffect(() => {
    hanldeProcess();
  }, []);
  const hanldeProcess = async () => {
    await validateUser();
  };
  if (
    !isLoggedIn ||
    (allowedRoles && !roles.some((role) => allowedRoles.includes(role)))
  ) {
    return <Navigate to="/login" replace />;
  }
  return <Outlet />;
}

ProtectedRoutes.propTypes = {
  allowedRoles: PropTypes.array,
};
export default ProtectedRoutes;
