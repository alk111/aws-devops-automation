
# Frontend Proxy (NGINX Reverse Proxy)

This directory contains Kubernetes manifests to deploy an NGINX-based reverse proxy called `frontendproxy`, which forwards traffic to the React frontend application deployed in the cluster.

---

## 📦 Components Included

- `frontendproxy-configmap.yaml` — NGINX configuration to proxy requests
- `frontendproxy-deployment.yaml` — Deployment of NGINX pods
- `frontendproxy-service.yaml` — ClusterIP service for frontendproxy
- `frontendproxy-ingress.yaml` — Ingress resource to expose the app via domain

---

## 🌐 Traffic Flow Overview

```
User Browser
   ↓
DNS Resolution (frontend.yourdomain.com → Ingress Controller IP)
   ↓
Ingress Controller (e.g., NGINX Ingress)
   ↓
Kubernetes Ingress resource (frontendproxy-ingress)
   ↓
frontendproxy Service (ClusterIP)
   ↓
frontendproxy Pod (NGINX container)
   ↓
NGINX reverse proxies request to omapi-frontend-service
   ↓
React Frontend Pod serves the application
   ↓
Response follows same path back to User
```

---

## 🔁 Step-by-Step Traffic Flow

1. **User opens** `http://frontend.yourdomain.com` in the browser.
2. **DNS** resolves the domain to the Ingress controller's external IP.
3. **Ingress controller** matches the domain/path to `frontendproxy` service.
4. **frontendproxy** is an NGINX pod that reverse proxies the request to `omapi-frontend-service`.
5. **omapi-frontend-service** forwards it to the React frontend pods.
6. **React app responds** and the response flows back through the same chain.

---

## 🚀 How to Deploy

Apply the resources in the following order:

```bash
kubectl apply -f frontendproxy-configmap.yaml
kubectl apply -f frontendproxy-deployment.yaml
kubectl apply -f frontendproxy-service.yaml
kubectl apply -f frontendproxy-ingress.yaml
```

Make sure:
- Your Ingress controller is installed and running.
- DNS (e.g., `frontend.yourdomain.com`) points to the Ingress controller IP.

---

## 🌍 Example Domain Setup

In your DNS provider, create an A record:

```
frontend.yourdomain.com → <Ingress External IP>
```

---

## 🔐 Optional: Enable HTTPS

To secure your site with TLS, integrate with **cert-manager** and use Let's Encrypt:

- Add a `ClusterIssuer`
- Update the Ingress with TLS block

Let us know if you'd like these YAMLs pre-generated.

---

## 📁 Directory Structure

```
kubernetes/frontendproxy/
├── frontendproxy-configmap.yaml
├── frontendproxy-deployment.yaml
├── frontendproxy-ingress.yaml
└── frontendproxy-service.yaml
```

---

## 📌 Notes

- The NGINX pod does not serve static files; it purely acts as a gateway.
- You can scale or update the proxy logic without touching the frontend pods.

---

## 🛠️ Maintainer

- [@alk111](https://github.com/alk111)
