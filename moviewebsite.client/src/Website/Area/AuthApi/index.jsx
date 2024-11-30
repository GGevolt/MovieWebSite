import axios from "axios";

const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
});
const signIn = async (formData) => {
  let res = null;
  await api
    .post("/authentication/login", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
        Accept: "application/json",
      },
    })
    .then((response) => {
      console.log("Login successful");
      res = response.data;
    })
    .catch((error) => {
      console.log("Login Fail:", error.response.data);
      alert(error.response.data.error || "An unknown error occurred");
    });
  return res;
};
const register = async (formData) => {
  let res = false;
  await api
    .post("/authentication/register", formData, {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
    .then(() => {
      console.log("Register successful! ");
      res = true;
    })
    .catch((error) => {
      console.log("Register fail: ", error.response.data.message || error);
      if (
        error.response &&
        error.response.data.errors &&
        Array.isArray(error.response.data.errors)
      ) {
        const errorMessages = error.response.data.errors.map(
          (err) => err.description
        );
        alert(errorMessages.join("\n"));
      } else {
        console.error("Unexpected error structure:", error);
      }
      res = false;
    });
  return res;
};
const signOut = async () => {
  let res = false;
  await api
    .get("/authentication/logout")
    .then((response) => {
      console.log(response.data.message);
      res = true;
    })
    .catch((error) => {
      console.log("Can't log out: ", error);
      res = false;
    });
  return res;
};
const getUserInfo = async () => {
  let res = null;
  await api
    .get("/authentication/info")
    .then((response) => {
      console.log(response.data.userInfo);
      res = response.data.userInfo;
    })
    .catch((error) => {
      console.log("Get user: ", error.response.data.error);
    });
  return res;
};
const validateUser = async () => {
  let res = { isValid: false, roles: [] };
  await axios
    .get("/api/authentication/validate")
    .then((response) => {
      res.isValid = true;
      if (response.data.roles) {
        res.roles = response.data.roles;
      }
    })
    .catch((error) => {
      console.log("Validation error:", error.response?.data || error.message);
      res.isValid = false;
    });
  return res;
};
const confirmEmail = async (formData) => {
  let res = false;
  await axios
    .post("/api/authentication/confirmEmail", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
        Accept: "application/json",
      },
    })
    .then(() => {
      res = true;
    })
    .catch((error) => {
      console.log(error.response.data);
      res = false;
    });
  return res;
};
const createCheckOutSession = async (selectedPlan) => {
  let res = null;
  const successUrl = `${window.location.origin}/user/memberships/success`;
  const failureUrl = `${window.location.origin}/user/memberships/failure`;
  const formData = {
    plan: selectedPlan,
    successUrl,
    failureUrl,
  };
  await axios
    .post("/api/payment/create_checkout_session", formData)
    .then((response) => {
      res = response.data;
    })
    .catch((error) => {
      console.log(
        "Fail to create check out session: ",
        error.response.data.ErrorMessage || error
      );
    });
  return res;
};
const RedirectToCustomerPortal = async () => {
  const returnUrl = `${window.location.origin}/user`;
  await api
    .post("/payment/customer_portal", { returnUrl })
    .then((response) => {
      if (response.data.url) {
        window.location.href = response.data.url;
      }
    })
    .catch((error) => {
      console.log(
        "Redirection to customer portal fail: ",
        error.response.data.error
      );
    });
};
const userFilmLogic = async ({
  filmId,
  isAddPlayList = false,
  isRemoveFromPlayList = false,
  filmRatting = -1,
  isViewed = false,
}) => {
  const response = await api
    .post("/userFilm", {
      filmId,
      isAddPlayList,
      isRemoveFromPlayList,
      filmRatting,
      isViewed,
    })
    .catch((error) => {
      console.log("add to play list error:", error.response);
    });
  return response.data;
};
const AuthApi = {
  register,
  signIn,
  signOut,
  getUserInfo,
  validateUser,
  confirmEmail,
  userFilmLogic,
  createCheckOutSession,
  RedirectToCustomerPortal,
};
export default AuthApi;
