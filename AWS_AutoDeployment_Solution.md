# Project: Automatic Deployment of Web App with Database & NGINX Proxy on AWS

### Task Overview

1. Containerize an application with database and NGINX proxy and deploy to AWS.
2. Enable scalability—able to spin up multiple instances.
3. Automate the full deployment pipeline from code push to production deployment.

---

### Architecture Components
- **App container** (React frontend + .NET backend)
- **Database**: MSSQL managed via AWS RDS
- **Reverse proxy**: NGINX deployed in Kubernetes
- **Orchestration**: EKS (AWS-managed Kubernetes)
- **Load balancer & auto-scaling**: via Kubernetes Service + AWS NLB/ALB + Cluster Autoscaler
- **CI/CD pipeline**: GitHub Actions → ECR (Docker registry) → EKS (kubectl apply or ArgoCD)

---

### 📜 Implementation Steps
1. **Containerization**
   - Built Docker images for backend, frontend, nginx proxy using provided Dockerfiles.
   - Pushed images to AWS ECR (Elastic Container Registry).

2. **Infrastructure Provisioning (Terraform)**
   - Created AWS VPC with public and private subnets.
   - Provisioned EKS cluster and Managed NodeGroups.

3. **Kubernetes Deployments**
   - Deployed backend and frontend Deployments with Services and Ingress.
   - Deployed `frontendproxy` Deployment/Service/Ingress to route traffic.
   - Integrated TLS with cert-manager.
   - Used HPA (HorizontalPodAutoscaler) and Cluster Autoscaler for automatic scaling.

4. **CI/CD Automation**
   - GitHub Actions triggered on push:
     - Builds Docker images → tags → pushes to ECR
     - Applies updated Kubernetes manifests to EKS
   - Alternatively, ArgoCD could be integrated for GitOps.

---

###  How to Deploy (Developer Guide)
1. **Clone repo**:
   ```bash
   git clone https://github.com/alk111/aws-devops-automation.git
   cd aws-devops-automation
   ```

2. **Bootstrap infrastructure**
   ```bash
   cd terraform
   terraform init
   terraform apply
   ```
   - Takes ~10 min: VPC, EKS provision.

3. **Setup credentials**
   ```bash
   aws eks update-kubeconfig --name <cluster-name> --region <region>
   ```

4. **Apply Kubernetes manifests**
   ```bash
   kubectl apply -f kubernetes/backend
   kubectl apply -f kubernetes/frontendproxy
   kubectl apply -f kubernetes/frontend
   ```

5. **Push code changes**
   - CI pipeline triggers image builds and deploys.

6. **Access the app**
   - Ingress assigns public DNS via AWS NLB/ALB.
   - React frontend accessible via `https://frontend.yourdomain.com`.

---

###  Scalability & HA
- **Backend & frontend** scale with **HPA** (e.g., based on CPU usage).
- **Cluster Autoscaler** scales EC2 nodes automatically.
- **RDS Multi-AZ** ensures database high availability.
- **EKS Managed NodeGroups** support rolling updates.
- **Ingress + ALB/NLB** distributes traffic evenly.

---

### Security & Best Practice
- **SSL/TLS**: via cert-manager and Let’s Encrypt.
- **Network security**: database in private subnet, restricted SGs.
- **Secrets management**: using AWS SSM Parameter Store or Kubernetes Secrets.
- **Monitoring/logging**: integrate Prometheus & Grafana via kube-prometheus; logs via CloudWatch.

---

### 🗂️ Directory Structure
```
.
├── terraform/                 # AWS infra provisioning
├── kubernetes/
│   ├── backend/
│   ├── frontend/
│   └── frontendproxy/
└── src/                       # application code
    ├── backend/
    └── frontend/
```


---

---

## 🔁 Step-by-Step Traffic Flow

1. **User opens** `http://frontend.yourdomain.com` in the browser.
2. **DNS** resolves the domain to the Ingress controller's external IP.
3. **Ingress controller** matches the domain/path to `frontendproxy` service.
4. **frontendproxy** is an NGINX pod that reverse proxies the request to `omapi-frontend-service`.
5. **omapi-frontend-service** forwards it to the React frontend pods.
6. **React app responds** and the response flows back through the same chain.

---

## Summary
This solution:
- **Automates** full infra + app deployment via IaC + CI/CD
- **Ensures scalability & HA** using Kubernetes autoscaling + managed DB
- **Separates concerns** between frontend, backend, proxy, and DB
- **Simplifies developer workflow**: push code → pipeline builds & publishes → live app updated
