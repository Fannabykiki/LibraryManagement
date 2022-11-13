import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { Select } from "antd";
import { Option } from "antd/lib/mentions";

function CreateBooks() {

  const [category, setCategory] = useState([]);

  const navigate = useNavigate();

  const [book, setBook] = useState({
    bookName: "",
    categoryIds: [],
  });

  const handleChange2 = (value) => {
    setBook({
      ...book,
      categoryIds: value,
    });
  };

  const handleBackToList = () => {
    navigate(`/book`);
  };

  useEffect(() => {
    axios({
      method: "GET",
      url: `https://localhost:7233/api/book-management/books`,
      data: null,
    })
      .then((data) => {
        console.log(data.data);
        setCategory(data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  useEffect(() => {
    axios({
      method: "GET",
      url: `https://localhost:7233/api/category-management/categories`,
      data: null,
    })
      .then((data) => {
        setCategory(data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  const handleOnSubmit = (evt) => {
    evt.preventDefault();
    axios({
      method: "post",
      url: "https://localhost:7233/api/book-management/books",
      data: {
        bookName : book.name,
        categoryIds: book.categoryIds,
      },
    })
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });

    setBook({
      bookName: "",
      categoryId: "",
    });
    handleBackToList();
  };
  const handleChange = (event) => {
    setBook({
      ...book,
      [event.target.name]: event.target.value,
    });
  };
  return (
    <div>
      <h1>Create Book:</h1>
        <form onSubmit={handleOnSubmit}>
          <div>
            <label>
              Book name:
              <input
                type="text"
                onChange={handleChange}
                name="name"
                required
              ></input>
            </label>
          </div>

          <div>
            <label>
              Category Id:
              <Select name="categoryIds"
              mode="multiple"
              style={{
                width: "100%",
              }}
              placeholder="select category"
              onChange={handleChange2}
              optionLabelProp="label"
            >
              {category.map((option) => (<Option value={option.categoryId} label={option.categoryName}>
                <div className="demo-option-label-item">
                  {option.categoryName}
                </div>
              </Option>))}
            </Select>
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
      
    </div>
  );
}

export default CreateBooks;
