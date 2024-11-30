import "bootstrap/dist/css/bootstrap.min.css";
import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";
import CategoryManagement from "../Area/Admin/Pages/Management/CategoryManagement";
import FilmManagement from "../Area/Admin/Pages/Management/FilmManagement";
import VideoManagement from "../Area/Admin/Pages/Management/VideoManagement";
import AccountManagement from "../Area/Admin/Pages/Management/AccountManagement";
import WebApi from "../WebApi";
import AdminState from "../Area/Admin/AminContext/State";
import AuthState from "../Area/AuthContext/State";
import WebState from "../WebContext/State";
import Login from "../Area/User/Page/Login";
import Register from "../Area/User/Page/Register";
import Home from "../Area/User/Page/Home";
import ProtectedRoutes from "./ProtectedRoutes";
import NotFoundPage from "../Area/User/Page/error/NotFoundPage";
import Body from "../Layout/Body";
import UserInfo from "../Area/User/Page/UserInfo";
import EmailConfirm from "../Area/User/Page/EmailConfirm";
import Detail from "../Area/User/Page/Detail";
import Memberships from "../Area/User/Page/Subscription";
import SubscriptionSuccess from "../Area/User/Page/Subscription/SubscriptionSuccess";
import SubscriptionFailure from "../Area/User/Page/Subscription/SubscriptionFailure";
import WatchFilm from "../Area/User/Page/WatchFilm";
import SearchFilms from "../Area/User/Page/SearchFilms";
import Playlist from "../Area/User/Page/Playlist";
import Dashboard from "../Area/Admin/Pages/DashBoard";

const ROLES = {
  User: "UserT0",
  Admin: "Admin",
};
const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Body />}>
      <Route
        path="/user"
        element={<ProtectedRoutes allowedRoles={[ROLES.User]} />}
      >
        <Route index element={<Home />} />
        <Route path="/user/userinfo" element={<UserInfo />} />
        <Route path="/user/search" element={<SearchFilms />} />
        <Route path="/user/memberships" element={<Memberships />} />
        <Route
          path="/user/memberships/success"
          element={<SubscriptionSuccess />}
        />
        <Route
          path="/user/memberships/failure"
          element={<SubscriptionFailure />}
        />
        <Route
          path="/user/detail/:filmId"
          element={<Detail />}
          loader={async ({ params }) => {
            const [film, relatedFilms, filmCates, filmScore] =
              await Promise.all([
                WebApi.getFilm(params.filmId),
                WebApi.getRelatedFilms(params.filmId),
                WebApi.getFilmCates(params.filmId),
                WebApi.getFilmsScore(params.filmId),
              ]);
            return { film, relatedFilms, filmCates, filmScore };
          }}
        />
        <Route
          path="/user/watchfilm/:filmId"
          element={<WatchFilm />}
          loader={async ({ params }) => {
            const [episodes, film] = await Promise.all([
              WebApi.getEpisodes(params.filmId),
              WebApi.getFilm(params.filmId),
            ]);
            return { episodes, film };
          }}
        />
      </Route>
      <Route
        path="/admin"
        element={<ProtectedRoutes allowRoles={[ROLES.Admin]} />}
      >
        <Route index element={<Dashboard />} />
        <Route
          path="/admin/category-Management"
          element={<CategoryManagement />}
        />
        <Route path="/admin/film-Management" element={<FilmManagement />} />
        <Route
          path="/admin/account-Management"
          element={<AccountManagement />}
        />
        <Route
          path="/admin/video-Management/:filmId"
          element={<VideoManagement />}
          loader={async ({ params }) => WebApi.getFilm(params.filmId)}
        />
      </Route>
      <Route path="/" element={<Login />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/emailconfirm/:token/:email" element={<EmailConfirm />} />
      <Route
        path="/playlist/:username"
        element={<Playlist />}
        loader={async ({ params }) => WebApi.getUserPlayList(params.username)}
      />
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
