using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serialization;
using Serialization.Implementations;
using Serialization.Interfaces;

var f = new F().Get();

var cfg = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IOutput, ConsoleOutput>();
builder.Services.AddSingleton<IConfiguration>(cfg);
builder.Services.AddSingleton(typeof(ISerializer<F>), typeof(CustomSerializer<F>));
builder.Services.AddSingleton(typeof(ISerializer<F>), typeof(JsonSerializer<F>));
builder.Services.AddSingleton(typeof(IDeserializer<F>), typeof(CustomDeserializer<F>));
builder.Services.AddSingleton(typeof(IDeserializer<F>), typeof(JsonDeserializer<F>));
builder.Services.AddSingleton(typeof(ISpeedMeter<F>), typeof(SpeedMeter<F>));


using IHost host = builder.Build();
var scope = host.Services.CreateScope();

var speedMeter = scope.ServiceProvider.GetService<ISpeedMeter<F>>();
var serializer = scope.ServiceProvider.GetServices<ISerializer<F>>();
var deserializer = scope.ServiceProvider.GetServices<IDeserializer<F>>();
var customSerializer = serializer.First(s => s.GetType() == typeof(CustomSerializer<F>));
var jsonSerializer = serializer.First(s => s.GetType() == typeof(JsonSerializer<F>));
var customDeserializer = deserializer.First(d => d.GetType() == typeof(CustomDeserializer<F>));
var jsonDeserializer = deserializer.First(d => d.GetType() == typeof(JsonDeserializer<F>));


var serializedCustom = customSerializer.Serialize(f);
var serializedJson = jsonSerializer.Serialize(f);
var analyzerCustom = new Analyzer<F>(customSerializer, customDeserializer, speedMeter);
var analyzerJson = new Analyzer<F>(jsonSerializer, jsonDeserializer, speedMeter);

analyzerCustom.CheckSerialization(f, true);
analyzerCustom.CheckSerialization(f, false);
analyzerCustom.CheckDeserialization(serializedCustom);

//analyzerJson.CheckSerialization(f, true);
//analyzerJson.CheckSerialization(f, false);
//analyzerJson.CheckDeserialization(serializedJson);


Console.ReadLine();
