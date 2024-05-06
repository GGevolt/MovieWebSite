import React, { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Table from 'react-bootstrap/Table';
import Spinner from 'react-bootstrap/Spinner';
import axios from 'axios';
import CategoryCUForm from '../../Components/Form/CategoryCUForm';
import NavBar from "../../Components/NavBar";
import Footer from "../../Footer";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Pagination from '../../Components/Pagination';
import './Management.css';

function CategoryManagement(){
    const [categories, setCategories] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 5;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = categories.slice(indexOfFirstItem, indexOfLastItem);

    useEffect(() => {
        refreshData();
    }, []);

    const handlePageChange = (page) => {
        setCurrentPage(page);
    }
    const refreshData=() => {
        axios.get('/category')
            .then(response => {
                setCategories(response.data);
                setIsLoading(false);
            })
           .catch(error => {
                console.error('There has been a problem with category get operation:', error);
                setIsLoading(false);
            });
    }
    
    const handleDelete = (event) =>{
        event.preventDefault();
        const id = event.target.elements.id.value;
        axios.delete(`/category/${id}`).then(() => {
            refreshData();
        }).catch(error => {
            console.error('There has been a problem with category delete operation:', error);
        });
    }


    const categotyTable = [
            <Table striped bordered responsive hover variant="dark">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Category</th>
                        <th>Update</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {currentItems.map((category,i) => (
                        <tr key={i} >
                            <td>{category.id}</td>
                            <td>{category.name}</td>
                            <td>
                                <CategoryCUForm category={category} onSuccess={refreshData} />
                            </td>
                            <td>
                                <Form onSubmit={handleDelete}>
                                    <input type="hidden" name="id" value={category.id} />
                                    <Button variant="outline-danger"type="submit">Delete</Button>
                                </Form>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>]

    return (
        <div className='body'>
            <NavBar/>
            <Container fluid>
                <Container className='firstLine'>
                    <h1>Manage Category</h1>
                    <CategoryCUForm onSuccess={refreshData}/>
                </Container>
                {isLoading? (<Spinner animation="border" /> ) 
                : categories.length > 0? categotyTable 
                : (<h4>No categories found.</h4>)}
                <div className="catePag">
                    <Pagination data={categories} onPageChange={handlePageChange} itemsPerPage= {itemsPerPage}/>
                </div>
            </Container>
            <Footer/>
        </div>
    );
}
export default CategoryManagement;