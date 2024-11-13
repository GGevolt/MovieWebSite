import React, { useContext, useEffect, useState } from 'react'
import AdminContext from '../../AminContext/Context'
import adminApi from '../../AdminApi/adminApi'
import styles from './AccountManagement.module.css'
import { Container, Card, Button, Badge, Spinner } from 'react-bootstrap'
import { EnvelopeFill, CalendarFill, ClockFill } from 'react-bootstrap-icons'
import { Users, Trash2, XCircle } from 'lucide-react'
import Pagination from '../../Components/Pagination'

function AccountManagement() {
    const adminContext = useContext(AdminContext);
    const { userList, getUsers, Delete } = adminContext;
    const [isLoading, setIsLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 6;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = userList ? userList.slice(indexOfFirstItem, indexOfLastItem) : [];
    const fetchUsers = async () => {
        setIsLoading(true)
        await getUsers()
        setIsLoading(false)
    }
    useEffect(() => {
        fetchUsers()
    }, []) 

    const handleDeleteAccount = async (userId) => {
        if (window.confirm('Are you sure you want to delete this account?')) {
            await Delete('account', userId);
            await getUsers();
        }
    }

    const handleCancelSubscription = async (userId) => {
        if (window.confirm('Are you sure you want to cancel this subscription?')) {
            await adminApi.cancelSubscription(userId);
            await getUsers();
        }
    }

    const handlePageChange = (page) => {
        setCurrentPage(page);
    }

    if (isLoading) {
        return (
        <Container fluid className={styles.container}>
            <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
            <Spinner animation="border" role="status" variant="light">
                <span className="visually-hidden">Loading...</span>
            </Spinner>
            </div>
        </Container>
        )
    }

    return (
        <Container fluid className={styles.container}>
        <h1 className={styles.header}>
            <Users className="me-2" aria-hidden="true" />
            Account Management
        </h1>
        <div className={styles.userList}>
            {currentItems.length > 0 ? (
            currentItems.map((user) => (
                <Card key={user.userId} className={styles.userCard}>
                <Card.Body>
                    <Card.Title className="d-flex justify-content-between align-items-center mb-3">
                    <h2 className="h4 mb-0 text-light">{user.userName}</h2>
                    <Badge 
                        className={`${styles.subscriptionBadge} ${
                        user.subscriptionPlan === 'Not Subscribed' ? styles.notSubscribed : styles.subscribed
                        }`}
                    >
                        {user.subscriptionPlan}
                    </Badge>
                    </Card.Title>
                    <div className={styles.userInfo}>
                    <EnvelopeFill aria-hidden="true" />
                    <span>Email: {user.email}</span>
                    </div>
                    <div className={styles.userInfo}>
                    <CalendarFill aria-hidden="true" />
                    <span>Created: {new Date(user.createdDate).toLocaleDateString()}</span>
                    </div>
                    <div className={styles.userInfo}>
                    <ClockFill aria-hidden="true" />
                    <span>Last Login: {user.lastLogin ? new Date(user.lastLogin).toLocaleString() : 'N/A'}</span>
                    </div>
                    <div className={styles.buttonGroup}>
                    <Button
                        variant="outline-danger"
                        onClick={() => handleDeleteAccount(user.userId)}
                        className="d-flex align-items-center"
                    >
                        <Trash2 size={18} className="me-2" aria-hidden="true" />
                        Delete Account
                    </Button>
                    {user.subscriptionPlan !== 'Not Subscribed' && (
                        <Button
                        variant="outline-warning"
                        onClick={() => handleCancelSubscription(user.userId)}
                        className="d-flex align-items-center"
                        >
                        <XCircle size={18} className="me-2" aria-hidden="true" />
                        Cancel Subscription
                        </Button>
                    )}
                    </div>
                </Card.Body>
                </Card>
            ))
            ) : (
            <div className="text-center text-light">No users found.</div>
            )}
        </div>
        <div className={styles.pagination}>
            <Pagination
            data={userList || []}
            onPageChange={handlePageChange}
            itemsPerPage={itemsPerPage}
            />
        </div>
        </Container>
    )
}

export default AccountManagement