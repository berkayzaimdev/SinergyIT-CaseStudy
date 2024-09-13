import Header from "../components/Header";
import PaginationArea from "../components/Pagination";
import ProductList from "../components/ProductList";

const Home: React.FC = () => {
  return (
    <div className="home-container">
      <Header />
      <ProductList />
      <PaginationArea />
    </div>
  );
};

export default Home;
