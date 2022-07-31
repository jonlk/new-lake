# New Lake

## Exploring decoupled services with .Net 6 and Kubernetes

- Redis Cache
- Rabbit MQ
- gRPC Server and Client

You will need to host Redis and RabbitMQ. If you don't already have them setup on a server, they can both be run as Docker containers.

### Redis Cache Setup
`docker run -d --name localredis -p 6379:6379 redis:latest`

1. The attached Postman file has set, get, and delete operations for working with the Redis cache. (You may need to adjust the url.)

### RabbitMQ Setup
`docker run -d --hostname rabbithost --name localrabbit -p 5672:5672 -p 15672:15672 rabbitmq:latest`

1. Launch NewLake.Api
2. Right click on NewLake.QueueListener and select Debug > Start New Instance.
3. Use the Message Publish example in the attached Postman collection to post messages through the Api to be received by the listener. (You may need to adjust the url.)

### gRPC Setup
1. Launch NewLake.Api
2. Right click on NewLake.GrpcServer and select Debug > Start New Instance.
3. The gRPC auto server will start sending messages to NewLake.Api at 10 second intervals (adjustable in appsettings.json `"DelayInterval":int`).
