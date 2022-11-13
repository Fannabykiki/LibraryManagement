import axios from "axios";
import { useEffect, useState } from "react";
import { Space, Table, Tag } from "antd";
import { Button } from 'antd';
import { useNavigate } from "react-router-dom";


import React from "react";
const { Column, ColumnGroup } = Table;

function CategoryService() {
  const [categories, setCategories] = useState([]);
  const handleAdd = ()=> {
    navigate(`/addcategory`);
  };
  const navigate = useNavigate();

  const handleUpdate = (id) => {
    navigate(`/category/${id}`);
  };

  const handleDelete = (id) => {
    var checkingDelete = window.confirm(
      `Do you want to delete category with id: ${id}`
    );
    if (checkingDelete) {
      axios({
        method: "delete",
        url: `https://localhost:7233/api/category-management/categories/${id}`,
      })
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });

      window.location.reload();
    }
  };

  useEffect(() => {
    axios
      .get(`https://localhost:7233/api/category-management/categories`)
      .then((data) => {
        setCategories(data.data);
        console.log(data.data);
      })
      .catch((error) => console.log(error));
  }, []);
    
  return (
    <>
    <Button type="primary" onClick={handleAdd} >
        Add
      </Button>
    <Table dataSource={categories}>
      <Column title="CategoryId" dataIndex="categoryId" key="categoryId" />
      <Column title="CategoryName" dataIndex="categoryName" key="categoryName" />
      <Column
        title="Action"
        key="action"
        render={(_, record) => (
           
          <Space size="middle">
             <Button type="text" dashed>
             <button
                    class="updateButton"
                    onClick={() => handleUpdate(record.categoryId)}
                  >
                    Edit {record.firstName}
                  </button>
            </Button>
            <Button type="text" danger>
                <button
                    class="deleteButton"
                    value={record.bookId}
                    onClick={() => handleDelete(record.categoryId)}
                  >
                    Delete
                  </button>
            </Button>
          </Space>
        )}
      />
    </Table>
    </>
  );
}

export default CategoryService;
