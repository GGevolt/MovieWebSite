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
  let res = false;
  await axios
    .get("/api/authentication/validate")
    .then(() => {
      res = true;
    })
    .catch(() => {
      res = false;
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
const createCheckOutSession = async (selectedPlan)=>{
  let res = null;
  const priceKeyIds = {
    "pro": "price_1Q5XdAATmHlXrMYowulmblzP",
    "premium": "price_1Q5XemATmHlXrMYoZLybRoux"
  }
  const successUrl = `${window.location.origin}/user/memberships/success`;
  const failureUrl = `${window.location.origin}/user/memberships/failure`;
  const formData = { 
    priceId: priceKeyIds[selectedPlan], 
    successUrl, 
    failureUrl 
  };
  await axios.post("/api/payment/create_checkout_session", formData)
  .then((response) => {
    res= response.data;
  })
  .catch((error) => {
    console.log("Fail to create check out session: ", error.response.data.ErrorMessage || error);
  });
  return res;
}
const AuthApi = {
  register,
  signIn,
  signOut,
  getUserInfo,
  validateUser,
  confirmEmail,
  createCheckOutSession
};
export default AuthApi;