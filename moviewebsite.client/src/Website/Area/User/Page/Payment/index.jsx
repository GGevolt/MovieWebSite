import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import React from "react";
import PaymentForm from "../../Components/Form/PaymentForm";

const stripePromise = loadStripe(
  "pk_test_51Q11QnATmHlXrMYovC1tzGFpr6n8jcij4FbkfDJawgky4z49rZ7qQ89kpj99mjAVQ1d0196mcpQhJ0lmQqlAisMG002QL8lkKO"
);
function PaymentPage() {
  const options = {
    mode: "payment",
    amount: 1099,
    currency: "usd",
    // Fully customizable with appearance API.
    appearance: {
      /*...*/
    },
  };
  return (
    <Elements stripe={stripePromise} options={options}>
      <PaymentForm />
    </Elements>
  );
}

export default PaymentPage;
