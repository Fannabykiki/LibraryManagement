import axios from "axios";
import { useEffect, useState } from "react";
import { Input, Modal, Select, Space, Table } from "antd";
import { Button } from "antd";
import { useNavigate } from "react-router-dom";
import React from "react";
import Column from "antd/lib/table/Column";

function BookService() {
  const [book, setBook] = useState([]);

  const navigate = useNavigate();

  const handleUpdate = (id) => {
    navigate(`/update/${id}`);
  };
  const handleAdd = ()=> {
    navigate(`/add`);
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
        setBook(data.data);
        console.log(data.data);
      })
      .catch((error) => console.log(error));
  }, []);

  return (
    <>
       <Button type="primary" onClick={handleAdd} >
        Add
      </Button>

      <Table dataSource={book}>
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
    </>
  );
}

export default BookService;
