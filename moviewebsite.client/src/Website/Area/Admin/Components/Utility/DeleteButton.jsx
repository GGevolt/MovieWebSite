import Button from "react-bootstrap/Button";
import { useContext } from "react";
import PropTypes from "prop-types";
import { Trash } from "react-bootstrap-icons";
import AdminContext from "../../AminContext/Context";

const DeleteButton = ({ type, id, name }) => {
  const adminContext = useContext(AdminContext);
  const { Delete } = adminContext;
  const handleDelete = async () => {
    await Delete(type, id);
  };
  return (
    <Button
      variant="outline-danger"
      className={name}
      onClick={() => handleDelete()}
    >
      <Trash className="me-2"/> Delete
    </Button>
  );
};
DeleteButton.propTypes = {
  type: PropTypes.string,
  id: PropTypes.number,
  name: PropTypes.string
};
export default DeleteButton;
