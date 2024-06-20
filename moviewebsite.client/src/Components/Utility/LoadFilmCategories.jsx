import React, {useState, useEffect} from "react";
import PropTypes from 'prop-types';
import axios from 'axios';

function LoadFilmCategories({filmId, isCateUpdated, onCateUpdate}){
    const [filmCates,setFilmCates] = useState([]);
    useEffect(() => {
        if (filmId!==0 || isCateUpdated) {
            axios.get(`/filmCate/${filmId}`)
                .then(response => {
                    setFilmCates(response.data);
                    onCateUpdate(false);
                })
                .catch(error => {
                    console.error('There has been a problem with get film categories operation:', error);
                });
        }
    }, [filmId, isCateUpdated]);
    return(
        filmCates.map((cate,i)=>(
            <p key={cate.id}>{cate.name}{filmCates.length === i+1?".":","}</p>
        ))
    );
}

LoadFilmCategories.propTypes ={
    filmId : PropTypes.number.isRequired,
    isCateUpdated : PropTypes.bool,
    onCateUpdate : PropTypes.func
}

export default LoadFilmCategories;