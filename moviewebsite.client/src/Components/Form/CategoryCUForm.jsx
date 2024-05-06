import Form from 'react-bootstrap/Form';
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Popup from 'reactjs-popup';
import { XCircleFill } from 'react-bootstrap-icons';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import './CategoryCUForm.css';
import FloatingLabel from 'react-bootstrap/FloatingLabel';
import PropTypes from 'prop-types';


function CategoryCUForm({ category, onSuccess}){
    const [name, setName] = useState(category? category.name : '');
    const [id, setId] = useState(category? category.id : 0);
    const [openForm, setOpen] = useState(false);
    const resetForm = () => {
        setName(category ? category.name : '');
        setId(category ? category.id : 0);
    };
    const closeForm = () => {
        setOpen(false);
        resetForm();
    };
    useEffect(() => {
        resetForm();
    }, [category]);
    
    const handleSubmit = (event) => {
        event.preventDefault();
        const formData = { name, id };
        if (id === 0) {
            axios.post('/category', formData)
                .then(() => {
                    onSuccess();
                    closeForm();
                })
                .catch(error => {
                    console.error('There has been a problem with category create operation:', error);
                });
        } else {
            axios.post('/categorymanagement', formData)
                .then(() => {
                    onSuccess();
                    closeForm();
                })
               .catch(error => {
                console.error('There has been a problem with category update operation:', error);
            });
        }
    };

    return (
        <>
        <Button variant="outline-success" onClick={() => setOpen(o => !o)}>{id === 0 ? "Create category" : "Update"}</Button>
        <Popup open={openForm} closeOnDocumentClick onClose={closeForm}>
            <Container>
                <XCircleFill className="close" onClick={closeForm}/>
                <h3>{id === 0 ? "Create" : "Update"} a category</h3>
                <Form onSubmit={handleSubmit} encType="multipart/form-data">
                    <input type="hidden" value={id} onChange={e => setId(e.target.value)} />
                    <Form.Group className="mb-3">
                        <FloatingLabel controlId="floatingName" label="Name" className="custom-label">
                            <Form.Control type="text" value={name} onChange={e => setName(e.target.value)} placeholder='' />  
                        </FloatingLabel>
                    </Form.Group>
                    <Button type="submit" className='button'>{id === 0 ? "Create" : "Update"}</Button>
                </Form>
            </Container>
        </Popup>
        </>
    );
}

CategoryCUForm.propTypes ={
    category : PropTypes.shape({
        id: PropTypes.number,
        name: PropTypes.string
    }),
    onSuccess: PropTypes.func.isRequired
}
export default CategoryCUForm;