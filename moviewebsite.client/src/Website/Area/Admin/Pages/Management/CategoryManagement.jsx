import { useEffect, useState, useContext } from "react";
import { Container, Table, Spinner, Card } from "react-bootstrap";
import CategoryCUForm from "../../Components/Form/CategoryCUForm";
import Pagination from "../../Components/Pagination";
import DeleteButton from "../../Components/Utility/DeleteButton";
import WebContext from "../../../../WebContext/Context";
import styles from "./CategoryManagement.module.css";

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

  const categoryTable = (
    <div className={styles.tableResponsive}>
      <Table className={styles.categoryTable}>
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
                <div className={styles.functions}>
                  <CategoryCUForm category={category}/>
                  <DeleteButton type="category" id={category.id}/>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );

  return (
    <div className={styles.categoryManagement}>
      <Container fluid>
        <Card className={styles.contentCard}>
          <Card.Body>
            <div className={styles.header}>
              <h1 className={styles.title}>Manage Categories</h1>
              <CategoryCUForm/>
            </div>
            {isCateLoading ? (
              <div className={styles.spinnerContainer}>
                <Spinner animation="border" />
              </div>
            ) : categories.length > 0 ? (
              <>
                {categoryTable}
                <div className={styles.pagination}>
                  <Pagination
                    data={categories}
                    onPageChange={handlePageChange}
                    itemsPerPage={itemsPerPage}
                  />
                </div>
              </>
            ) : (
              <h4 className={styles.noCategories}>No categories found.</h4>
            )}
          </Card.Body>
        </Card>
      </Container>
    </div>
  );
}

export default CategoryManagement;