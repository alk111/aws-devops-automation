import React, { useEffect } from "react";
import { Button, Card, Col, Container, ListGroup, Row } from "react-bootstrap";
import toast from "react-hot-toast";
import { FaMinus, FaPlus, FaTrash } from "react-icons/fa";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { BASE_URL } from "../constants";
import {
  useDeleteCartQuantityMutation,
  useGetCartByUserIdQuery,
  useUpdateCartQuantityMutation,
} from "../slices/cartApiSlice";
import { addToCart, removeFromCart } from "../slices/cartSlice"; // Adjust the import path as needed
import { handleApiRequest } from "../utils/helper";
const Cart = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { userInformation } = useSelector((state) => state?.auth);
  const { cartItems } = useSelector((state) => state.cart);
  const user_id = userInformation && userInformation?.NameIdentifier;
  const { data, refetch, isSuccess } = useGetCartByUserIdQuery(user_id, {
    skip: !user_id, // Skip the query if user_id is falsy
  });
  const cartData = data && data?.data?.carts;
  const [updateQuantity] = useUpdateCartQuantityMutation();
  const [deleteCart] = useDeleteCartQuantityMutation();
  const updatedCart =
    cartData &&
    Object?.values(
      cartData?.reduce((acc, item) => {
        if (!acc[item?.product_id]) {
          acc[item?.product_id] = { ...item };
        } else {
          acc[item?.product_id].quantity += item?.quantity;
        }
        return acc;
      }, {})
    );
  const newCartData = updatedCart ?? cartItems;
  // Calculate total, GST, and discounts
  useEffect(() => {
    if (user_id && isSuccess) {
      refetch();
    }
  }, [refetch, user_id, isSuccess]);

  const itemsPrice =
    newCartData &&
    newCartData.reduce(
      (acc, item) => acc + (item?.price ?? item?.total_price) * item?.quantity,
      0
    );
  // const gst = itemsPrice * 0.18;
  // const discount = itemsPrice > 1000 ? 100 : 0;
  const total_price = itemsPrice;

  const handleRemoveFromCart = async (product_id) => {
    try {
      if (userInformation) {
        // Call API to delete cart item
        await handleApiRequest(() =>
          deleteCart({
            user_id,
            product_id: product_id.toString(),
          }).unwrap()
        );
        // Dispatch action to remove item from Redux store
        dispatch(removeFromCart(product_id));
      } else {
        // Directly dispatch action if user information is not present
        dispatch(removeFromCart(product_id));
      }

      // Ensure refetch only runs if the query was started
      if (isSuccess && typeof refetch === "function") {
        const updatedCartData = await refetch();
        if (updatedCartData?.error) {
          console.error("Error refetching cart data:", updatedCartData.error);
        }
      } else {
        console.warn("Cannot refetch as the query is not active.");
      }
    } catch (error) {
      toast.error(error?.message || "An error occurred.");
    }
  };

  const handleQuantityChange = async (item, quantity) => {
    const product_id = item.product_id;
    const updatedQuantity = Number(quantity);
    dispatch(addToCart({ ...item, quantity: updatedQuantity }));

    try {
      if (userInformation) {
        // Make the API request to update the quantity
        await handleApiRequest(() =>
          updateQuantity({
            user_id: user_id,
            product_id,
            entity_id: item?.entity_id,
            quantity: updatedQuantity,
          }).unwrap()
        );
      }

      // After successfully updating the backend, refetch the updated data
      refetch();
    } catch (error) {
      console.log("error", error);
      // Optionally revert the UI change if the API request fails
      dispatch(addToCart({ ...item, quantity: item.quantity }));
    }
  };

  const checkOutHandler = () => {
    navigate(`/shipping`);
  };

  return (
    <Container className="py-1">
      {newCartData?.length > 0 && (
        <Row>
          <h3 className="mb-2"> Cart</h3>
          <Col md={8}>
            <ListGroup variant="flush">
              {newCartData?.map((item) => (
                <ListGroup.Item
                  key={item?.product_id}
                  className="mb-2 pb-2 "
                  style={{ border: "1px solid #ddd", borderRadius: "8px" }}
                >
                  <Row className="align-items-center">
                    {/* Product Image */}
                    <Link to={`/product/${item?.product_id}`}>
                      <Col xs="auto" className="p-0">
                        <img
                          src={`${BASE_URL}/${item?.imageName}`}
                          alt={item?.productName}
                          onError={(e) =>
                            (e.target.src =
                              "https://plus.unsplash.com/premium_photo-1679517155620-8048e22078b1?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D")
                          }
                          className="img-fluid rounded"
                          style={{
                            width: "80px",
                            height: "80px",
                            objectFit: "cover",
                          }}
                        />
                      </Col>
                    </Link>

                    {/* Product Name and Remove Button on the Same Line */}
                    <Col className="ms-2">
                      <Row className="d-flex align-items-center  justify-content-end">
                        <Col>
                          <h5 className="mb-1">{item?.productName ?? ""}</h5>
                        </Col>
                        <Col xs="auto" className="p-1">
                          <Button
                            variant="outline-danger"
                            size="sm"
                            onClick={() =>
                              handleRemoveFromCart(
                                item?.product_id,
                                item?.entity_id
                              )
                            }
                            style={{ fontSize: "12px" }}
                          >
                            <FaTrash />
                          </Button>
                        </Col>
                      </Row>

                      {/* Price, Quantity Selector, and Total Price in Same Row */}
                      <Row className="d-flex align-items-center mt-2  justify-content-end">
                        {/* Item Price */}
                        <Col>
                          <span
                            className="mb-1 text-muted me-3"
                            style={{ fontSize: ".9em" }}
                          >
                            ₹{item?.price?.toFixed(2)}
                          </span>
                          {/* Added margin-right (me-3) to separate from buttons */}
                        </Col>

                        <Col
                          xs="auto"
                          className="p-1 d-flex align-items-center  justify-content-end"
                        >
                          {/* Quantity selector with minus button */}
                          <Button
                            variant="light"
                            size="sm"
                            onClick={() =>
                              handleQuantityChange(
                                item,
                                Math.max(1, item?.quantity - 1)
                              )
                            }
                            className="border-black btn-primary " // Added padding for buttons
                            style={{ fontSize: "12px" }}
                          >
                            <FaMinus />
                          </Button>
                          {/* Quantity display */}
                          <span className="mx-2">{item?.quantity}</span>{" "}
                          {/* Added margin-x (mx-2) to separate quantity from buttons */}
                          {/* Quantity selector with plus button */}
                          <Button
                            variant="light"
                            size="sm"
                            onClick={() =>
                              handleQuantityChange(item, item?.quantity + 1)
                            }
                            className="border-black btn-primary" // Added padding for buttons
                            style={{ fontSize: "12px" }}
                          >
                            <FaPlus />
                          </Button>
                          {/* Total price display */}
                          <span className="ms-3 ">
                            {/* Added margin-left (ms-3) for space from the quantity selector */}
                            ₹{(item?.price * item?.quantity).toFixed(2)}
                          </span>
                        </Col>
                      </Row>
                    </Col>
                  </Row>
                </ListGroup.Item>
              ))}
            </ListGroup>
          </Col>

          {/* Summary Section */}
          <Col md={4}>
            <Card>
              {/* <Card.Header className="bg-light text-white">
                Price Summary
              </Card.Header> */}
              <Card.Body>
                <ListGroup variant="flush">
                  {/* <ListGroup.Item>
                    <Row>
                      <Col>Subtotal:</Col>
                      <Col>₹{itemsPrice?.toFixed(2)}</Col>
                    </Row>
                  </ListGroup.Item> */}
                  {/* <ListGroup.Item>
                    <Row>
                      <Col>GST (18%):</Col>
                      <Col>₹{gst?.toFixed(2)}</Col>
                    </Row>
                  </ListGroup.Item> 
                  <ListGroup.Item>
                    <Row>
                      <Col>Discount:</Col>
                      <Col className="text-danger">
                        -₹{discount?.toFixed(2)}
                      </Col>
                    </Row>
                  </ListGroup.Item> */}
                  <ListGroup.Item className="d-flex align-items-center  justify-content-end">
                    <Row>
                      <Col>Total:</Col>
                      <Col>
                        <strong>₹{total_price?.toFixed(2)}</strong>
                      </Col>
                    </Row>
                  </ListGroup.Item>
                </ListGroup>
                {/* Checkout Button */}
                <Button
                  variant="success"
                  className="w-100 mt-3"
                  disabled={newCartData.length === 0}
                  onClick={() => checkOutHandler()}
                >
                  Place Order
                </Button>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      )}
    </Container>
  );
};

export default Cart;
