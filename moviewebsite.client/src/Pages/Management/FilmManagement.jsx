import React, { useEffect, useState } from 'react';
import {Spinner, Image, Container, Card} from 'react-bootstrap';
import axios from 'axios';
import NavBar from "../../Components/NavBar";
import Footer from "../../Footer";
import FilmCUForm from '../../Components/Form/FilmCUForm';
import Pagination from '../../Components/Pagination';
import PictureUpload from '../../Components/Upload/PictureUpload';
import Delete from '../../Components/Utility/Delete';
import LoadFilmCategories from '../../Components/Utility/LoadFilmCategories';
import './Management.css';

function FilmManagement(){
    const [films, setFilms] = useState([]);
    const [isCateUpdated, setIsCateUpdated] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 6;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = films.slice(indexOfFirstItem, indexOfLastItem);

    useEffect(() => {
        refreshData();
    }, []);

    const hanldeCateUpdate =(updateStatus)=>{
        setIsCateUpdated(updateStatus);
    }
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

    const displayFilms=[
        <Container className='film-container'>
            {currentItems.map((film) => (
                <Card className='film-card' key={film.id}>
                    <Card.Title className='film-title'>{film.title}</Card.Title>
                    <div className='film-image'><Image src={`/images/${film.filmImg}`} thumbnail alt='Movie picture'/></div>
                    <div className='film-info'>
                        <div className='film-info-director'><strong>Director:</strong> {film.director}</div>
                        <div className='film-info-categories'><strong>Categories:</strong> <LoadFilmCategories filmId={film.id} isCateUpdated={isCateUpdated} onCateUpdate={hanldeCateUpdate}/></div>
                    </div>
                    <div className='film-functions'>
                        <strong>Functionalities: </strong>
                        <div className='film-functions-content'>
                            <PictureUpload id={film.id} onSuccess={refreshData}/>
                            <FilmCUForm film={film} onSuccess={refreshData} onCateUpdate={hanldeCateUpdate}/>
                            <Delete type="film" id={film.id} onSuccess={refreshData}/>
                        </div>
                    </div>
                    <div className='film-synopsis'><h5>Synopsis</h5>{film.synopsis}</div>
                </Card>
            ))}
            <div className="Pag">
                    <Pagination data={films} onPageChange={handlePageChange} itemsPerPage= {itemsPerPage}/>
            </div>
        </Container>
    ]

    return (
        <div className='body'>
            <NavBar/>
            <Container fluid>
                <Container className='firstLine'>
                    <h1>Manage Films</h1>
                    <FilmCUForm onSuccess={refreshData}/>
                </Container>
                {isLoading? (<Spinner animation="border" /> ) : films.length > 0? displayFilms : (<h4>No Movies found.</h4>)}
            </Container>
            <Footer/>
        </div>
    );
}
export default FilmManagement;