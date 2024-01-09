# Oqtane.LicensedModule

Oqtane is a CMS and app framework which can be extended with additional modules and themes. Extensions which are developed as off-the-shelf products by ISVs must be registered in the [Oqtane Marketplace](https://www.oqtane.net). Extensions can be offered under either an open source license or commercial license. Commercial extensions will need to include a licensing capability to restrict access to authorized users. Developers can utilize their own licensing solution within their extensions, or they can take advantage of the integrated licensing solution which is included with the Oqtane Framework. 

This repo provides an example of a simplistic commercial module which utilizes the integrated licensing solution.

Oqtane.LicensedModule.Client.csproj 

Includes the Nuget package reference to Oqtane.Licensing:

```
<PackageReference Include="Oqtane.Licensing" Version="5.0.0" />
```

Index.razor

Includes the LicenseView component as a wrapper around the module content. The PackageName parameter is required and should match the PackageName specified in the ModuleInfo.cs (IModule interface definition). It must also match the Package Name field which is specified for the product in the Oqtane Marketplace administrative UI. Note that the LicenseView component also supports content sections for NotLicensed and Validating which can optionally be implemented depending on your requirements.

```
@namespace Oqtane.LicensedModule
@using Oqtane.Licensing
@inherits ModuleBase

<LicenseView PackageName="@ModuleState.ModuleDefinition.PackageName">
    <Licensed>
        <p>I am Licensed!</p>
    </Licensed>
</LicenseView>
```

ModuleInfo.cs

If you wish to allow your module to support Blazor WebAssembly you will need to ensure you specify the licensing component as a dependency of your module:

```
Dependencies = "Oqtane.Licensing.Client.Oqtane,Oqtane.Licensing.Shared.Oqtane"
```

Oqtane.LicensedModule.Package.csproj

Note that the Oqtane.LicensedModule.Package project contains no specific reference to the licensing component. This is because the licensing component is distributed by default with the Oqtane Framework starting in version 5.0. If you need to support older versions of Oqtane (ie. 4.x, 3.x, etc...) then you will need to distribute the licensing component with your module. This would require you to modify the following files:

debug.cmd

```
XCOPY "..\Client\bin\Debug\net8.0\Oqtane.Licensing.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\Client\bin\Debug\net8.0\Oqtane.Licensing.Server.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\Client\bin\Debug\net8.0\Oqtane.Licensing.Shared.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
```

Oqtane.LicensedModule.nuspec

```
<file src="..\Client\bin\Release\net8.0\Oqtane.Licensing.Client.Oqtane.dll" target="lib\net8.0" /> 
<file src="..\Client\bin\Release\net8.0\Oqtane.Licensing.Shared.Oqtane.dll" target="lib\net8.0" />
<file src="..\Client\bin\Release\net8.0\Oqtane.Licensing.Server.Oqtane.dll" target="lib\net8.0" />
```

# Oqtane Marketplace Sites

As mentioned above, extensions which are developed as off-the-shelf products by ISVs must be registered in the Oqtane Marketplace. In order to allow developers to safely test the licensing process for their extensions, there are in fact 2 distinct Marketplace sites:

[Sandbox Marketplace](https://sandbox.oqtane.net) - a test environment where extensions can be registered and e-commerce transactions can be simulated

[Production Marketplace](https://www.oqtane.net) - the actual production Oqtane Marketplace

In order to test the licensing process, you will want to use the Sandbox Marketplace. You can login to the Sandbox Marketplace using your GitHub account and register your products. Once the products are registered, you will want to point your local development environment to the Sandbox Marketplace. You can do this by setting the PackageRegistryUrl setting in appsettings.json (you can also manage this via the Oqtane Admin Dashboard UI and choosing the System Info option - Options tab).

appsettings.json

```
  "PackageRegistryUrl": "https://sandbox.oqtane.net"
```

Once this is configured, when you browse to the Module or Theme Management areas of the Admin Dashboard and choose Install, the products will be loaded from the Sandbox Marketplace. It will allow you to perform a complete simulated purchase and licensing flow for these products. 

Be sure to remember to reset the PackageRegistryUrl when you are finished testing.

