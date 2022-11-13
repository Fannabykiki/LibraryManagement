import axios from "axios";
import { useEffect, useState } from "react";
import { Space, Table, Tag } from "antd";
import { Button } from 'antd';
import { useNavigate } from "react-router-dom";


import React from "react";
const { Column, ColumnGroup } = Table;


function CategoryService() {
  const [categories, setCategories] = useState([]);

  const navigate = useNavigate();

  const handleUpdate = (id) => {
    navigate(`/update/${id}`);
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
    <Table dataSource={categories}>
      <Column title="Category" dataIndex="categoryName" key="categoryName" />
      <Column
        title="Action"
        key="action"
        render={(_, record) => (
          <Space size="middle">
             <Button type="text" dashed>
             <button
                    class="updateButton"
                    onClick={() => handleUpdate(record.bookId)}
                  >
                    Edit {record.firstName}
                  </button>
            </Button>
            <Button type="text" danger>
                <button
                    class="deleteButton"
                    value={record.bookId}
                    onClick={() => handleDelete(record.bookId)}
                  >
                    Delete
                  </button>
            </Button>
          </Space>
        )}
      />
    </Table>
  );
}

export default CategoryService;
