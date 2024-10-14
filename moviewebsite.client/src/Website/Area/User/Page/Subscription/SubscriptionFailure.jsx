import { Container, Row, Col, Card, Button } from "react-bootstrap";
import {
  EnvelopeAtFill,
  QuestionCircleFill,
  ArrowCounterclockwise,
  ExclamationTriangleFill,
} from "react-bootstrap-icons";
import { useNavigate } from "react-router-dom";
import AuthContext from "../../../AuthContext/Context";
import { useContext, useEffect } from "react";
import styles from "./SubscriptionFailure.module.css";

export default function SubscriptionFailure() {
  const navigate = useNavigate();
  const authContext = useContext(AuthContext);
  const { isUserUpdated, getUserStatus } = authContext;

  useEffect(() => {
    if (isUserUpdated) {
      getUserStatus();
    }
  }, [isUserUpdated, getUserStatus]);

  return (
    <div className={styles.subscriptionFailureWrapper}>
      <Container className="py-5">
        <Row className="justify-content-center">
          <Col md={8}>
            <Card className={styles.failureCard}>
              <Card.Body className="text-center">
                <ExclamationTriangleFill
                  className={`${styles.failureIcon} mb-4`}
                />
                <Card.Title as="h1" className={`${styles.title} mb-4`}>
                  Subscription Unsuccessful
                </Card.Title>
                <Card.Text className={`${styles.leadText} mb-4`}>
                  We're sorry, but there was an issue with your subscription.
                  This could be due to a payment failure or a cancellation
                  request.
                </Card.Text>
                <div className={`${styles.optionsGrid} mb-4`}>
                  <div
                    className={styles.optionItem}
                    onClick={() => navigate("/user/memberships")}
                  >
                    <ArrowCounterclockwise className={styles.optionIcon} />
                    <p>Try Again</p>
                  </div>
                  <div className={styles.optionItem}>
                    <QuestionCircleFill className={styles.optionIcon} />
                    <p>FAQ</p>
                  </div>
                  <div className={styles.optionItem}>
                    <EnvelopeAtFill className={styles.optionIcon} />
                    <p>Contact Support</p>
                  </div>
                </div>
                <Button
                  variant="primary"
                  size="lg"
                  className={styles.returnHomeBtn}
                  onClick={() => navigate("/")}
                >
                  Return to Homepage
                </Button>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
      <div className={`${styles.decorationCircle} ${styles.circle1}`}></div>
      <div className={`${styles.decorationCircle} ${styles.circle2}`}></div>
      <div className={`${styles.decorationCircle} ${styles.circle3}`}></div>
    </div>
  );
}
