# New Lake

## NET 6 Api with the following services

- Redis Cache
- Rabbit MQ
- gRPC Server and Client

You will need to host Redis and RabbitMQ. If you don't already have them setup on a server, they can both be run as Docker containers.

### Docker Redis Cache Setup
`docker run -d --name localredis -p 6379:6379 redis:latest`

### Docker RabbitMQ Setup
`docker run -d --hostname rabbithost --name localrabbit -p 5672:5672 -p 15672:15672 rabbitmq:latest`