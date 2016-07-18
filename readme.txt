NOTE! This code should not be used for constant application monitoring as far as may cause perfomance degradation.

Deployment guide:

1. Build project IISLogger
2. Deploy dll to GAC or add as a reference to the web application
3. Add the following line to the web.config file configuration/system.webServer/modules
 <add name="IISLogger" type="IISLogger.FilterSaveLogModule, IISLogger, Version=1.0.0.3, Culture=neutral, PublicKeyToken=8e9655242d1af61b"/>
4. Add the following line to the web.config file configuration/appSettings
<add key="iislogger:folder" value="<PATH TO THE LOGS FOLDER>"/>
e.g.
<add key="iislogger:folder" value="c:\temp\logs"/>
5. Add the following line to the web.config file configuration/appSettings
<add key="iislogger:logformat" value="<LOG FORMAT>"/>
e.g.
<add key="iislogger:logformat" value="{host}_{port}\{y}\{m}\{d}\{H}_{i}_{s}.log"/>
where
{host} - request host name
{port} - request port
{y} - year
{m} - month
{d} - day
{H} - hour
{i} - minute
{s} - second