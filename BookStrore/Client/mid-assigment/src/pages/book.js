import axios from "axios";
import { useEffect, useState } from "react";
import { Space, Table, Tag } from "antd";
import { Button } from 'antd';
import { useNavigate } from "react-router-dom";


import React from "react";
const { Column, ColumnGroup } = Table;


function BookService() {
  const [books, setBooks] = useState([]);

  const navigate = useNavigate();

  const handleUpdate = (id) => {
    navigate(`/update/${id}`);
  };

  const handleDelete = (id) => {
    var checkingDelete = window.confirm(
      `Do you want to delete book with id: ${id}`
    );
    if (checkingDelete) {
      axios({
        method: "delete",
        url: `https://localhost:7233/api/book-management/books/${id}`,
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
      .get(`https://localhost:7233/api/book-management/books`)
      .then((data) => {
        setBooks(data.data);
        console.log(data.data);
      })
      .catch((error) => console.log(error));
  }, []);
  
  return (
    <Table dataSource={books}>
      <Column title="BookId" dataIndex="bookId" key="bookId" />
      <Column title="BookName" dataIndex="bookName" key="bookName" />
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

export default BookService;
