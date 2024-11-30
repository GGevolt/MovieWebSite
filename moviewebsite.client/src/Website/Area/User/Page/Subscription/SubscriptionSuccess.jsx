import { Container, Row, Col, Card, Button } from "react-bootstrap";
import { StarFill, Tv, Film, CheckCircleFill } from "react-bootstrap-icons";
import { useNavigate } from "react-router-dom";
import styles from "./SubscriptionSuccess.module.css";

export default function SubscriptionSuccess() {
  const navigate = useNavigate();
  return (
    <div className={styles.subscriptionSuccessWrapper}>
      <Container className="py-5">
        <Row className="justify-content-center">
          <Col md={8}>
            <Card className={styles.successCard}>
              <Card.Body className="text-center">
                <CheckCircleFill className={`${styles.successIcon} mb-4`} />
                <Card.Title as="h1" className={`${styles.title} mb-4`}>
                  Subscription Successful!
                </Card.Title>
                <Card.Text className={`${styles.leadText} mb-4`}>
                  Welcome to our premium streaming service. Your account is now
                  active and ready to use.
                </Card.Text>
                <div className={`${styles.featuresGrid} mb-4`}>
                  <div className={styles.featureItem}>
                    <Film className={styles.featureIcon} />
                    <p>Unlimited Movies</p>
                  </div>
                  <div className={styles.featureItem}>
                    <Tv className={styles.featureIcon} />
                    <p>Exclusive TV Shows</p>
                  </div>
                  <div className={styles.featureItem}>
                    <StarFill className={styles.featureIcon} />
                    <p>Personalized Recommendations</p>
                  </div>
                </div>
                <Button
                  variant="primary"
                  size="lg"
                  className={styles.startWatchingBtn}
                  onClick={() => navigate("/")}
                >
                  Start Watching Now
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
