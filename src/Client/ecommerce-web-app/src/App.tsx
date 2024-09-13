import { BrowserRouter, Route, Router, Routes } from "react-router-dom";
import "./App.css";
import "holderjs";
import BrandProductList from "./components/BrandProductList";
import ProductList from "./components/ProductList";
import Layout from "./pages/Layout";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={ProductList} />
          <Route path="/:brandId" Component={BrandProductList} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
