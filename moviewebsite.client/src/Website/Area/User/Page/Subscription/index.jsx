import { useState, useMemo } from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import './index.css';
import PaymentForm from '../../Components/Form/PaymentForm';

const stripePromise = loadStripe(
    "pk_test_51Q11QnATmHlXrMYovC1tzGFpr6n8jcij4FbkfDJawgky4z49rZ7qQ89kpj99mjAVQ1d0196mcpQhJ0lmQqlAisMG002QL8lkKO"
);

export default function Subscription() {
    const [selectedPlan, setSelectedPlan] = useState(null);
    const paymentAmount = useMemo(() => ({
        monthly: 1999,
        yearly: 18000
      }), []);
    const options = {
        mode: 'payment',
        amount: paymentAmount[selectedPlan],
        currency: 'usd',
        appearance: {
            theme: 'night',
            labels: 'floating'
        },
    };
    const handleSelectPlan = (plan) => {
        setSelectedPlan(plan);
    };

    return (
        <div className="subscription-wrapper">
        <Container className="py-5">
            <h1 className="text-center mb-5 text-light">Choose Your Subscription Plan</h1>
            <Row className="justify-content-center">
            <Col md={5}>
                <Card className={`mb-4 h-100 bg-dark text-light ${selectedPlan === 'monthly' ? 'border-primary' : 'border-secondary'}`}>
                <Card.Header as="h5" className="text-center bg-secondary">Monthly Plan</Card.Header>
                <Card.Body className="d-flex flex-column justify-content-between">
                    <div className="text-center mb-4">
                    <Card.Title className="text-primary display-6">$19.99/month</Card.Title>
                    </div>
                    <ul className="list-unstyled">
                    <li>Watch any movie</li>
                    <li>Watch any TV series</li>
                    <li>Leave comments and ratings</li>
                    </ul>
                    <Button 
                    variant={selectedPlan === 'monthly' ? 'primary' : 'outline-primary'} 
                    className="w-100 mt-3"
                    onClick={() => handleSelectPlan('monthly')}
                    >
                    {selectedPlan === 'monthly' ? 'Selected' : 'Choose Monthly'}
                    </Button>
                </Card.Body>
                </Card>
            </Col>
            <Col md={5}>
                <Card className={`mb-4 h-100 bg-dark text-light ${selectedPlan === 'yearly' ? 'border-primary' : 'border-secondary'}`}>
                <Card.Header as="h5" className="text-center bg-secondary">Yearly Plan</Card.Header>
                <Card.Body className="d-flex flex-column justify-content-between">
                    <div className="text-center mb-4">
                    <Card.Title className="text-primary display-6">$180/year</Card.Title>
                    </div>
                    <ul className="list-unstyled">
                    <li>All Monthly Plan Features</li>
                    <li>Create your own playlist of films</li>
                    <li>Share playlists with everyone</li>
                    <li>Save 25% compared to monthly plan</li>
                    </ul>
                    <Button 
                    variant={selectedPlan === 'yearly' ? 'primary' : 'outline-primary'} 
                    className="w-100 mt-3"
                    onClick={() => handleSelectPlan('yearly')}
                    >
                    {selectedPlan === 'yearly' ? 'Selected' : 'Choose Yearly'}
                    </Button>
                </Card.Body>
                </Card>
            </Col>
            </Row>
            {selectedPlan && stripePromise && (
                    <div className="text-center bg-dark mt-4 payment">
                        <h4>You&apos;ve selected the {selectedPlan} plan!</h4>
                        <Elements stripe={stripePromise} options={options}>
                            <PaymentForm plan={selectedPlan} amount={paymentAmount[selectedPlan]} />
                        </Elements>
                    </div>
            )}
        </Container>
        </div>
    );
}