import { useLayoutEffect, useState } from "react";
import { Container } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import CategoriesTab from "../components/CategoriesTab";
import LogoLoader from "../components/LogoLoader";
import { useLazyGetProductsByNameQuery } from "../slices/productsApiSlice";
import ProductListPage from "./SearchProductScreen";

export default function HomeScreen() {
  const { keyword } = useParams();
  const [products, setProducts] = useState([]);
  const [getProducts, { isLoading }] = useLazyGetProductsByNameQuery();
  const shippingAddress = useSelector((state) => state?.cart?.shippingAddress);
  useLayoutEffect(() => {
    if (!keyword) return;
    const fetchProducts = async () => {
      try {
        const { data } = await getProducts({
          search: keyword,
          latitude: shippingAddress?.latitude,
          longitude: shippingAddress?.longitude,
        }).unwrap();
        setProducts(data?.products || []);
      } catch (error) {
        console.error("Fetch error:", error);
      }
    };
    fetchProducts();
  }, [
    getProducts,
    shippingAddress?.latitude,
    shippingAddress?.longitude,
    keyword,
  ]);

  return (
    <Container>
      {!keyword ? <CategoriesTab /> : null}

      {isLoading ? (
        <LogoLoader />
      ) : (
        <>
          {keyword && products?.length > 0 && (
            <ProductListPage products={products} keyword={keyword} />
          )}
        </>
      )}
    </Container>
  );
}
