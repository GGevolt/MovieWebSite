import axios from "axios";

const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
});

export const Delete = async (type, id) => {
  await api
    .delete(`/${type}/${id}`)
    .then(() => {
      console.log("Delete success!");
    })
    .catch((error) => {
      console.log(
        `There has been a problem with ${type} delete operation: `,
        error.response.data
      );
    });
};

const getSubscriptionStatusData = async () => {
  const response = await api
    .get("/dashBoard/subscription-status")
    .catch((error) => {
      console.log("Getting Subscription Status Data error:", error.response);
    });
  return response.data;
};

const getRevenue = async () => {
  const response = await api.get("/dashBoard/revenue").catch((error) => {
    console.log("Getting revenue error:", error.response);
  });
  return response.data;
};

const getContentPopularity = async () => {
  const response = await api
    .get("/dashBoard/content-popularity")
    .catch((error) => {
      console.log("Getting Content Popularity error:", error.response);
    });
  return response.data;
};

const getUserDemographics = async () => {
  const response = await api
    .get("/dashBoard/user-demographics")
    .catch((error) => {
      console.log("Getting User Demographics error:", error.response);
    });
  return response.data;
};

const getGenrePopularity = async () => {
  const response = await api
    .get("/dashBoard/genre-popularity")
    .catch((error) => {
      console.log("Getting Genre Popularity error:", error.response);
    });
  return response.data;
};

const getUsers = async () => {
  const response = await api.get("/account").catch((error) => {
    console.log("Getting users error:", error.response);
  });
  return response.data;
};

const cancelSubscription = async (userId) => {
  await api.get(`/payment/cancel/${userId}`).catch((error) => {
    console.log("Cancel subscription error:", error.response);
  });
}

const adminApi = {
  Delete,
  getContentPopularity,
  getUserDemographics,
  getGenrePopularity,
  getSubscriptionStatusData,
  getRevenue,
  getUsers,
  cancelSubscription
};
export default adminApi;
