import Pag from "react-bootstrap/Pagination";
import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import styles from "./index.module.css";

function Pagination({ data, onPageChange, itemsPerPage }) {
  const [currentPage, setCurrentPage] = useState(1);
  const totalPages = Math.ceil(data.length / itemsPerPage);

  useEffect(() => {
    onPageChange(currentPage);
  }, [currentPage, onPageChange]);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const renderPageNumbers = () => {
    const pageNumbers = [];
    const maxPagesToShow = 5;
    let startPage = Math.max(1, currentPage - 2);
    let endPage = Math.min(totalPages, startPage + maxPagesToShow - 1);

    if (endPage - startPage + 1 < maxPagesToShow) {
      startPage = Math.max(1, endPage - maxPagesToShow + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      pageNumbers.push(
        <button
          key={i}
          onClick={() => handlePageChange(i)}
          className={`${styles.pageButton} ${
            currentPage === i ? styles.active : ""
          }`}
        >
          {i}
        </button>
      );
    }

    return pageNumbers;
  };

  return (
    <nav className={styles.pagination}>
      <button
        onClick={() => handlePageChange(1)}
        disabled={currentPage === 1}
        className={`${styles.pageButton} ${styles.navButton}`}
      >
        &laquo;
      </button>
      <button
        onClick={() => handlePageChange(currentPage - 1)}
        disabled={currentPage === 1}
        className={`${styles.pageButton} ${styles.navButton}`}
      >
        &lsaquo;
      </button>
      {renderPageNumbers()}
      <button
        onClick={() => handlePageChange(currentPage + 1)}
        disabled={currentPage === totalPages}
        className={`${styles.pageButton} ${styles.navButton}`}
      >
        &rsaquo;
      </button>
      <button
        onClick={() => handlePageChange(totalPages)}
        disabled={currentPage === totalPages}
        className={`${styles.pageButton} ${styles.navButton}`}
      >
        &raquo;
      </button>
    </nav>
  );
}

Pagination.propTypes = {
  data: PropTypes.array,
  onPageChange: PropTypes.func.isRequired,
  itemsPerPage: PropTypes.number,
};

export default Pagination;
