import { useState, useContext } from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import ConfirmMembershipsPopUp from '../../Components/Popup/ConfirmMembershipsPopUp';
import { Navigate } from 'react-router-dom';
import AuthContext from "../../../AuthContext/Context";
import './index.css';

export default function Memberships() {
    const [selectedPlan, setSelectedPlan] = useState(null);
    const authContext = useContext(AuthContext);
    const { roles } = authContext;
    const handleSelectPlan = (plan) => {
        setSelectedPlan(plan);
        setIsPopupOpen(true);
    };
    const handleClosePopUp = () => {
        setIsPopupOpen(false);
        setSelectedPlan(null);
      };
    const [isPopupOpen, setIsPopupOpen] = useState(false);
    if(roles.length > 1){
        return <Navigate to="/" replace/>
    }
    return (
        <div className="subscription-wrapper">
            {
                <ConfirmMembershipsPopUp isOpen={isPopupOpen} selectedPlan={selectedPlan} handleClose={handleClosePopUp} />
            }
        <Container className="py-5">
            <h1 className="text-center mb-5 text-light">Choose Your Memberships Plan</h1>
            <Row className="justify-content-center">
            <Col md={5}>
                <Card className={`mb-4 h-100 bg-dark text-light ${selectedPlan === 'pro' ? 'border-primary' : 'border-secondary'}`}>
                <Card.Header as="h5" className="text-center bg-secondary">Pro Memberships</Card.Header>
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
                    variant={selectedPlan === 'pro' ? 'primary' : 'outline-primary'} 
                    className="w-100 mt-3"
                    onClick={() => handleSelectPlan('pro')}
                    >
                    {selectedPlan === 'pro' ? 'Selected' : 'Choose Pro'}
                    </Button>
                </Card.Body>
                </Card>
            </Col>
            <Col md={5}>
                <Card className={`mb-4 h-100 bg-dark text-light ${selectedPlan === 'premium' ? 'border-primary' : 'border-secondary'}`}>
                <Card.Header as="h5" className="text-center bg-secondary">Premium Memberships</Card.Header>
                <Card.Body className="d-flex flex-column justify-content-between">
                    <div className="text-center mb-4">
                    <Card.Title className="text-primary display-6">$180/year</Card.Title>
                    </div>
                    <ul className="list-unstyled">
                    <li>All Monthly Plan Features</li>
                    <li>Create your own playlist of films</li>
                    <li>Save 25% compared to monthly plan</li>
                    </ul>
                    <Button 
                    variant={selectedPlan === 'premium' ? 'primary' : 'outline-primary'} 
                    className="w-100 mt-3"
                    onClick={() => handleSelectPlan('premium')}
                    >
                    {selectedPlan === 'premium' ? 'Selected' : 'Choose Premium'}
                    </Button>
                </Card.Body>
                </Card>
            </Col>
            </Row>
        </Container>
        </div>
    );
}