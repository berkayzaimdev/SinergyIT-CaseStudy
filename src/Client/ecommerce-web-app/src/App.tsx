import { BrowserRouter, Route, Router, Routes } from "react-router-dom";
import "./App.css";
import "holderjs";
import ProductList from "./pages/ProductList";
import BrandProductList from "./pages/BrandProductList";
import Login from "./pages/Login";
import Register from "./pages/Register";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={ProductList} />
          <Route path="/:brandId" Component={BrandProductList} />
          <Route path="/login" Component={Login} />
          <Route path="/register" Component={Register} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
