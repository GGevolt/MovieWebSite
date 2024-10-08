import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import {EnvelopeAtFill, QuestionCircleFill, ArrowCounterclockwise , ExclamationTriangleFill} from "react-bootstrap-icons"
import {useNavigate} from "react-router-dom"
import AuthContext from '../../../AuthContext/Context';
import { useContext, useEffect } from 'react';
import './SubscriptionFailure.css';

export default function SubscriptionFailure() {
    const navigate = useNavigate();
    const authContext = useContext(AuthContext);
    const{ isUserUpdated, getUserStatus} = authContext;
    useEffect(()=>{
        if(isUserUpdated){
          getUserStatus();
        }
      },[])
    return (
        <div className="subscription-failure-wrapper">
        <Container className="py-5">
            <Row className="justify-content-center">
            <Col md={8}>
                <Card className="failure-card">
                <Card.Body className="text-center">
                    <ExclamationTriangleFill className="failure-icon mb-4" />
                    <Card.Title as="h1" className="mb-4">Subscription Unsuccessful</Card.Title>
                    <Card.Text className="lead mb-4">
                    We&apos;re sorry, but there was an issue with your subscription. This could be due to a payment failure or a cancellation request.
                    </Card.Text>
                    <div className="options-grid mb-4">
                    <div className="option-item" onClick={()=>navigate("/user/memberships")}>
                        <ArrowCounterclockwise className="option-icon" />
                        <p>Try Again</p>
                    </div>
                    <div className="option-item">
                        <QuestionCircleFill className="option-icon" />
                        <p>FAQ</p>
                    </div>
                    <div className="option-item">
                        <EnvelopeAtFill className="option-icon" />
                        <p>Contact Support</p>
                    </div>
                    </div>
                    <Button variant="primary" size="lg" className="return-home-btn" onClick={()=>navigate("/")}>
                        Return to Homepage
                    </Button>
                </Card.Body>
                </Card>
            </Col>
            </Row>
        </Container>
        <div className="decoration-circle circle-1"></div>
        <div className="decoration-circle circle-2"></div>
        <div className="decoration-circle circle-3"></div>
        </div>
    );
}