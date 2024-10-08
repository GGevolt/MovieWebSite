import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { StarFill, Tv, Film, CheckCircleFill } from "react-bootstrap-icons"
import {useNavigate} from "react-router-dom"
import AuthContext from '../../../AuthContext/Context';
import { useContext, useEffect } from 'react';
import './SubscriptionSuccess.css';

export default function SubscriptionSuccess() {
    const authContext = useContext(AuthContext);
    const{ isUserUpdated, getUserStatus} = authContext;
    useEffect(()=>{
        if(isUserUpdated){
          getUserStatus();
        }
      },[])
    const navigate = useNavigate();
    return (
        <div className="subscription-success-wrapper">
        <Container className="py-5">
            <Row className="justify-content-center">
            <Col md={8}>
                <Card className="success-card">
                <Card.Body className="text-center">
                    <CheckCircleFill className="success-icon mb-4" />
                    <Card.Title as="h1" className="mb-4">Subscription Successful!</Card.Title>
                    <Card.Text className="lead mb-4">
                    Welcome to our premium streaming service. Your account is now active and ready to use.
                    </Card.Text>
                    <div className="features-grid mb-4">
                    <div className="feature-item">
                        <Film className="feature-icon" />
                        <p>Unlimited Movies</p>
                    </div>
                    <div className="feature-item">
                        <Tv className="feature-icon" />
                        <p>Exclusive TV Shows</p>
                    </div>
                    <div className="feature-item">
                        <StarFill className="feature-icon" />
                        <p>Personalized Recommendations</p>
                    </div>
                    </div>
                    <Button variant="primary" size="lg" className="start-watching-btn" onClick={()=>navigate("/")}>
                    Start Watching Now
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