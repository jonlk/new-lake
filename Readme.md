# New Lake
## Portable Application Cluster with Kubernetes
### Using .Net7, MSSQL Server 2019, gRPC, Redis, RabbitMq and Seq

---
*Now updated for .NET 7

*These instructions are for Minikube and the Docker Engine (not Docker Desktop) on Ubuntu 22.04.*

*A different operating system may have different requirements for running Docker, however Minikube can run against any Docker implementation as well as several other VM's at the time of this project. See the [Minikube docs](https://minikube.sigs.k8s.io/docs/) for more details.*

*You do not need the .NET 6 SDK to run this application cluster, however, if you want to run and debug the application locally, you will need to [install it](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).*

---
### Instructions:
Install [Docker Engine](https://docs.docker.com/engine/) or [Docker Desktop](https://www.docker.com/products/docker-desktop/) depending on your system.

[Install Minikube (local Kubernetes cluster)](https://minikube.sigs.k8s.io/docs/start/)

**In a terminal shell, run the following:**

`minikube start`

`minikube addons enable registry`

`minikube addons enable ingress`

`eval $(minikube docker-env)` *this binds the local Docker environment to the Minikube registry.*

---

In the terminal shell, navigate to the repository root directory and run the following:

`docker-compose build`

It may take a couple of minutes for all of the images to build. Once the build process is completed, run the following:

`docker images`

and confirm the **four** vertwave/new-lake-<*app name*> images are shown.

---
### Setting up the namespace:
1. In the terminal shell, navigate to the **'k8s/'** directory.
2. Run the following: `kubectl apply -f namespace.yml`.
3. Switch to the new namespace: `kubectl config set-context --current --namespace=new-lake`.
---
### Setting up the monitoring:
Seq is a centralized logging visualization tool built for microservices monitoring. Find out more about it [here](https://datalust.co/seq).

1. In the terminal shell, navigate to the **'k8s/dev/bgapps'** directory.
2. Run the following: `kubectl apply -f seq.yml`.
3. Run the following: `kubectl get all` to ensure the Seq tool has started.
4. Note the NodePort number in the service/seq-service. It should be 31534.
5. Run the following to get the IP Address for the Minikube cluster. `minikube ip`.
6. Open a new browser instance and in the address bar, enter <*your_minikube_ip_address*>:31534.
7. The Seq web application should launch. Note there is no output at the moment.   
---
*Recommend running in stages as running all of the background services at once will be a heavy load on most machines!!!*

---
### Setting up the Redis cache:
Redis is a high-availability, low-latency, in-memory NoSQL cache with optional durability that can be used for anything from a key-value store to a query accelerator. Find more about it [here](https://redis.io/).

Redis StatefulSet phase:
1. In the terminal shell, navigate to the **'k8s/dev/bgapps'** directory.
2. Run the following: `kubectl apply -f redis` to run all of the manifests in the folder.
3. Run the following: `kubectl get all` to ensure the Redis stateful set has started.

---
### Setting up and starting the New Lake Api
This Api will be the main entry point for interacting with all of the services for this demo.
1. In the terminal shell, navigate to the **'k8s/dev'** directory.
2. Run the following: `kubectl apply -f api.yml` to start the New Lake Api service.
3. It will take about 30 seconds for the liveness and readiness probes to complete.
4. Run the following: `kubectl get all` to ensure the New Lake Api has started.
5. Run the following: `kubectl apply -f ingress.yml`. The ingress will allow external access to the service.
6. Open the hosts file on your machine. The location will vary depending on your OS and you will probably need to open it with administrative rights. Once you have located and opened it, add the following line: <*your_minikube_ip_address*> TABSPACE `new-lake.local` . This will map the host named in the **ingress.yml** file to your Minikube cluster.

*NOTE: You can now also use this host name in place of the ip address for the Seq application listed above*

## Api/Redis cache - Postman Test

1. Run the **POST CacheSet** request , you should receive a **Status: 201 Created**
2. In the Response Headers, find the key Location and note the URL value. This is the URL that should be supplied to retrieve the data.

