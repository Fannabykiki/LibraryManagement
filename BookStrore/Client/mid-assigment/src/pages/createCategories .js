import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../Components/Css/button.css";
import axios from "axios";


function CreateCategories() {

  const [category, setCategory] = useState({
    name: "",
  });

  const navigate = useNavigate();

  const handleChange = (evt) => {
    setCategory({
      ...category,
      [evt.target.name]: evt.target.value,
    });
  };

  const handleBackToList = () => {
    navigate(`/category`);
  };

  const handleOnSubmit = (evt) => {
    evt.preventDefault();
    axios({
      method: "post",
      url: "https://localhost:7233/api/category-management/categories",
      data: {
        categoryName :"",
      },
    })
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });

    setCategory({
      categoryName :"",
    });
    handleBackToList();
  };

  return (
    <div>
      <h1>Create category:</h1>
      { 
        <form onSubmit={handleOnSubmit}>
          <div>
            <label>
              Category name:
              <input
                type="text"
                onChange={handleChange}
                name="name"
                required
              ></input>
            </label>
          </div>
          <div>
            <button class="submitButton" type="submit">
              Submit
            </button>
            <button
              class="backButton"
              onClick={handleBackToList}
              variant="secondary"
              type="reset"
            >
              Back to Post Page
            </button>
          </div>
        </form>
      }
    </div>
  );
}

export default CreateCategories;
