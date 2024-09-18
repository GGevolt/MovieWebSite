import React, { useEffect, useState, useContext } from "react";
import Container from "react-bootstrap/Container";
import Table from "react-bootstrap/Table";
import Spinner from "react-bootstrap/Spinner";
import CategoryCUForm from "../../Components/Form/CategoryCUForm";
import Pagination from "../../Components/Pagination";
import DeleteButton from "../../Components/Utility/DeleteButton";
import WebContext from "../../../../WebContext/Context";
import "./Management.css";

function CategoryManagement() {
  const webContext = useContext(WebContext);
  const { categories, getCategories } = webContext;
  const [isCateLoading, setIsCateLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 6;
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = categories
    ? categories.slice(indexOfFirstItem, indexOfLastItem)
    : 0;

  useEffect(() => {
    loadCategoryData();
  }, []);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };
  const loadCategoryData = async () => {
    await getCategories();
    setIsCateLoading(false);
  };

  const categotyTable = (
    <Table striped bordered responsive hover variant="dark">
      <thead>
        <tr>
          <th>Category</th>
          <th>Functions</th>
        </tr>
      </thead>
      <tbody>
        {currentItems.map((category) => (
          <tr key={category.id}>
            <td>{category.name}</td>
            <td>
              <div className="func">
                <CategoryCUForm category={category} />
                <DeleteButton type="category" id={category.id} />
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );

  return (
    <div className="body">
      <Container fluid className="cate-container">
        <Container className="firstLine">
          <h1>Manage Category</h1>
          <CategoryCUForm />
        </Container>
        {isCateLoading ? (
          <Spinner animation="border" />
        ) : categories.length > 0 ? (
          <>
            {categotyTable}
            <div className="Pag">
              <Pagination
                data={categories}
                onPageChange={handlePageChange}
                itemsPerPage={itemsPerPage}
              />
            </div>
          </>
        ) : (
          <h4>No categories found.</h4>
        )}
      </Container>
    </div>
  );
}
export default CategoryManagement;
