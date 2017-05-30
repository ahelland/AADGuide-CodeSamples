## AADGuide-CodeSamples
Code Samples for AADGuide

### Description
There are the code samples for the Azure Active Directory Guide  
Website: [https://aadguide.azurewebsites.net](http://aadguide.azurewebsites.net)  
GitHub: [https://github.com/ahelland/AADGuide](https://github.com/ahelland/AADGuide)

### Projects in this solution  

#### AboutMePasswordGrant
Platform: .NET 4.6.1  
Demo Windows Forms app implementing the password grant OAuth flow. No libraries involved, just plain http calls.

#### ADFSWebAPIClient 
Platform: Universal Windows Platform (Min 10240)  
Client for calling into a Web API protected by ADFS 2016.

#### ADFSWebAPIServer 
Platform: .NET 4.6.1  
Demo Web API protected by ADFS 2016.
 
#### B2CVideoPortal
Platform: .NET 4.6  
Demo portal for the Azure AD B2C feature. Users login with social identity providers. Theming of site uses Office 365 UI Fabric. 

#### ClaimsWebApp 
Platform: .NET 4.6.1  
Demo app that prints out the claims of the logged in user. Also show to add/remove claims, and customize content based on attributes in claims.
 
#### CoreWebAPIClient 
Platform: Universal Windows Platform (Min 10240)  
App demoing how to acquire a token from the Azure AD v2 endpoint using Microsoft Authentication Library (MSAL). Subsequently calls into the API in CoreWebAPIServer. 

#### CoreWebAPIServer 
Platform: .NET Core 1.0
Demo web API protected by Azure AD using .NET Core. Accepts token issued by the Azure AD v2 endpoint.
 
#### GraphTreeView 
Platform: Windows Forms App  
App for getting info from an AAD tenant using the Graph API.
 
#### HelloAADB2C
Platform: Universal Windows Platform (Min 10586)  
App that shows how to authenticate a user against an Azure Active Directory B2C tenant using custom policies. The custom policy is set up to support both social identities (Facebook) and corporate identities (Azure AD).

#### HelloAzureAD 
Platform: Universal Windows Platform (Min 10240)  
App that shows how to authenticate a user against an Azure Active Directory tenant. Subsequently the acquired token is used to execute a query against the Graph API to extract the user object.
 
#### JwtCracker 
Platform: Windows Forms App  
Troubleshooting / debug utility that decodes a JSON Web Token.
 
#### WebAPIClientSingleTenant 
Platform: Universal Windows Platform (Min 10240)  
App demoing how to acquire a token from the Azure AD v1 endpoint using Active Directory Authentication Library (ADAL). Subsequently calls into the API in WebAPIServerSingleTenant.

#### WebAPIServerSingleTenant 
Platform: .NET 4.6.1  
Demo web API protected by Azure AD using .NET 4.6.1. Accepts tokens issued by the Azure AD v1 endpoint.