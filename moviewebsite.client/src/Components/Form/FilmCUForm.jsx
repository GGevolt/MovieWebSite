import {Form, Container, FloatingLabel, Button, Col, Row} from 'react-bootstrap';
import axios from 'axios';
import React, { useEffect, useState } from 'react';
import './Form.css';
import Popup from 'reactjs-popup';
import { XCircleFill } from 'react-bootstrap-icons';
import PropTypes from 'prop-types';
import Select from 'react-select'
import makeAnimated from 'react-select/animated';
import { object, string, array } from 'yup';

function FilmCUForm (props) {
    const [categories, setCategories] = useState([]);
    const [selectedCategories, setSelectedCategories] = useState([]);
    const categoryOption = categories.map((category)=>({
        value: category.id,
        label: category.name
    }))
    const [film,setFilm] = useState({
        id: props.film? props.film.id : 0,
        title: props.film? props.film.title : '',
        synopsis: props.film? props.film.synopsis : '',
        director: props.film? props.film.director : '',
        filmImg : props.film? props.film.filmImg : ''
    });
    const [openForm, setOpen] = useState(false);
    const animatedComponents = makeAnimated();
    const [errors,setErrors] = useState([]);
    const validateSchema = object({
        film: object({
            title : string().required("Title field is empty, Pls input to proceed!").max(150,"The title is too long!"),
            synopsis: string().required("Synopsis field is empty, Pls input to proceed!"),
            director: string().required("Director field is empty, Pls input to proceed!").max(100,"The name is too long!"),
        }),
        selectedCategories: array().min(1,"Select atleast 1 category!").required("Select atleast 1 category!")
    });
    useEffect(() => {
        axios.get('/category')
            .then(response => {
                setCategories(response.data);
            })
            .catch(error => {
                console.error('There has been a problem with category get operation:', error);
            });
        if (film.id!==0) {
            axios.get(`/film/${film.id}`)
                .then(response => {
                    let cateIds = response.data;
                    setSelectedCategories(categoryOption.filter(option => cateIds.includes(option.value)));
                })
                .catch(error => {
                    console.error('There has been a problem with get film categories operation:', error);
                });
        }
    }, [film, film.id]);

    const handleChange=(event)=>{
        setFilm( prev=> ({...prev, [event.target.name]: event.target.value}));
    }

    const resetForm = () => {
        setFilm({
            id: props.film? props.film.id : 0,
            title: props.film? props.film.title : '',
            synopsis: props.film? props.film.synopsis : '',
            director: props.film? props.film.director : '',
            filmImg : props.film? props.film.filmImg : ''
        });
        setSelectedCategories(null);
        setErrors({});
    };

    const closeForm = () => {
        setOpen(false);
        resetForm();
    };

    const handleSubmit =async(event) => {
        event.preventDefault();
        const filmVM = {
            film: film,
            selectedCategories: selectedCategories && selectedCategories.map(category => category.value),
          };
        try{
            await validateSchema.validate(filmVM,{ abortEarly: false })
            setErrors({});
        }catch(error){
            const newErrors = {};
            error.inner.forEach(err => {
                newErrors[err.path] = err.message;
            });
            setErrors(newErrors);
            return;
        }
        axios.post('/film', filmVM, {
            headers: {
                'Content-Type': 'application/json'
              }
        })
        .then(() => {
            props.onSuccess();
            closeForm();
        })
        .catch(error => {
            console.error('There has been a problem with the film submission:', error);
        });
    };


    return (
        <>
        <Button variant="outline-success" onClick={() => setOpen(o => !o)}>{film.id === 0 ? "Create New Film" : "Update"}</Button>
        <Popup open={openForm} closeOnDocumentClick onClose={closeForm} className="form-popup">
            <Container >
                <XCircleFill className="close" onClick={closeForm}/>
                <h3>{film.id === 0 ? "Create" : "Update"} a Movie</h3>
                <Form onSubmit={handleSubmit} encType="multipart/form-data">
                    <Form.Control type="hidden" value={film.id} onChange={handleChange} name="id" />
                    <Row className="mb-3">
                        <Form.Group as={Col}>
                            <FloatingLabel controlId="floating-Title" label="Title" className="custom-label">
                                <Form.Control type="text" value={film.title} onChange={handleChange} placeholder='' name="title" />
                                { errors['film.title'] && <Form.Text className='error' >{errors['film.title']}</Form.Text>}
                            </FloatingLabel>
                        </Form.Group>
                        <Form.Group as={Col}>
                            <FloatingLabel controlId="floating-Director" label="Director" className="custom-label">
                                <Form.Control type="text" value={film.director} onChange={handleChange} placeholder='' name="director" />  
                                {errors['film.director'] && <Form.Text className='error' >{errors['film.director']}</Form.Text>} 
                            </FloatingLabel>
                        </Form.Group>
                    </Row>
                    <Form.Group className="mb-3">
                        <FloatingLabel controlId="floating-Synopsis" label="Synopsis" className="custom-label">
                            <Form.Control as="textarea" value={film.synopsis} onChange={handleChange} placeholder='' name="synopsis" />  
                            {errors['film.synopsis'] && <Form.Text className='error' >{errors['film.synopsis']}</Form.Text>} 
                        </FloatingLabel>
                    </Form.Group>
                    <Form.Group className="mb-3">
                    <Select
                        options={categoryOption}
                        components={animatedComponents}
                        placeholder="Select Categories for the film"
                        isMulti
                        onChange={event=>setSelectedCategories(event)}
                        value={selectedCategories}
                    />
                    {errors.selectedCategories && <Form.Text className='error'>{errors.selectedCategories}</Form.Text>} 
                    </Form.Group>
                    <Button type="submit" className='button'>{film.id === 0 ? "Create" : "Update"}</Button>
                </Form>
            </Container>
        </Popup>
        </>
    );
}

FilmCUForm.propTypes ={
    film : PropTypes.shape({
        id: PropTypes.number,
        title: PropTypes.string,
        synopsis: PropTypes.string,
        director: PropTypes.string,
       filmImg: PropTypes.string 
    }),
    onSuccess: PropTypes.func
}

export default FilmCUForm;
