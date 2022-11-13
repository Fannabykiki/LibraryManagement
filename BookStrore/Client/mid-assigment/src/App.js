import './App.css';
import LayoutPages from './pages/layout';
 import { Routes, Route } from "react-router-dom"
import HomePage from './pages/home';
import BookService from './pages/book';
import UpdateBook from './pages/updatebook';
import CategoryService from './pages/category';

function App() {
  return (
      <div className="App">
        <Routes>
        <Route element={<LayoutPages/>}>
        <Route path="/" element={<HomePage/>}/>
        <Route path="/book" element={<BookService/>}/>
        <Route path="/update/:id" element={<UpdateBook/>}/>
        <Route path="/category" element={<CategoryService/>}/>

        </Route>
      </Routes>
      </div>
    );
}

export default App;
