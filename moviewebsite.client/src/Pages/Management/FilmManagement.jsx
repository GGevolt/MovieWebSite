import React, { useContext, useEffect, useState } from "react";
import { Button, Card, Container, Image, Spinner } from "react-bootstrap";
import { Link } from "react-router-dom";
import Footer from "../../Components/Footer";
import FilmCUForm from "../../Components/Form/FilmCUForm";
import NavBar from "../../Components/NavBar";
import Pagination from "../../Components/Pagination";
import PictureUpload from "../../Components/Upload/PictureUpload";
import Delete from "../../Components/Utility/Delete";
import LoadFilmCategories from "../../Components/Utility/LoadFilmCategories";
// import {getFilms} from "../../api/serverApi.jsx";
import AdminContext from "../../Context/AdminContext/Context";
import "./Management.css";

function FilmManagement() {
  const [isCateUpdated, setIsCateUpdated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 6;
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const adminContext = useContext(AdminContext);
  // const [films, setFilms] =useState([]);
  const { films, getFilms } = adminContext;
  const currentItems = films.slice(indexOfFirstItem, indexOfLastItem);

  useEffect(() => {
    refreshData();
  }, []);

  const hanldeCateUpdate = (updateStatus) => {
    setIsCateUpdated(updateStatus);
  };
  const handlePageChange = (page) => {
    setCurrentPage(page);
  };
  const refreshData = () => {
    getFilms();
    // loadFilmsData();
  };
  // const loadFilmsData=async ()=>{
  //     const filmsData = await getFilms();
  //     setFilms(filmsData);
  //     setIsLoading(false);
  // }

  const displayFilms = [
    <Container className="film-container">
      {currentItems.map((film) => (
        <Card className="film-card" key={film.id}>
          <Card.Title className="film-title">{film.title}</Card.Title>
          <div className="film-image">
            <Image
              src={`/images/${film.filmImg}`}
              thumbnail
              alt="Movie picture"
            />
          </div>
          <div className="film-info">
            <div className="film-info-director">
              <strong>Director:</strong> {film.director}
            </div>
            <div className="film-info-categories">
              <strong>Categories:</strong>{" "}
              <LoadFilmCategories
                filmId={film.id}
                isCateUpdated={isCateUpdated}
                onCateUpdate={hanldeCateUpdate}
              />
            </div>
            <div className="film-info-type">
              <strong>Type:</strong> {film.type}
            </div>
          </div>
          <div className="film-functions">
            <strong>Functionalities: </strong>
            <div className="film-functions-content">
              <PictureUpload id={film.id} onSuccess={refreshData} />
              <FilmCUForm
                film={film}
                onSuccess={refreshData}
                onCateUpdate={hanldeCateUpdate}
              />
              <Button
                variant="outline-success"
                as={Link}
                to={`/Admin/Video-Management/${film.id}`}
              >
                Manage Video
              </Button>
              <Delete type="film" id={film.id} onSuccess={refreshData} />
            </div>
          </div>
          <div className="film-synopsis">
            <h5>Synopsis</h5>
            {film.synopsis}
          </div>
        </Card>
      ))}
      <div className="Pag">
        <Pagination
          data={films}
          onPageChange={handlePageChange}
          itemsPerPage={itemsPerPage}
        />
      </div>
    </Container>,
  ];

  return (
    <div className="body">
      <NavBar />
      <Container fluid>
        <Container className="firstLine">
          <h1>Manage Films</h1>
          <FilmCUForm onSuccess={refreshData} />
        </Container>
        {isLoading ? (
          <Spinner animation="border" />
        ) : films.length > 0 ? (
          displayFilms
        ) : (
          <h4>No Movies found.</h4>
        )}
      </Container>
      <Footer />
    </div>
  );
}
export default FilmManagement;
