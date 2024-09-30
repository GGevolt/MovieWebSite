import { useState } from "react";
import { Button } from 'react-bootstrap';
import {
  useStripe,
  useElements,
  PaymentElement,
} from "@stripe/react-stripe-js";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";
import PopUp from "../Popup";
import axios from 'axios'

function PaymentForm({plan, amount}) {
  document.title = "Paymnet";
  const navigate = useNavigate();
  const stripe = useStripe();
  const elements = useElements();
  const [errorMessage, setErrorMessage] = useState();
  const [loading, setLoading] = useState(false);
  const [isPopupOpen, setIsPopupOpen] = useState(false);
  
  const handleError = (error) => {
    setLoading(false);
    setErrorMessage(error.message);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return;
    }

    setLoading(true);

    // Trigger form validation and wallet collection
    const { error: submitError } = await elements.submit();
    if (submitError) {
      return;
    }

    const res = await axios.post("/api/payment", { amount },{
        headers: { 'Content-Type': 'application/json' }
    });

    const {client_secret: clientSecret} = await res.data.client_secret;
    const { error, paymentIntent } = await stripe.confirmPayment({
      elements,
      clientSecret,
      confirmParams: {
        return_url: `${window.location.origin}/`,
      },
      redirect: "if_required",
    });

    if (error) {
      handleError(error);
    } else if(paymentIntent && paymentIntent.status === "succeeded"){
      setIsPopupOpen(true);
    }
  };
  const handleClosePop = () => {
    setIsPopupOpen(false);
    navigate("/");
  };

  const popUpContent = () => {
    return (
      <div>
        <h3>
          Payment successful
        </h3>
        <p>
          You have successfully pay for the {plan} plan.
        </p>
      </div>
    );
  };
  return (
    <div>
      {
        <PopUp isOpen={isPopupOpen} handleClose={handleClosePop}>
          {popUpContent()}
        </PopUp>
      }
      <form onSubmit={handleSubmit}>
        <div>
          <PaymentElement />
          <Button type="submit" disabled={!stripe || loading}>
            Pay for the {plan} plan
          </Button>
          {errorMessage && <div>{errorMessage}</div>}
        </div>
      </form>
    </div>
  );
}
PaymentForm.propTypes = {
  plan: PropTypes.string.isRequired,
  amount: PropTypes.number.isRequired,
};

export default PaymentForm;
