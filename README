# Provider for Common.Logging Library to Sentry Raven

This is a provider for Common.Logging Library (C#) for sending your log items to Sentry Raven log servers.

## Dependencies:

- Common.Logging
- Newtonsoft.Json

## Version
1.0
## How to Install
### First way
Just run command in Package Manager Console in Visual Studio 
```
PM> Install-Package Common.Logging.Raven
```
### Second way

1. Right-click on `References` item of your project in Solution Explorer and click menu item `Manage NuGet Packages...`
2. Find in the `Nuget official package source (Online)` package `Common.Logging.Raven` and click install button.

## How to Use
### Configure app.config/web.config file of your project

```xml
...
<configSections>
  <sectionGroup name="common">
    <section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging" />
  </sectionGroup>
</configSections>
...
<common>
  <logging>
    <factoryAdapter type="Common.Logging.Raven.RavenLoggingFactoryAdapter, Common.Logging.Raven">
      <arg key="connectionString" value="{PROTOCOL}://{PUBLIC_KEY}:{SECRET_KEY}@{HOST}:{PORT}/{PROJECT_ID}" />
      <arg key="loggerName" value="Common.Logging.Raven.Test" />
    </factoryAdapter>
  </logging>
</common>
...
```
Where

- {PROTOCOL} - `http`, `https` or `udp`
- {PUBLIC_KEY} and {SECRET_KEY} - your keys from server. [See here](http://raven.readthedocs.org/en/latest/config/).
- {HOST} - site of server. For example: www.example.com
- {PORT} - Number of port
- {PROJECT_ID} - Identity of your project on server

For example:

- `http://b70a31b3510c4cf793964a185cfe1fd0:b7d80b520139450f903720eb7991bf3d@example.com:1000/1`
- `udp://b70a31b3510c4cf793964a185cfe1fd0:b7d80b520139450f903720eb7991bf3d@example.com:1000/23`

### In your code type
```csharp
ILog log = LogManager.GetCurrentClassLogger();
log.Error("Error text");
```

## Links
- [Common.Logging Library](https://github.com/net-commons/common-logging)
- [Sentry Raven Homepage](https://www.getsentry.com/welcome/)
- [Sentry Raven Documentation](https://sentry.readthedocs.org/en/latest/index.html)
- [Common.Logging.Raven on NuGet site](http://nuget.org/packages/Common.Logging.Raven)