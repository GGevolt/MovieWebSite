import NavBar from "../../Components/NavBar";
import React, { useEffect, useState } from 'react';
import {Image, Container, Card, Button} from 'react-bootstrap';
import { useLocation } from 'react-router-dom';


function WatchFilm(){
    const location = useLocation();
    const film = {
        id: location.state.film.id,
        title: location.state.film.title,
        synopsis: location.state.film.synopsis,
        director: location.state.film.director,
        type: location.state.film.type,
        filmImg : location.state.film.filmImg
    };
    return(
        <div className='body'>
            <NavBar/>
            <Container fluid>
                <h1>Watch {film.title}</h1>
            </Container>
        </div>
    );
}
export default WatchFilm;