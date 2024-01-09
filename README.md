# Oqtane.LicensedModule

Oqtane is a CMS and app framework which can be extended with additional modules and themes. Extensions which are developed as off-the-shelf products by ISVs are available through the [Oqtane Marketplace](https://www.oqtane.net). Extensions can be offered under either an open source license or commercial license. Commercial extensions will need to include a licensing capability to restrict access to authorized users. Developers can utilize their own licensing solution within their extensions, or they can take advantage of the integrated licensing solution which is included with the Oqtane Framework. 

This repo provides an example of a simplistic commercial module which utilizes the integrated licensing solution.

Oqtane.LicensedModule.Client.csproj 

Includes the Nuget package reference to Oqtane.Licensing:

```
	<PackageReference Include="Oqtane.Licensing" Version="5.0.0" />
```

Index.razor

Includes the LicenseView component as a wrapper around the module content. The PackageName parameter is required and should match the PackageName specified in the ModuleInfo.cs (IModule interface definition). 

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

Note that there are also content sections for NotLicensed and Validating which can optionally be implemented depending on your requirements.

