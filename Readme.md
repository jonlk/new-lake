# New Lake
## Portable Application Cluster with Kubernetes
### Using .Net6, MSSQL Server 2019, gRPC, Redis, RabbitMq and Seq
---
*These instructions are for Minikube and the Docker Engine (not Docker Desktop) on Ubuntu 22.04.*

*A different operating system may have different requirements for running Docker, however Minikube can run against any Docker implementation as well as several other VM's at the time of this project. See the [Minikube docs](https://minikube.sigs.k8s.io/docs/) for more details.*

*You do not need the .NET 6 SDK to run this application cluster, however, if you want to run and debug the application locally, you will need to [install it](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).*

---
### Instructions:
Install [Docker Engine](https://docs.docker.com/engine/) or [Docker Desktop](https://www.docker.com/products/docker-desktop/) depending on your system.

[Install Minikube (local Kubernetes cluster)](https://minikube.sigs.k8s.io/docs/start/)

**In a terminal shell, run the following:**

`minikube addons enable registry`

`minikube addons enable ingress`

`eval $(minikube docker-env)` *this binds the local Docker environment to the Minikube registry.*

---
*Recommend running in stages as running all of the background services at once will be a heavy load on most machines*

Redis phase:
1. In the terminal shell, navigate to the **'k8s/bgapps'** folder
2. Run the following: `kubectl apply -f redis`

