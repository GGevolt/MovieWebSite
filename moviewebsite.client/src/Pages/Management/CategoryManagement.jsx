import React, { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Table from 'react-bootstrap/Table';
import Spinner from 'react-bootstrap/Spinner';
import axios from 'axios';
import CategoryCUForm from '../../Components/Form/CategoryCUForm';
import NavBar from "../../Components/NavBar";
import Footer from "../../Footer";
import Pagination from '../../Components/Pagination';
import Delete from '../../Components/Utility/Delete';
import './Management.css';

function CategoryManagement(){
    const [categories, setCategories] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 6;
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


    const categotyTable = [
            <Table striped bordered responsive hover variant="dark">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Category</th>
                        <th>Functions</th>
                    </tr>
                </thead>
                <tbody>
                    {currentItems.map((category,i) => (
                        <tr key={i} >
                            <td>{category.id}</td>
                            <td>{category.name}</td>
                            <td>
                                <div className='func'>
                                    <CategoryCUForm category={category} onSuccess={refreshData} />
                                    <Delete type="category" onSuccess={refreshData} id={category.id}/>
                                </div>
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
                <div className="Pag">
                    <Pagination data={categories} onPageChange={handlePageChange} itemsPerPage= {itemsPerPage}/>
                </div>
            </Container>
            <Footer/>
        </div>
    );
}
export default CategoryManagement;