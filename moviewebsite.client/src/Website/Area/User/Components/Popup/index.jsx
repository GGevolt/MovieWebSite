import Popup from "reactjs-popup";
import PropTypes from "prop-types";
import {  Card } from 'react-bootstrap';
import { MessageCircleX } from "lucide-react"
import style from "./popup.module.css";

function PopUp({ isOpen, handleClose, children }) {
  return (
    <Popup open={isOpen} closeOnDocumentClick onClose={handleClose}>
      <Card className={`bg-dark text-light border-primary ${style.card}`}>
        <a className="close-pop" onClick={handleClose}>
          <MessageCircleX className={style.close_btn}/>
        </a>
        <Card.Body>
          {children}
        </Card.Body>
      </Card>
    </Popup>
  );
}

PopUp.propTypes = {
  isOpen: PropTypes.bool,
  handleClose: PropTypes.func,
  children: PropTypes.node,
};

export default PopUp;
