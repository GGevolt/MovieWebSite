import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import AdminBody from "../Admin/Layout/AdminBody";
import NotFoundPage from "../Admin/Pages/error/NotFoundPage";
import CategoryManagement from "../Admin/Pages/Management/CategoryManagement";
import FilmManagement from "../Admin/Pages/Management/FilmManagement";
import VideoManagement from "../Admin/Pages/Management/VideoManagement";
import WebApi from "../WebApi";
import "./index.css";
import AdminState from "../Admin/AminContext/State";
import WebState from "../WebContext/State";
import Login from "../User/Page/Login";
import Register from "../User/Page/Register";
const router = createBrowserRouter([
  {
    path: "/Login",
    element: <Login />,
  },
  {
    path: "/Register",
    element: <Register />,
  },
  {
    path: "/",
    element: <AdminBody />,
    children: [
      { index: true, element: <FilmManagement /> },
      { path: "/Admin/Category-Management", element: <CategoryManagement /> },
      { path: "/Admin/Film-Management", element: <FilmManagement /> },
      {
        path: "/Admin/Video-Management/:filmId",
        element: <VideoManagement />,
        loader: async ({ params }) => WebApi.getFilm(params.filmId),
      },
      { path: "*", element: <NotFoundPage /> },
    ],
  },
]);
function AdminApp() {
  return (
    <WebState>
      <AdminState>
        <RouterProvider router={router} />
      </AdminState>
    </WebState>
  );
}

export default AdminApp;
