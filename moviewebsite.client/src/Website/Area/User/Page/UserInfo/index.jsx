import { useEffect, useState, useContext } from 'react';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { 
  PersonFill, 
  EnvelopeFill, 
  GenderAmbiguous, 
  CalendarEventFill, 
  CalendarDateFill, 
  ClockFill 
} from 'react-bootstrap-icons';
import { Crown } from 'lucide-react';
import AuthApi from '../../../AuthApi';
import AuthContext from '../../../AuthContext/Context';
import styles from './UserInfo.module.css';

export default function UserInfo() {
  const authContext = useContext(AuthContext);
  const { isLoggedIn, redirectToCustomerPortal } = authContext;
  const [userInfo, setUserInfo] = useState(null);

  useEffect(() => {
    if (isLoggedIn) {
      fetchUserInfo();
    }
  }, [isLoggedIn]);

  const fetchUserInfo = async () => {
    try {
      const info = await AuthApi.getUserInfo();
      setUserInfo(info);
    } catch (error) {
      console.error('Failed to fetch user info:', error);
    }
  };

  if (!isLoggedIn) {
    return (
      <Container className={styles.container}>
        <div className={styles.warning}>
          Please log in to view your information.
        </div>
      </Container>
    );
  }

  return (
    <Container className={styles.container}>
      <h1 className={styles.title}>User Information</h1>
      {userInfo ? (
        <Row>
          <Col md={8} lg={6} className="mx-auto">
            <Card className={styles.card}>
              <Card.Body>
                <div className={styles.avatarContainer}>
                  <div className={styles.avatar}>
                    {userInfo.fullName.charAt(0).toUpperCase()}
                  </div>
                </div>
                <Card.Title className={styles.cardTitle}>{userInfo.fullName}</Card.Title>
                <div className={styles.infoItem}>
                  <PersonFill className={styles.icon} />
                  <strong>Username:</strong> {userInfo.userName}
                </div>
                <div className={styles.infoItem}>
                  <EnvelopeFill className={styles.icon} />
                  <strong>Email:</strong> {userInfo.email}
                </div>
                <div className={styles.infoItem}>
                  <GenderAmbiguous className={styles.icon} />
                  <strong>Gender:</strong> {userInfo.gender}
                </div>
                <div className={styles.infoItem}>
                  <CalendarDateFill className={styles.icon} />
                  <strong>Date of Birth:</strong> {new Date(userInfo.dob).toLocaleDateString()}
                </div>
                <div className={styles.infoItem}>
                  <CalendarEventFill className={styles.icon} />
                  <strong>Account Created:</strong> {new Date(userInfo.accountCreatedDate).toLocaleDateString()}
                </div>
                {userInfo.subscriptionPlan ? (
                  <>
                    <div className={styles.subscriptionInfo}>
                      <div className={styles.infoItem}>
                        <Crown className={styles.icon} />
                        <strong>Subscription Plan:</strong> {userInfo.subscriptionPlan}
                      </div>
                      <div className={styles.infoItem}>
                        <ClockFill className={styles.icon} />
                        <strong>Subscription Ends:</strong> {new Date(userInfo.subscriptionEndPeriod).toLocaleDateString()}
                      </div>
                    </div>
                    <Button variant="outline-primary" className={styles.manageButton} onClick={redirectToCustomerPortal}>
                      Manage Subscription
                    </Button>
                  </>
                ) : (
                  <div className={styles.subscriptionCta}>
                    <p>Enhance your experience with a membership!</p>
                    <Link to="/user/memberships" className={styles.subscribeButton}>
                      Subscribe Now
                    </Link>
                  </div>
                )}
              </Card.Body>
            </Card>
          </Col>
        </Row>
      ) : (
        <div className={styles.loading}>Loading user information...</div>
      )}
    </Container>
  );
}