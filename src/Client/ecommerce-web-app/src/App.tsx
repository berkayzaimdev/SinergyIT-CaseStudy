import { BrowserRouter, Route, Router, Routes } from "react-router-dom";
import "./App.css";
import "holderjs";
import ProductList from "./pages/ProductList";
import BrandProductList from "./pages/BrandProductList";

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
