import { Link, Outlet } from "react-router-dom";
import { Layout, Menu } from "antd";

const { Header, Content, Footer } = Layout;

function LayoutPage() {
  return (
    <div>
      <Layout className="layout">
        <Header>
          <div className="logo" />
          <Menu theme="dark" mode="horizontal" defaultSelectedKeys={["1"]} >
            <Menu.Item key="1">
              <Link to="/" style={{ padding: "10px" }}>
                Home
              </Link>
            </Menu.Item>
            <Menu.Item key="2">
              <Link to="/book" style={{ padding: "10px" }}>
                Book
              </Link>
            </Menu.Item>
            <Menu.Item key="3">
                <Link to="/category" style={{ padding: "10px" }}>
                Category
                </Link>
            </Menu.Item>
          </Menu>
        </Header>
        <Content
          style={{
            padding: "0 50px",
          }}
        >
          <div className="site-layout-content">
            <h1>Welcome to Book Store!</h1>
            <Outlet />
          </div>
        </Content>
        <Footer
          style={{
            textAlign: "center",
          }}
        >
        </Footer>
      </Layout>
    </div>
  );
}
export default LayoutPage;