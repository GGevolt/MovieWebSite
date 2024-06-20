import axios from 'axios';
import Button from 'react-bootstrap/Button';
import React from 'react';
import PropTypes from 'prop-types';

const Delete = ({ type, id, onSuccess})=>{
    if(type==="category" || type==="film" || type==="quality" && id > 0){
        const handleDelete = () =>{
            axios.delete(`/${type}/${id}`).then(() => {
                onSuccess();
            }).catch(error => {
                console.error(`There has been a problem with ${type} delete operation:'`, error);
            });
        }
        return(
            <Button variant="outline-danger" onClick={()=>handleDelete()}>Delete</Button>
        );
    }
    return console.log(`Something is wrong with delete! The type: ${type}, id:${id}`);
}
Delete.propTypes ={
    type: PropTypes.string,
    id: PropTypes.number,
    onSuccess: PropTypes.func.isRequired
}
export default Delete;