import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from "./Pages/Home";
import Management from "./Pages/Management";
import NotFoundPage from "./Pages/error/NotFoundPage";
function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route index element={<Home/>}/>
                <Route path="/Home" element={<Home/>}/>
                <Route path="/Admin/Category-Management"  element={<Management type="Category"/>} />
                <Route path="/Admin/Film-Management"  element={<Management type="Film"/>} />
                <Route path='*' element={<NotFoundPage/>} />
            </Routes>
       </BrowserRouter>
    );
}

export default App;