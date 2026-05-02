var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Xelit3_IssueLab_IdentityServerScalar>("idp");

builder.AddProject<Projects.Xelit3_IssueLab_IdentityServerScalar_Api>("api")
    .WithUrl("/scalar", "Scalar UI");

builder.Build().Run();
