import axios from "axios";

const Delete = async (type, id) => {
  await axios.delete(`/api/${type}/${id}`).catch((error) => {
    console.error(
      `There has been a problem with ${type} delete operation:'`,
      error
    );
  });
};

const adminApi = {
  Delete,
};
export default adminApi;
