import { useState, useEffect, useContext, useCallback } from "react";
import { Container, Row, Col, Form, Button, Alert } from "react-bootstrap";
import { useNavigate, useLocation } from "react-router-dom";
import axios from "axios";
import Select from 'react-select';
import FilmImg from "../../Components/Img/FilmImg";
import WebContext from "../../../../WebContext/Context";
import styles from "./SearchFilms.module.css";

export default function SearchFilms() {
  const webContext = useContext(WebContext);
  const { categories, getCategories } = webContext;
  const [query, setQuery] = useState("");
  const [director, setDirector] = useState("");
  const [results, setResults] = useState([]);
  const [selectedOptions, setSelectedOptions] = useState([]);
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const location = useLocation();

  const parseSearchParams = useCallback(() => {
    const searchParams = new URLSearchParams(location.search);
    const paramQuery = searchParams.get("query") || "";
    const paramDirector = searchParams.get("director") || "";
    const paramCategories = searchParams.getAll('categories');
    const paramType = searchParams.get('type');

    setQuery(paramQuery);
    setDirector(paramDirector);
    setSelectedOptions([
      ...(paramType ? [{ value: paramType, label: paramType, group: 'type' }] : []),
      ...paramCategories.map(id => ({ 
        value: id, 
        label: categories.find(c => c.id.toString() === id)?.name || id,
        group: 'category'
      }))
    ]);

    return { paramQuery, paramDirector, paramCategories, paramType };
  }, [categories, location.search]);

  useEffect(() => {
    if (!(Array.isArray(categories) && categories.length > 0)) {
      getCategories();
    }
  }, [categories, getCategories]);

  useEffect(() => {
    const { paramQuery, paramDirector, paramCategories, paramType } = parseSearchParams();
    if (paramQuery || paramCategories.length > 0 || paramType || paramDirector) {
      searchFilms(paramQuery, paramDirector, paramCategories, paramType);
    }
  }, [location.search, parseSearchParams]);

  const searchFilms = async (searchQuery, searchDirector, searchCategories, searchType) => {
    try {
      setError("");
      const response = await axios.get("/api/film/search", {
        params: {
          query: searchQuery || undefined,
          categories: searchCategories.join(",") || undefined,
          type: searchType || undefined,
          director: searchDirector || undefined,
        },
      });
      setResults(response.data);
    } catch (error) {
      console.error("Error searching films:", error);
      setError("An error occurred while searching for films. Please try again.");
      setResults([]);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    updateSearchParams();
  };

  const updateSearchParams = () => {
    const params = new URLSearchParams();
    if (query) params.set("query", query);
    selectedOptions.forEach((option) => {
      if (option.group === 'category') {
        params.append("categories", option.value);
      } else if (option.group === 'type') {
        params.set("type", option.value);
      }
    });
    if (director) params.set("director", director);
    navigate(`/user/search?${params.toString()}`, { replace: true });
  };

  const typeOptions = [
    { value: "Movie", label: "Movie", group: 'type' },
    { value: "TV-Series", label: "TV-Series", group: 'type' },
  ];

  const categoryOptions = categories.map(category => ({
    value: category.id.toString(),
    label: category.name,
    group: 'category'
  }));

  const groupedOptions = [
    {
      label: 'Type',
      options: typeOptions
    },
    {
      label: 'Categories',
      options: categoryOptions
    }
  ];

  return (
    <Container fluid className={styles.searchContainer}>
      {error && <Alert variant="danger">{error}</Alert>}
      <Form onSubmit={handleSubmit} className={styles.searchForm}>
        <Row>
          <Col md={6}>
            <Form.Group className="mb-3">
              <Form.Label>Search Query</Form.Label>
              <Form.Control
                type="text"
                value={query}
                onChange={(e) => setQuery(e.target.value)}
                placeholder="Enter film title"
                className={styles.formControl}
              />
            </Form.Group>
          </Col>
          <Col md={6}>
            <Form.Group className="mb-3">
              <Form.Label>Director</Form.Label>
              <Form.Control
                type="text"
                value={director}
                onChange={(e) => setDirector(e.target.value)}
                placeholder="Enter director name"
                className={styles.formControl}
              />
            </Form.Group>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form.Group className="mb-3">
              <Form.Label>Film Type and Categories</Form.Label>
              <Select
                isMulti
                value={selectedOptions}
                onChange={setSelectedOptions}
                options={groupedOptions}
                className={styles.reactSelect}
                classNamePrefix="react-select"
                placeholder="Select type and categories..."
              />
            </Form.Group>
          </Col>
        </Row>
        <Button variant="primary" type="submit" className={styles.searchButton}>
          Search
        </Button>
      </Form>
      <div className={styles.resultsContainer}>
        <Row className={styles.filmRow}>
          {results.map((film) => (
            <Col key={film.id} xs={6} sm={4} md={3} lg={2} className="mb-4 px-2">
              <div 
                className={styles.filmCardWrapper}
                onClick={() => navigate(`/user/detail/${film.id}`)}
                role="button"
                tabIndex={0}
              >
                <div className={styles.tvSeriesCard}>
                  <FilmImg
                    src={`/api/images/${film.filmPath}`}
                    hash={film.blurHash}
                  />
                  <div className={styles.filmTitle}>{film.title}</div>
                </div>
              </div>
            </Col>
          ))}
        </Row>
      </div>
    </Container>
  );
}