import React, { useState, useEffect, useContext } from "react";
import PropTypes from "prop-types";
import WebApi from "../../../../WebApi";
import AdminContext from "../../AminContext/Context";

function LoadFilmCategories({ filmId }) {
  const adminContext = useContext(AdminContext);
  const { isCateUpdated, onFilmCateUpdate } = adminContext;
  const [filmCates, setFilmCates] = useState([]);
  useEffect(() => {
    if (filmId !== 0 || isCateUpdated) {
      loadFilmCatesData();
    }
  }, [filmId, isCateUpdated]);
  const loadFilmCatesData = async () => {
    const data = await WebApi.getFilmCates(filmId);
    setFilmCates(data);
    onFilmCateUpdate(false);
  };
  return filmCates?.map((cate, i) => (
    <p key={cate.id}>
      {cate.name}
      {filmCates.length === i + 1 ? "." : ","}
    </p>
  ));
}

LoadFilmCategories.propTypes = {
  filmId: PropTypes.number.isRequired,
};

export default LoadFilmCategories;
