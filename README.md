# Oqtane.LicensedModule

Oqtane is a CMS and app framework which can be extended with additional modules and themes. Extensions which are developed as off-the-shelf products by ISVs must be registered in the [Oqtane Marketplace](https://www.oqtane.net). Extensions can be offered under either an open source license or commercial license. Commercial extensions will need to include a licensing capability to restrict access to authorized users. Developers can utilize their own licensing solution within their extensions, or they can take advantage of the integrated licensing solution which is included with the Oqtane Framework. 

This repo provides an example of a simplistic commercial module which utilizes the integrated licensing solution.

Oqtane.LicensedModule.Client.csproj 

Includes the Nuget package reference to Oqtane.Licensing:

```
<PackageReference Include="Oqtane.Licensing" Version="5.0.1" />
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

The Oqtane.LicensedModule.Package project must include references to the licensing assemblies so they are distributed with your module. 

debug.cmd (to copy the assemblies during development)

```
XCOPY "..\Client\bin\Debug\net8.0\Oqtane.Licensing.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\Client\bin\Debug\net8.0\Oqtane.Licensing.Server.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
XCOPY "..\Client\bin\Debug\net8.0\Oqtane.Licensing.Shared.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net8.0\" /Y
```

Oqtane.LicensedModule.nuspec (to include the assemblies in the Nuget package)

```
<file src="..\Client\bin\Release\net8.0\Oqtane.Licensing.Client.Oqtane.dll" target="lib\net8.0" /> 
<file src="..\Client\bin\Release\net8.0\Oqtane.Licensing.Shared.Oqtane.dll" target="lib\net8.0" />
<file src="..\Client\bin\Release\net8.0\Oqtane.Licensing.Server.Oqtane.dll" target="lib\net8.0" />
```

# Oqtane Marketplace Sites

As mentioned above, extensions which are developed as off-the-shelf products by ISVs must be registered in the Oqtane Marketplace. In order to allow developers to safely test the licensing process for their extensions, there are in fact 2 distinct Marketplace sites:

[Sandbox Marketplace](https://sandbox.oqtane.net) - a test environment where extensions can be registered and e-commerce transactions can be simulated

[Production Marketplace](https://www.oqtane.net) - the actual production Oqtane Marketplace

In order to test the licensing process, you will want to use the Sandbox Marketplace. You can login to the Sandbox Marketplace using your GitHub account and register your owner information (which will require integration with Stripe Connect) as well as your products. Once the products are registered, you will want to point your local development environment to the Sandbox Marketplace. You can do this by setting the PackageRegistryUrl setting in appsettings.json (you can also manage this via the Oqtane Admin Dashboard UI and choosing the System Info option - Options tab).

appsettings.json

```
  "PackageRegistryUrl": "https://sandbox.oqtane.net"
```

Once this is configured, when you browse to the Module or Theme Management areas of the Admin Dashboard and choose Install, the products will be loaded from the Sandbox Marketplace. It will allow you to perform a complete simulated purchase and licensing flow for these products. Please note that license keys generated in the Sandbox Marketplace are only valid for 7 days.

Be sure to remember to reset the PackageRegistryUrl when you are finished testing.

# Development 

When performing local development (ie. on localhost or 127.0.0.1) the licensing component will always indicate that a module is licensed. This ensures that your development activities are never impacted based on licensing restrictions. If you would like to simulate the licensing flow of a production environment for testing purposes, you will need to generate a license key for your installation.

First you will need to create the product in the Marketplace (see above). Navigate to the License section and choose the Keys button next to the specific license variant you want to activate. This will display a modal dialog where you can enter the Installation Id of your local environment (found in the System Info area of the Admin Dashboard or appsettings.json file) as well as your Email Address and click Generate. This will create a license key and a notification will be sent to your email.

Once the license key is generated, you can return to your local installation and you can add a "licensing=testmode" parameter to the querystring in your browser. This will force the licensing component into an unlicensed flow where you have the ability to Activate a license key manually. Clicking Activate provides the option to Fetch a license key from the Marketplace server or manually enter a license key and click Validate.

# License Keys

License files are stored locally in the /bin folder of an installation. They utilize a file naming convention of Package Name plus an ".lic" extension so that they are easy to identify. If a license file is deleted, it will be regenerated automatically the next time the module is accessed (in production only). License keys are specific to a product variant and installation (and Marketplace). License keys contain 10 segments of 4 characters (an example is included below).

```
C686-855A-7572-C55F-715E-58EB-C31D-9999-1231-0001
```


