import Popup from "reactjs-popup";
import React from "react";
import PropTypes from "prop-types";
import style from "./popup.module.css";

function PopUp({ isOpen, handleClose, children }) {
  return (
    <Popup open={isOpen} closeOnDocumentClick onClose={handleClose}>
      <div className={style.popUp}>
        <a className={style.close} onClick={handleClose}>
          &times;
        </a>
        {children}
      </div>
    </Popup>
  );
}

PopUp.propTypes = {
  isOpen: PropTypes.bool,
  handleClose: PropTypes.func,
  children: PropTypes.node,
};

export default PopUp;
