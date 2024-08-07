import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Home from "../src/Admin/Pages/Home";
import NotFoundPage from "../src/Admin/Pages/error/NotFoundPage";
import CategoryManagement from "../src/Admin/Pages/Management/CategoryManagement.jsx";
import FilmManagement from "../src/Admin/Pages/Management/FilmManagement.jsx";
import VideoManagement from "../src/Admin/Pages/Management/VideoManagement";
import serverApi from "../src/Admin/api/serverApi.jsx";
import "./index.css";
import AdminState from "../src/Admin/Context/AdminContext/State";

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
      return serverApi.getFilm(params.filmId);
    },
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <AdminState>
      <RouterProvider router={router} />
    </AdminState>
  </React.StrictMode>
);
