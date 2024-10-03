import axios from "axios";

const Delete = async (type, id) => {
  await axios
    .delete(`/api/${type}/${id}`)
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

const adminApi = {
  Delete,
};
export default adminApi;
