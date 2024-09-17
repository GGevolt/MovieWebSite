import Button from "react-bootstrap/Button";
import React, { useContext } from "react";
import PropTypes from "prop-types";
import AdminContext from "../../AminContext/Context";

const DeleteButton = ({ type, id }) => {
  const adminContext = useContext(AdminContext);
  const { Delete } = adminContext;
  const handleDelete = async () => {
    await Delete(type, id);
  };
  return (
    <Button variant="outline-danger" onClick={() => handleDelete()}>
      Delete
    </Button>
  );
};
DeleteButton.propTypes = {
  type: PropTypes.string,
  id: PropTypes.number,
};
export default DeleteButton;
