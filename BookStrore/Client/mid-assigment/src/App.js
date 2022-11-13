import './App.css';
import LayoutPages from './pages/layout';
 import { Routes, Route } from "react-router-dom"
import HomePage from './pages/home';
import BookService from './pages/book';
import UpdateBook from './pages/updatebook';
import CategoryService from './pages/category';
import CreateBooks from './pages/createBooks';
import CreateCategories from './pages/createCategories ';
import UpdateCategory from './pages/updatecategory';

function App() {
  return (
      <div className="App">
        <Routes>
        <Route element={<LayoutPages/>}>
        <Route path="/" element={<HomePage/>}/>
        <Route path="/book" element={<BookService/>}/>
        <Route path="/update/:id" element={<UpdateBook/>}/>
        <Route path="/category" element={<CategoryService/>}/>
        <Route path="/add" element={<CreateBooks/>}/>
        <Route path="/addcategory" element={<CreateCategories/>}/>
        <Route path="/category/:id" element={<UpdateCategory/>}/>

        </Route>
      </Routes>
      </div>
    );
}

export default App;
