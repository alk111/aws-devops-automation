import { useEffect, useState } from "react";
import {
  Badge,
  Button,
  Col,
  Container,
  Nav,
  Navbar,
  Row,
} from "react-bootstrap";
import { FaShoppingCart, FaUser } from "react-icons/fa";
import { useDispatch, useSelector } from "react-redux";
import { LinkContainer } from "react-router-bootstrap";
import logo from "../assets/logo.png";
import { saveShippingAddress } from "../slices/cartSlice";
import {
  buildPayload,
  getAddressFromCoords,
  parseAddressComponents,
} from "../utils/helper";
import UserMenu from "./UserMenu";
export default function Header() {
  const { cartItems } = useSelector((state) => state.cart);
  const { userInformation } = useSelector((state) => state?.auth);
  const shippingAddress = useSelector((state) => state?.cart?.shippingAddress);
  const streetAddress = shippingAddress && shippingAddress?.streetAddress;
  const areaUpdated = shippingAddress && shippingAddress?.area;
  const [street, setStreet] = useState(shippingAddress?.streetAddress || "");
  // eslint-disable-next-line no-unused-vars
  const [area, setArea] = useState(shippingAddress?.area || "");
  const [district, setDistrict] = useState(shippingAddress?.district || "");
  const [state, setState] = useState(shippingAddress?.state || "");
  const [pincode, setPincode] = useState(shippingAddress?.pincode || "");
  const [country, setCountry] = useState("");
  const dispatch = useDispatch();

  const isShippingAddressValid = Object.entries(shippingAddress)?.some(
    (item) => item !== null || item !== undefined
  );
  // Using RTK Query hooks for address management

  // Geolocation handler with better cleanup and error handling;
  useEffect(() => {
    const isPayloadValid = (payload) =>
      Object.values(payload).every((val) => val != null && val !== "");

    const handleGeolocationSuccess = async (position) => {
      const { latitude, longitude } = position.coords;
      if (!latitude || !longitude) throw new Error("Invalid coordinates");

      const address = await getAddressFromCoords(latitude, longitude);
      const addressComponents = parseAddressComponents(address);
      if (addressComponents) {
        setCountry(addressComponents?.country);
        setStreet(addressComponents?.street);
        setDistrict(addressComponents?.district);
        setState(addressComponents?.state);
        setPincode(addressComponents?.pincode);
      }

      const { user } = buildPayload(
        latitude,
        longitude,
        area,
        district,
        state,
        country,
        pincode,
        street
      );

      if (isPayloadValid(user)) {
        dispatch(saveShippingAddress({ ...user }));
      }
    };

    const handleGeolocationError = (error) => {
      let message;
      switch (error.code) {
        case error.PERMISSION_DENIED:
          message =
            "Location permission denied. Enable it in browser settings.";
          break;
        case error.TIMEOUT:
          message = "Location request timed out. Check your internet.";
          break;
        default:
          message = "Could not get your location. Please enter it manually.";
      }
      alert(message);
    };

    const handleUseCurrentLocation = async () => {
      if (isShippingAddressValid) return;

      try {
        const position = await new Promise((resolve, reject) => {
          navigator.geolocation.getCurrentPosition(resolve, reject, {
            enableHighAccuracy: true,
            timeout: 10000,
            maximumAge: 60000,
          });
        });
        await handleGeolocationSuccess(position);
      } catch (error) {
        handleGeolocationError(error);
      }
    };

    const initGeolocation = () => {
      if (!navigator.geolocation) {
        if (window.isSecureContext) {
          const retryTimer = setTimeout(() => {
            navigator.geolocation
              ? handleUseCurrentLocation()
              : alert("Geolocation is not supported by this browser.");
          }, 500);
          return () => clearTimeout(retryTimer);
        } else {
          alert("Geolocation requires HTTPS. Please use a secure connection.");
        }
        return;
      }

      handleUseCurrentLocation();
    };

    initGeolocation();
  }, [
    street,
    district,
    state,
    country,
    pincode,
    dispatch,
    streetAddress,
    isShippingAddressValid,
    area,
    shippingAddress.latitude,
    shippingAddress.longitude,
    shippingAddress,
  ]);

  return (
    <header>
      <Navbar
        style={{
          padding: "0rem 0rem",
          position: "fixed",
          top: 0,
          right: 0,
          left: 0,
        }}
        expand="md"
        collapseOnSelect
        className="navbar-glassmorph"
      >
        <Container>
          <Row className="align-items-center w-100">
            {/* Logo and Address Section */}
            <Col xs={9} md={4} className="d-flex align-items-center">
              <LinkContainer to="/">
                <Navbar.Brand className="me-1">
                  <img src={logo} alt="suuq" height={28} width={56} />
                </Navbar.Brand>
              </LinkContainer>

              {areaUpdated || area ? (
                <div
                  className="address-container"
                  style={{
                    maxWidth: "200px",
                    overflow: "hidden",
                    whiteSpace: "nowrap",
                    textOverflow: "ellipsis",
                  }}
                  title={areaUpdated ?? area}
                >
                  <small className="">{areaUpdated ?? area}</small>
                </div>
              ) : (
                <p style={{ margin: "0" }}>No Address</p>
              )}
            </Col>

            {/* User Info Section */}
            {userInformation ? (
              <Col
                xs={3}
                md={8}
                className="d-flex align-items-center justify-content-end p-0"
              >
                <h6 className="text-nowrap ms-2 mb-0">
                  {userInformation?.Name}
                </h6>
              </Col>
            ) : (
              <Col
                xs={3}
                md={8}
                className="d-flex align-items-center justify-content-end p-0"
              >
                <LinkContainer to="/login">
                  <Button
                    variant="light"
                    size="sm"
                    className="border-black d-block d-md-none"
                  >
                    Sign In
                  </Button>
                </LinkContainer>
              </Col>
            )}
          </Row>

          {/* Navigation Collapse Section */}
          <Navbar.Collapse id="basic-navbar" className="collapse-class">
            <Nav className="ms-auto align-items-center">
              <LinkContainer to="/cart">
                <Nav.Link className="nav-link-cart d-none d-md-block">
                  <FaShoppingCart className="nav-icon" />
                  Cart
                  {cartItems?.length > 0 && (
                    <Badge pill bg="success" className="nav-icon-pill">
                      {cartItems?.length}
                    </Badge>
                  )}
                </Nav.Link>
              </LinkContainer>

              <div className="user-menu">
                {userInformation ? (
                  <UserMenu />
                ) : (
                  <LinkContainer to="/login">
                    <Nav.Link className="d-none d-md-block nav-link">
                      <FaUser className="nav-icon" />
                      Login
                    </Nav.Link>
                  </LinkContainer>
                )}
              </div>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </header>
  );
}
