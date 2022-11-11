import './App.css';
import { Routes, Route } from "react-router-dom"
import LoginPages from './pages/login'; 
function App() {
  return (
    <div className="App">

      <LoginPages></LoginPages>
      {/* <Routes>
        <Route path="login" element={ <LoginPages/> } />
      </Routes> */}
      
    </div>
  );
}

export default App;
