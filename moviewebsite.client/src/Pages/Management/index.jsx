import PropTypes from 'prop-types';
import CategoryManagement from './CategoryManagement';
import NotFoundPage from '../error/NotFoundPage';
import FilmManagement from './FilmManagement';

const Management = ({type})=> {
    if(type==="Category"){
        return(<CategoryManagement/>)
    }
    else if(type==="Film"){
        return(<FilmManagement/>)
    }
    else{
        return(<NotFoundPage/>)
    }
}


Management.propTypes ={
    type: PropTypes.string
}
Management.defaultProps = {
    type: ""
}
export default Management;
