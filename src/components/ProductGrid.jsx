import React from "react";
import { Card, Button, Row, Col } from "react-bootstrap";
import { Link } from "react-router-dom";
import { FaCartPlus, FaCheck } from "react-icons/fa"; // Icons for cart actions
import { useDispatch, useSelector } from "react-redux";
import { addToCart, removeFromCart } from "../slices/cartSlice";
import { handleApiRequest } from "../utils/helper";
import {
  useAddToCartApiMutation,
  useDeleteCartQuantityMutation,
} from "../slices/cartApiSlice";
import toast from "react-hot-toast";
import { BASE_URL } from "../constants";

const ProductGrid = ({ product }) => {
  const dispatch = useDispatch();
  const cartItems = useSelector((state) => state.cart.cartItems);
  const [addToCartApi] = useAddToCartApiMutation();
  const [deleteCart] = useDeleteCartQuantityMutation();

  const isInCart = (productId) =>
    cartItems?.some((item) => item.product_id === productId);
  const { userInformation } = useSelector((state) => state?.auth);
  const user_id = userInformation && userInformation?.NameIdentifier;

  const toggleCartHandler = async (product) => {
    try {
      if (isInCart(product?.product_id)) {
        if (userInformation) {
          await handleApiRequest(() =>
            deleteCart({
              user_id,
              entity_id: product?.entity_id,
              product_id: product?.product_id.toString(),
            }).unwrap()
          );
          dispatch(removeFromCart(product.product_id));
        } else {
          dispatch(removeFromCart(product.product_id));
        }
      } else {
        if (userInformation) {
          let res = await handleApiRequest(() =>
            addToCartApi({
              user_id,
              entity_id: product?.entity_id,
              product_id: product?.product_id?.toString(),
              quantity: 1,
            })
          );
          if (res.error) {
            toast.error(res.error.message);
            return;
          } else {
            dispatch(addToCart({ ...product, quantity: 1 }));
          }
        } else {
          dispatch(addToCart({ ...product, quantity: 1 }));
        }
      }
    } catch (error) {
      console.log("error", error);
      toast.error(error?.message);
    }
  };

  if (!Array.isArray(product)) {
    return <p>No products available</p>;
  }

  return (
    <Row className="">
      {product?.map((product) => (
        <Col
          key={product?.product_id}
          xs={6} // Two items per row on extra-small screens
          sm={6} // Two items per row on small screens
          md={4} // Three items per row on medium screens
          lg={3} // Four items per row on large screens
          xl={3}
          className="mb-2"
        >
          <Link
            to={`/product/${product?.product_id}`}
            style={{ textDecoration: "none" }}
          >
            <Card
              className="shadow-sm product-card "
              style={{
                borderRadius: "4px",
                overflow: "hidden",
                position: "relative",
                cursor: "pointer",
                display: "flex",
                flexDirection: "column",
                margin: "0 auto",
                height: "100%",
              }}
            >
              {/* Product Image on Top */}
              <Card.Img
                variant="top"
                src={`${BASE_URL}/${product?.imageName}`}
                alt={product?.productName}
                style={{
                  objectFit: "cover",
                  height: "140px",
                  width: "100%",
                }}
                onError={(e) =>
                  (e.target.src =
                    "https://cdn.pixabay.com/photo/2017/09/10/10/01/background-2734972_1280.jpg")
                }
              />

              {/* Product Details */}
              <Card.Body className="d-flex flex-column">
                <Card.Title
                  style={{
                    fontSize: "0.9rem", // Adjusted font size
                    fontWeight: "bold",
                    overflow: "hidden",
                    textOverflow: "ellipsis",
                    whiteSpace: "nowrap",
                  }}
                >
                  {product?.productName}
                </Card.Title>

                {/* Price */}
                <div className="d-flex align-items-center gap-1">
                  <strong className="">₹{product?.price}</strong>
                  {/* {product?.unitofQuantity && product?.unitofMeasure && (
                    <small>
                      / {product?.unitofQuantity} {product?.unitofMeasure}
                    </small>
                  )} */}
                  {product?.unitofMeasure && (
                    <small>/ {product?.unitofMeasure}</small>
                  )}
                </div>
                {product?.establishment_name && (
                  <p
                    className="order_detail_p"
                    style={{
                      fontSize: "0.75rem", // Adjusted font size
                      overflow: "hidden",
                      textOverflow: "ellipsis",
                      whiteSpace: "nowrap",
                    }}
                  >
                    Shop : {product?.establishment_name}
                  </p>
                )}
                {product?.brand && <p>{product?.brand}</p>}

                {/* Add to Cart Button */}
                <Button
                  variant={
                    isInCart(product.product_id) ? "success" : "outline-success"
                  }
                  onClick={(e) => {
                    e.preventDefault();
                    toggleCartHandler(product);
                  }}
                  style={{
                    fontSize: "0.8rem", // Adjusted button size
                    borderColor: "#28a745",
                    color: isInCart(product.product_id) ? "#fff" : "#28a745",
                    backgroundColor: isInCart(product.product_id)
                      ? "#28a745"
                      : "rgba(211, 211, 211, 0.5)",
                    padding: "0.25rem 0.75rem",
                    marginTop: "auto",
                    alignSelf: "flex-end",
                    display: "flex",
                    alignItems: "center",
                  }}
                >
                  {isInCart(product.product_id) ? (
                    <FaCheck size={16} />
                  ) : (
                    <FaCartPlus size={16} />
                  )}
                </Button>
              </Card.Body>
            </Card>
          </Link>
        </Col>
      ))}
    </Row>
  );
};

export default ProductGrid;
