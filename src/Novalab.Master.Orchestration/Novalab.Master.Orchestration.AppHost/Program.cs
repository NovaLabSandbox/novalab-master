using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo", 27017)
                   .WithLifetime(ContainerLifetime.Persistent);

var mongodb = mongo.AddDatabase("novalab", "novalab");

var redis = builder.AddRedis("redis", 6380)
                    .WaitFor(mongodb)
                    .WithRedisInsight()
                    .WithLifetime(ContainerLifetime.Persistent);

var pubSub = builder.AddDaprPubSub("pubsub", new DaprComponentOptions()
{
    LocalPath = "./../../../components/"
});

var configuration = builder.AddDaprComponent("configuration", "configuration.redis", new DaprComponentOptions()
{
    LocalPath = "./../../../components/"
});

var master = builder.AddProject<Projects.Novalab_Master_Host>("masterservice")
                    .WithReference(redis)
                    .WithReference(mongodb)
                    .WithDaprSidecar("master")
                    .WithReference(configuration)
                    .WithReference(pubSub)
                    .WaitFor(redis)
                    .WaitFor(mongodb);

var frontend = builder.AddProject<Projects.Novalab_Master_Frontend>("webfrontend")
                      .WithReference(master)
                      .WaitFor(master);

await builder.Build().RunAsync();
