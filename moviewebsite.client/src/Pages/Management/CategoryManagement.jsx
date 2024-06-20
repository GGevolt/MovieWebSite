import React, { useEffect, useState } from "react";
import Container from "react-bootstrap/Container";
import Table from "react-bootstrap/Table";
import Spinner from "react-bootstrap/Spinner";
import CategoryCUForm from "../../Components/Form/CategoryCUForm";
import NavBar from "../../Components/NavBar";
import Footer from "../../Components/Footer";
import Pagination from "../../Components/Pagination";
import Delete from "../../Components/Utility/Delete";
// import { getCategories } from "../../api/serverApi.jsx";
import AdminContext from "../../Context/AdminContext/Context";
import "./Management.css";

function CategoryQualityManagement() {
  const adminContext = useContext(AdminContext);
  const [categories, getCategories] = adminContext;
  const [isCateLoading, setIsCateLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 6;
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = categories.slice(indexOfFirstItem, indexOfLastItem);

  useEffect(() => {
    refreshCategoryData();
  }, []);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };
  const refreshCategoryData = async () => {
    getCategories();
    // const cateData = await getCategories();
    // setCategories(cateData);
    setIsCateLoading(false);
  };

  const categotyTable = [
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
                <CategoryCUForm
                  category={category}
                  onSuccess={refreshCategoryData}
                />
                <Delete
                  type="category"
                  onSuccess={refreshCategoryData}
                  id={category.id}
                />
              </div>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>,
  ];

  return (
    <div className="body">
      <NavBar />
      <Container fluid className="cate-container">
        <Container className="firstLine">
          <h1>Manage Category</h1>
          <CategoryCUForm onSuccess={refreshCategoryData} />
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
      <Footer />
    </div>
  );
}
export default CategoryQualityManagement;
