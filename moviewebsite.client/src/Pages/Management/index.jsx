import PropTypes from 'prop-types';
import CategoryManagement from './CategoryManagement';
import NotFoundPage from '../error/NotFoundPage';

const Management = ({type})=> {
    return(type==="Category"? <CategoryManagement/>: <NotFoundPage/>);
}


Management.propTypes ={
    type: PropTypes.string
}
Management.defaultProps = {
    type: ""
}
export default Management;
