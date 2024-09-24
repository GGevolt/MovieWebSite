import React,{useEffect,useState} from "react";
import { Blurhash } from 'react-blurhash';
import PropTypes from "prop-types";

export default function FilmImg({src, hash, name}){
    const [isImgLoaded,setIsImgLoaded] = useState(false);
    useEffect(() => {
        const img = new Image();
        img.onload = () =>{
          setIsImgLoaded(true);
        };
        img.src = src;
    }, [src]);

    if(!isImgLoaded){
        return<Blurhash hash={hash} width="100%" height="100%"  className={name}/>
    }

    return <img src={src} alt="Movie picture" loading="lazy" className={name}/>
}

FilmImg.propTypes = {
    src: PropTypes.string.isRequired,
    hash: PropTypes.string,
    name: PropTypes.string
};