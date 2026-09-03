using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var _ = builder.AddProject<Gsri_Personnels>("app");

await builder.Build().RunAsync();
