using TaskoMask.ApiGateways.UserPanel.ApiGateWay.Configuration;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices().ConfigurePipeline();

app.Run();