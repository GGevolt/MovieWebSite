import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Home from "./Pages/Home";
import NotFoundPage from "./Pages/error/NotFoundPage";
import CategoryManagement from "./Pages/Management/CategoryManagement.jsx";
import FilmManagement from "./Pages/Management/FilmManagement.jsx";
import VideoManagement from "./Pages/Management/VideoManagement";
import { getFilm } from "./api/serverApi.jsx";
import "./index.css";
import AdminState from "./Context/AdminContext/State";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/Admin/Category-Management",
    element: <CategoryManagement />,
  },
  {
    path: "/Admin/Film-Management",
    element: <FilmManagement />,
  },
  {
    path: "/Admin/Video-Management/:filmId",
    element: <VideoManagement />,
    loader: async ({ params }) => {
      return getFilm(params.filmId);
    },
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <AdminState>
    <RouterProvider router={router} />
  </AdminState>
);
