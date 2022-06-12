using System.Diagnostics;
using AzureClassLibrary.Grpc.Channel;
using GrpcService;
using Microsoft.AspNetCore.Mvc;
using MVC1.Models;

namespace MVC1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GrpcChannelFactory _channelFactory;
    private readonly string _grpc1Host;
    private readonly string _grpc2Host;
    private readonly string _grpc3Host;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _channelFactory = new GrpcChannelDemoFactory();
        _grpc1Host = Environment.GetEnvironmentVariable("GRPC1_HTTP") ?? ""; 
        _grpc2Host = Environment.GetEnvironmentVariable("GRPC2_HTTP") ?? "";
        _grpc3Host = Environment.GetEnvironmentVariable("GRPC3_HTTP") ?? "";
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Service1_Demo()
    {
        var channel = _channelFactory.GetChannel(_grpc1Host);
        var client = new Serv1Demo.Serv1DemoClient(channel);
        client.Demo(new Serv1Request{ });
        return View();
    }
    
    public IActionResult Service2_Demo()
    {
        var channel = _channelFactory.GetChannel(_grpc2Host);
        var client = new Serv2Demo.Serv2DemoClient(channel);
        client.Demo(new Serv2Request { });
        return View();
    }
    
    public IActionResult Service2_401()
    {
        var channel = _channelFactory.GetChannel(_grpc2Host);
        var client = new Serv2Demo.Serv2DemoClient(channel);
        client.Return401(new Serv2Request { });
        return View();
    }
    
    public IActionResult Service2_403()
    {
        var channel = _channelFactory.GetChannel(_grpc2Host);
        var client = new Serv2Demo.Serv2DemoClient(channel);
        client.Return403(new Serv2Request { });
        return View();
    }
    
    public IActionResult Service2_404()
    {
        var channel = _channelFactory.GetChannel(_grpc2Host);
        var client = new Serv2Demo.Serv2DemoClient(channel);
        client.Return404(new Serv2Request { });
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}