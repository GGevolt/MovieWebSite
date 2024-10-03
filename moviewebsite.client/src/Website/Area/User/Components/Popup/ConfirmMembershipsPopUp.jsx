import Popup from "reactjs-popup";
import PropTypes from "prop-types";
import {  Card, Button, Spinner } from 'react-bootstrap';
import style from "./popup.module.css";
import { MessageCircleX} from "lucide-react"
import AuthContext from "../../../AuthContext/Context";
import {useContext, useState} from "react";

function ConfirmMembershipsPopUp({ isOpen, handleClose, selectedPlan }) {
    const authContext = useContext(AuthContext);
    const { createCheckoutSession } = authContext;
    const [loading, SetLoading] = useState(false);
    const hanldeCheckOut = async ()=>{
        SetLoading(true);
        await createCheckoutSession(selectedPlan);
        SetLoading(false);
    }
    return (
        <Popup open={isOpen} closeOnDocumentClick onClose={handleClose}>
            <Card className={`bg-dark text-light border-primary ${style.confirmation_card}`}>
                <a className="close-pop" onClick={handleClose}>
                <MessageCircleX className={style.close_btn}/>
                </a>
                <Card.Body>
                    <h2 className="text-center mb-4">Confirm Your Purchase</h2>
                    <p className="text-center">
                        You have selected the <strong>{selectedPlan === 'pro' ? 'Pro' : 'Premium'} Membership</strong>.
                    </p>
                    <p className="text-center">
                        Price: <strong>{selectedPlan === 'pro' ? '$19.99/month' : '$180/year'}</strong>
                    </p>
                    <Button 
                        variant="primary" 
                        className={`w-100 mt-3 ${style.btn_primary}`}
                        onClick={hanldeCheckOut}
                        disabled={loading}
                    >
                        {loading? <Spinner/>: "Confirm Purchase"}
                    </Button>
                </Card.Body>
            </Card>
        </Popup>
    );
}

ConfirmMembershipsPopUp.propTypes = {
  isOpen: PropTypes.bool,
  handleClose: PropTypes.func,
  selectedPlan: PropTypes.string
};

export default ConfirmMembershipsPopUp;
