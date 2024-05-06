import NavBar from "../../Components/NavBar";
import Footer from "../../Footer";

function Home(){
    return (
        <div className='body'>
            <NavBar/>
            <h1>This is the Home Page</h1>
            <Footer/>
        </div>
    );
}
export default Home;