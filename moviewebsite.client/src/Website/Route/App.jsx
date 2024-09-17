import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import CategoryManagement from "../Admin/Pages/Management/CategoryManagement";
import FilmManagement from "../Admin/Pages/Management/FilmManagement";
import VideoManagement from "../Admin/Pages/Management/VideoManagement";
import WebApi from "../WebApi";
import "./index.css";
import AdminState from "../Admin/AminContext/State";
import AuthState from "../User/AuthContext/State";
import WebState from "../WebContext/State";
import Login from "../User/Page/Login";
import Register from "../User/Page/Register";
import Home from "../User/Page/Home";
import ProtectedRoutes from "./ProtectedRoutes";
import NotFoundPage from "../User/Page/error/NotFoundPage";
import UnAuthorize from "../User/Page/error/UnAuthorize";
import UserBody from "../Layout/UserBody";
import UserInfo from "../User/Page/UserInfo";

const ROLES = {
  User: "UserT0",
  SubUser: "UserT1",
  Admin: "Admin",
};
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<UserBody />}>
      <Route element={<ProtectedRoutes allowedRoles={[ROLES.User]} />}>
        <Route path="/userinfo" element={<UserInfo />} />
      </Route>
      <Route element={<ProtectedRoutes allowRoles={[ROLES.Admin]} />}>
        {/* <Route path="/" element={<FilmManagement />} /> */}
        <Route
          path="/admin/category-Management"
          element={<CategoryManagement />}
        />
        <Route path="/admin/film-Management" element={<FilmManagement />} />
        <Route
          path="/admin/video-Management/:filmId"
          element={<VideoManagement />}
          loader={async ({ params }) => WebApi.getFilm(params.filmId)}
        />
      </Route>
      <Route path="/" element={<Home />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/unauthorize" element={<UnAuthorize />} />
      <Route path="*" element={<NotFoundPage />} />
    </Route>
  )
);
function AdminApp() {
  return (
    <AuthState>
      <WebState>
        <AdminState>
          <RouterProvider router={router} />
        </AdminState>
      </WebState>
    </AuthState>
  );
}

export default AdminApp;
