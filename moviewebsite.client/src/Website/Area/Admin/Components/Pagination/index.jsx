import Pag from "react-bootstrap/Pagination";
import React, { useState } from "react";
import PropTypes from "prop-types";

function Pagination({ data, onPageChange, itemsPerPage }) {
  const [currentPage, setCurrentPage] = useState(1);
  const totalPages = Math.ceil(data.length / itemsPerPage);
  const pages = [...Array(totalPages + 1).keys()].slice(1);

  const handlePageChange = (page) => {
    setCurrentPage(page);
    onPageChange(page);
  };
  const firstPage = () => {
    handlePageChange(1);
  };
  const lastPage = () => {
    handlePageChange(totalPages);
  };
  const prevPage = () => {
    if (currentPage !== 1) {
      handlePageChange(currentPage - 1);
    }
  };
  const nextPage = () => {
    if (currentPage !== totalPages) {
      handlePageChange(currentPage + 1);
    }
  };
  return (
    <Pag>
      {currentPage !== 1 ? (
        <>
          <Pag.First
            onClick={firstPage}
            disabled={currentPage === 1}
            linkStyle={{ backgroundColor: "black" }}
          />
          <Pag.Prev
            onClick={prevPage}
            disabled={currentPage === 1}
            linkStyle={{ backgroundColor: "black" }}
          />
        </>
      ) : null}
      {pages.map((page, i) => (
        <Pag.Item
          key={i}
          active={page === currentPage}
          onClick={() => handlePageChange(page)}
          linkStyle={{ backgroundColor: "black" }}
        >
          {page}
        </Pag.Item>
      ))}
      {currentPage !== totalPages ? (
        <>
          <Pag.Next
            onClick={nextPage}
            linkStyle={{ backgroundColor: "black" }}
          />
          <Pag.Last
            onClick={lastPage}
            linkStyle={{ backgroundColor: "black" }}
          />
        </>
      ) : null}
    </Pag>
  );
}

Pagination.propTypes = {
  data: PropTypes.array,
  onPageChange: PropTypes.func.isRequired,
  itemsPerPage: PropTypes.number,
};

export default Pagination;
