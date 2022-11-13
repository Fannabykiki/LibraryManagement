// import axios from "axios";
// import { useEffect, useState } from "react";

// function BookService(){
//     const [categories, serCategories] = useState([]);

//     useEffect(() => {
//         axios
//           .get(`https://localhost:7233/api/book-management/books`)
//           .then((data) => {
//             serCategories(data.data);
//           })
//           .catch((error) => console.log(error));
//       }, []);
// }
// export default BookService;