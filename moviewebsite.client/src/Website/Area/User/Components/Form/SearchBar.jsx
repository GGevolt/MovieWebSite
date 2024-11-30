import React, { useState, useEffect, useRef } from 'react';
import { Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Search } from 'lucide-react';
import styles from './SearchBar.module.css';

const SearchBar = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [showDropdown, setShowDropdown] = useState(false);
  const navigate = useNavigate();
  const dropdownRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setShowDropdown(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      if (searchQuery) {
        fetchSearchResults();
      } else {
        setSearchResults([]);
        setShowDropdown(false);
      }
    }, 300);

    return () => clearTimeout(delayDebounceFn);
  }, [searchQuery]);

  const fetchSearchResults = async () => {
    try {
      const response = await axios.get('/api/film/search', {
        params: { query: searchQuery, limit: 5 }
      });
      setSearchResults(response.data);
      setShowDropdown(true);
    } catch (error) {
      console.error('Error fetching search results:', error);
    }
  };

  const handleSearch = (e) => {
    if (e.key === 'Enter' && searchQuery.trim()) {
      e.preventDefault();
      navigate(`/user/search?query=${encodeURIComponent(searchQuery)}`);
      setShowDropdown(false);
    }
  };

  const handleResultClick = (filmId) => {
    navigate(`/user/detail/${filmId}`);
    setShowDropdown(false);
    setSearchQuery('');
  };

  return (
    <div className={styles.searchContainer}>
      <div className={styles.searchInputWrapper}>
        <Search className={styles.searchIcon} />
        <Form.Control
          type="search"
          placeholder="Search films..."
          className={styles.searchInput}
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          onKeyPress={handleSearch}
        />
      </div>
      {showDropdown && searchResults.length > 0 && (
        <div className={styles.dropdownMenu} ref={dropdownRef}>
          {searchResults.map((film) => (
            <div
              key={film.id}
              className={styles.dropdownItem}
              onClick={() => handleResultClick(film.id)}
            >
              <img
                src={`/api/images/${film.filmPath}`}
                alt={film.title}
                className={styles.filmThumbnail}
              />
              <span>{film.title}</span>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SearchBar;