import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Body from "./Layout/Body";
import NotFoundPage from "./Pages/error/NotFoundPage";
import CategoryManagement from "./Pages/Management/CategoryManagement";
import FilmManagement from "./Pages/Management/FilmManagement";
import VideoManagement from "./Pages/Management/VideoManagement";
import WebApi from "../WebApi";
import "./AdminIndex.css";
import AdminState from "../Admin/AminContext/State";
import WebState from "../WebContext/State";
const router = createBrowserRouter([
  {
    path: "/",
    element: <Body />,
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
