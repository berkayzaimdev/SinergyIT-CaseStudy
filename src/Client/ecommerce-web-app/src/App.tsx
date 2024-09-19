import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import "holderjs";
import Login from "./pages/Login";
import Register from "./pages/Register";
import BrandPage from "./pages/BrandPage";
import ProductsPage from "./pages/ProductsPage";
import ShoppingCart from "./pages/ShoppingCart";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<ProductsPage />} />
        <Route path="/home" element={<ProductsPage />} />
        <Route path="/:brandId" element={<BrandPage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/mycart" element={<ShoppingCart />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
