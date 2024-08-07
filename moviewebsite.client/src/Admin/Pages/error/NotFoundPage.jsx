import React from 'react';
import NavBar from "../../Components/NavBar";
import Footer from "../../Components/Footer";

const NotFoundPage = () => {
    return (
        <div className='body'>
            <NavBar/>
            <h1>404 - Page Not Found</h1>
            <Footer/>
        </div>
        );
};

export default NotFoundPage;