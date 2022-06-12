using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace AzureClassLibrary.Monitor;

public class ApplicationInsights
{
    
}

public class CustomTelemetryInitializer : ITelemetryInitializer
{
    private readonly string _roleName;
    public CustomTelemetryInitializer(string roleName)
    {
        _roleName = roleName;
    }

    public void Initialize(ITelemetry telemetry)
    {
        if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
        {
            //set custom role name here
            telemetry.Context.Cloud.RoleName = this._roleName;
            //telemetry.Context.Cloud.RoleInstance = "Custom RoleInstance";
        }
    }
}

//namespace CustomInitializer.Telemetry
//{
    
//}