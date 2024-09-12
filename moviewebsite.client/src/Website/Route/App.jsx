import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
// import AdminBody from "../Admin/Layout/AdminBody";
// import CategoryManagement from "../Admin/Pages/Management/CategoryManagement";
// import FilmManagement from "../Admin/Pages/Management/FilmManagement";
// import VideoManagement from "../Admin/Pages/Management/VideoManagement";
// import WebApi from "../WebApi";
import "./index.css";
import AdminState from "../Admin/AminContext/State";
import AuthState from "../User/AuthContext/State";
import WebState from "../WebContext/State";
import Login from "../User/Page/Login";
import Register from "../User/Page/Register";
import Home from "../User/Page/Home";
import ProtectedRoutes from "./ProtectedRoutes";
import NotFoundPage from "../User/Page/error/NotFoundPage";
// const router = createBrowserRouter(
//   // [
//   // {
//   //   path: "/",
//   //   element: <Home/>,
//   // },
//   // {
//   //   path: "/Login",
//   //   element: <Login />,
//   // },
//   // {
//   //   path: "/Register",
//   //   element: <Register />,
//   // },
//   // {
//   //   path: "/Admin",
//   //   element: <AdminBody />,
//   //   children: [
//   //     { index: true, element: <FilmManagement /> },
//   //     { path: "/Admin/Category-Management", element: <CategoryManagement /> },
//   //     { path: "/Admin/Film-Management", element: <FilmManagement /> },
//   //     {
//   //       path: "/Admin/Video-Management/:filmId",
//   //       element: <VideoManagement />,
//   //       loader: async ({ params }) => WebApi.getFilm(params.filmId),
//   //     },
//   //     { path: "*", element: <NotFoundPage /> },
//   //   ],
//   // },
// // ]
// );

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route element={<ProtectedRoutes />}>
        <Route path="/" element={<Home />} />
      </Route>
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
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
