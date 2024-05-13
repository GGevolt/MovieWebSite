import React, { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Table from 'react-bootstrap/Table';
import Spinner from 'react-bootstrap/Spinner';
import axios from 'axios';
import NavBar from "../../Components/NavBar";
import Footer from "../../Footer";
import Image from 'react-bootstrap/Image';
import FilmCUForm from '../../Components/Form/FilmCUForm';
import Pagination from '../../Components/Pagination';
import PictureUpload from '../../Components/Upload/PictureUpload';
import Delete from '../../Components/Utility/Delete';
import './Management.css';

function FilmManagement(){
    const [films, setFilms] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 6;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = films.slice(indexOfFirstItem, indexOfLastItem);

    useEffect(() => {
        refreshData();
    }, []);

    const handlePageChange = (page) => {
        setCurrentPage(page);
    }
    const refreshData=() => {
        axios.get('/film')
            .then(response => {
                setFilms(response.data);
                setIsLoading(false);
            })
           .catch(error => {
                console.error('There has been a problem with film get operation:', error);
                setIsLoading(false);
            });
    }

    const filmTable = [
            <Table striped bordered hover variant="dark" id='film-table'>
                <thead>
                    <tr>
                        <th className='image-col'>Image</th>
                        <th className='title-col'>Title</th>
                        <th className='synopsis-col'>Synopsis</th>
                        <th className='director-col'>Diretor</th>
                        <th className='funcs-col'>Functions</th>
                    </tr>
                </thead>
                <tbody>
                    {currentItems.map((film,i) => (
                        <tr key={i} >
                            <td>{film.filmImg !=='' && <Image src={`/images/${film.filmImg}`} thumbnail alt='Movie picture' className='movie-pic'/>}</td>
                            <td>{film.title}</td>
                            <td>{film.synopsis}</td>
                            <td>{film.director}</td>
                            <td>
                                <div className='func'>
                                    <PictureUpload id={film.id} onSuccess={refreshData}/>
                                    <FilmCUForm film={film} onSuccess={refreshData}/>
                                    <Delete type="film" id={film.id} onSuccess={refreshData}/>
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
                    <h1>Manage Films</h1>
                    <FilmCUForm onSuccess={refreshData}/>
                </Container>
                {isLoading? (<Spinner animation="border" /> ) 
                : films.length > 0? filmTable 
                : (<h4>No Movies found.</h4>)}
                <div className="Pag">
                    <Pagination data={films} onPageChange={handlePageChange} itemsPerPage= {itemsPerPage}/>
                </div>
            </Container>
            <Footer/>
        </div>
    );
}
export default FilmManagement;