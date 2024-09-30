import {useEffect, useContext} from "react";
import { Container} from "react-bootstrap"
import FilmRow from "../../Components/DisplayFilm/FilmRow";
import WebContext from "../../../../WebContext/Context";
import CarouselDisplay from "../../Components/DisplayFilm/CarouselDisplay";

function Home() {
  document.title = "Home";
  const webContext = useContext(WebContext);
  const { getFilms, films } = webContext;
  useEffect(()=>{
    getFilms();
  },[])
  const splitFilmsByType = (films) => {
    return films.reduce((acc, film) => {
      acc[film.type === 'TV-Series' ? 'tvSeries' : 'movies'].push(film);
      return acc;
    }, { tvSeries: [], movies: [] });
  };
  const { tvSeries, movies } = splitFilmsByType(films);
  const randomFilms = films.sort(() => Math.random() - 0.5).slice(0, 5);
  return (
    <Container fluid className="p-0">
      <CarouselDisplay films={randomFilms}/>
      <FilmRow films={movies} filmsRowTitle="Movies"/>
      <FilmRow films={tvSeries} filmsRowTitle="TV-Series"/>
    </Container>
  )
}
export default Home;