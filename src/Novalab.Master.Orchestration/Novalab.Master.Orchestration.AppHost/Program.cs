var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo")
                   .WithLifetime(ContainerLifetime.Persistent);

var mongodb = mongo.AddDatabase("mongodb");

var rabbitmq = builder.AddRabbitMQ("messaging")
                      .WithManagementPlugin()
                      .WithLifetime(ContainerLifetime.Persistent);

var master = builder.AddProject<Projects.Novalab_Master_Host>("master")
                    .WithReference(rabbitmq)
                    .WithReference(mongodb)
                    .WaitFor(rabbitmq)
                    .WaitFor(mongodb);

var frontend = builder.AddProject<Projects.Novalab_Master_Frontend>("frontend")
                      .WithReference(master)
                      .WaitFor(master);
builder.Build().Run();
