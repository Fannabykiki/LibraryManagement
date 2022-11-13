// import { useState } from "react";
// import axios from "axios";
// import { useNavigate } from "react-router-dom";
// import "../Components/Css/button.css";
// import { useEffect } from "react";

// function CreateBooks() {
//   let token = localStorage.getItem("access-token");
//   const decoded = jwt_decode(token);

//   const [book, SetBook] = useState({
//     title: "",
//     categoryId: "",
//   });
//   const [category, setCategory] = useState([]);

//   const navigate = useNavigate();

//   const handleChange = (evt) => {
//     SetBook({
//       ...book,
//       [evt.target.name]: evt.target.value,
//     });
//   };

//   const handleBackToList = () => {
//     navigate(`/book`);
//   };

//   useEffect(() => {
//     axios({
//       method: "GET",
//       url: `https://localhost:7233/api/category`,
//       data: null,
//     })
//       .then((data) => {
//         console.log(data.data);
//         setCategory(data.data);
//       })
//       .catch((err) => {
//         console.log(err);
//       });
//   }, []);

//   const handleOnSubmit = (evt) => {
//     evt.preventDefault();
//     axios({
//       method: "post",
//       url: "https://localhost:7233/api/book",
//       data: {
//         title: book.title,
//         categoryId: book.categoryId,
//       },
//     })
//       .then((response) => {
//         console.log(response);
//       })
//       .catch((error) => {
//         console.log(error);
//       });

//     SetBook({
//       title: "",
//       categoryId: "",
//     });
//     handleBackToList();
//   };

//   return (
//     <div>
//       <h1>Create Book:</h1>
//       {decoded[
//         "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
//       ] === "SuperAdmin" && (
//         <form onSubmit={handleOnSubmit}>
//           <div>
//             <label>
//               Title name:
//               <input
//                 type="text"
//                 onChange={handleChange}
//                 //defaultValue={data.title}
//                 name="title"
//                 required
//               ></input>
//             </label>
//           </div>

//           <div>
//             <label>
//               Category Id:
//               <select onChange={handleChange} name="categoryId">
//                 <option value="">Choose a category Id</option>
//                 {category.map((option) => (
//                   <option value={option.categoryId}>{option.categoryId}</option>
//                 ))}
//               </select>
//             </label>
//           </div>
//           <div>
//             <button class="submitButton" type="submit">
//               Submit
//             </button>
//             <button
//               class="backButton"
//               onClick={handleBackToList}
//               variant="secondary"
//               type="reset"
//             >
//               Back to Post Page
//             </button>
//           </div>
//         </form>
//       )}
//     </div>
//   );
// }

// export default CreateBooks;
