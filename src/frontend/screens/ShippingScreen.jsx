import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import FormContainer from "../components/FormContainer";

import {
  Button,
  Col,
  Form,
  FormCheck,
  FormControl,
  FormGroup,
  FormLabel,
  Row,
} from "react-bootstrap";
import {
  FaCity,
  FaGlobeAsia,
  FaHome,
  FaMailBulk,
  FaMapMarkerAlt,
} from "react-icons/fa";
import { indianStates } from "../constants";
import { saveShippingAddress } from "../slices/cartSlice";
import { useAddUserAddressMutation } from "../slices/usersApiSlice";
import { handleApiRequest } from "../utils/helper";
const ShippingScreen = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { shippingAddress } = useSelector((state) => state.cart);
  const { userInformation } = useSelector((state) => state.auth);
  const user_id = userInformation && userInformation?.NameIdentifier;

  // Array of Indian states

  // Initialize state with separated address components
  const [streetAddress, setStreetAddress] = useState(
    shippingAddress?.streetAddress || ""
  );
  const [district, setDistrict] = useState(shippingAddress?.district || "");
  const [state, setState] = useState(shippingAddress?.state || "");
  const [pincode, setPincode] = useState(shippingAddress?.pincode || "");
  const [building, setBuilding] = useState(shippingAddress?.building || "");
  const [landmark, setLandmark] = useState(shippingAddress?.landmark || "");
  const [addUserAddress, { isLoading }] = useAddUserAddressMutation();

  const [stateError, setStateError] = useState("");
  // Validate state whenever it changes
  useEffect(() => {
    if (state) {
      const stateExists = indianStates.some(
        (s) => s.toLowerCase() === state.toLowerCase()
      );
      if (stateExists) {
        // Find the correct case version of the state
        const correctCaseState = indianStates.find(
          (s) => s.toLowerCase() === state.toLowerCase()
        );
        setState(correctCaseState);
        setStateError("");
      } else {
        setStateError("Please enter a valid Indian state");
      }
    } else {
      setStateError("");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [state]);

  const submitHandler = async (e) => {
    e.preventDefault();
    const userAddress = {
      userID: user_id ?? "",
      aptStreet: streetAddress,
      district,
      state,
      country: shippingAddress?.country || "India",
      pinCode: +pincode,
      latitude: shippingAddress?.latitude,
      longitude: shippingAddress?.longitude,
    };
    const res = await handleApiRequest(addUserAddress(userAddress).unwrap());
    console.log("res", res);
    if (!res) {
      dispatch(
        saveShippingAddress({
          streetAddress,
          district,
          state,
          pincode,
          building,
          landmark,
        })
      );
    }

    const stateExists = indianStates.some(
      (s) => s.toLowerCase() === state.toLowerCase()
    );

    if (!stateExists) {
      setStateError("Please enter a valid Indian state");
      return;
    }
    try {
    } catch (error) {}

    navigate(`/placeorder`);
  };

  return (
    <FormContainer className="address-form-container">
      <h2 className="mb-4">Shipping Address</h2>

      <Form onSubmit={submitHandler} className="address-form">
        {/* Address Type Toggle */}

        {/* Address Fields */}
        <Row className="mb-3">
          <Col md={8}>
            <FormGroup controlId="streetAddress">
              <FormLabel>
                <FaMapMarkerAlt className="me-2" />
                Street Address*
              </FormLabel>
              <FormControl
                type="text"
                placeholder="House no., Street, Area"
                value={streetAddress}
                required
                onChange={(e) => setStreetAddress(e.target.value)}
                className="address-field"
              />
            </FormGroup>
          </Col>
          <Col md={4}>
            <FormGroup controlId="building">
              <FormLabel>
                <FaHome className="me-2" />
                Building/Apartment
              </FormLabel>
              <FormControl
                type="text"
                placeholder="Building name, Floor, Flat no."
                value={building}
                onChange={(e) => setBuilding(e.target.value)}
                className="address-field"
              />
            </FormGroup>
          </Col>
        </Row>

        <Row className="mb-3">
          <Col md={8}>
            <FormGroup controlId="district">
              <FormLabel>
                <FaCity className="me-2" />
                District/Taluka*
              </FormLabel>
              <FormControl
                type="text"
                placeholder="District, Taluka"
                value={district}
                required
                onChange={(e) => setDistrict(e.target.value)}
                className="address-field"
              />
            </FormGroup>
          </Col>
          <Col md={4}>
            <FormGroup controlId="landmark">
              <FormLabel>
                <FaMapMarkerAlt className="me-2" />
                Landmark
              </FormLabel>
              <FormControl
                type="text"
                placeholder="Nearby landmark"
                value={landmark}
                onChange={(e) => setLandmark(e.target.value)}
                className="address-field"
              />
            </FormGroup>
          </Col>
        </Row>

        <Row className="mb-4">
          <Col md={6}>
            <FormGroup controlId="state">
              <FormLabel>
                <FaGlobeAsia className="me-2" />
                State*
              </FormLabel>
              <FormControl
                type="text"
                list="indianStatesList"
                placeholder="State"
                value={state}
                required
                onChange={(e) => setState(e.target.value)}
                className={`address-field ${stateError ? "is-invalid" : ""}`}
              />
              <datalist id="indianStatesList">
                {indianStates.map((state, index) => (
                  <option key={index} value={state} />
                ))}
              </datalist>
              {stateError && (
                <div className="invalid-feedback">{stateError}</div>
              )}
            </FormGroup>
          </Col>
          <Col md={6}>
            <FormGroup controlId="pincode">
              <FormLabel>
                <FaMailBulk className="me-2" />
                Pincode*
              </FormLabel>
              <FormControl
                type="text"
                placeholder="6-digit pincode"
                value={pincode}
                required
                onChange={(e) => setPincode(e.target.value)}
                maxLength="6"
                pattern="[0-9]{6}"
                className="address-field"
              />
            </FormGroup>
          </Col>
        </Row>

        {/* Save as default checkbox */}
        <FormCheck
          type="checkbox"
          id="saveAsDefault"
          label="Save as default shipping address"
          className="mb-4"
        />

        <Button
          type="submit"
          variant="light"
          className="w-100 submit-btn"
          disabled={
            !streetAddress ||
            !district ||
            !state ||
            !pincode ||
            stateError ||
            isLoading
          }
        >
          Continue
        </Button>
      </Form>

      <style jsx>{`
        .address-form-container {
          max-width: 800px;
          margin: 0 auto;
          padding: 2rem;
          background: #fff;
          border-radius: 10px;
          box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        .address-form {
          margin-top: 1.5rem;
        }

        .address-type-btn {
          width: 50%;
          padding: 0.75rem;
          display: flex;
          align-items: center;
          justify-content: center;
        }

        .current-location-btn {
          padding: 0.75rem;
          display: flex;
          align-items: center;
          justify-content: center;
        }

        .address-field {
          padding: 0.75rem;
          border-radius: 5px;
        }

        .submit-btn {
          padding: 0.75rem;
          font-weight: 600;
          border-radius: 5px;
          border: 1px solid black;
          margin-bottom: 1rem;
        }
      `}</style>
    </FormContainer>
  );
};

export default ShippingScreen;
