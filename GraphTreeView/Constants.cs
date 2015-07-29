namespace GraphTreeView
{
    class Constants
    {
        // Note: update the string TenantId with your TenantId.
        // This can be retrieved from the login Federation Metadata end point:             
        // https://login.microsoftonline.com/contoso.onmicrosoft.com/FederationMetadata/2007-06/FederationMetadata.xml
        //  Replace "contoso.onmicrosoft.com" with any domain owned by your organization
        // The returned value from the first xml node "EntityDescriptor", will have a STS URL
        // containing your TenantId e.g. "https://sts.windows.net/4fd2b2f2-ea27-4fe5-a8f3-7b1a7c975f34/" is returned for GraphDir1.onMicrosoft.com

        public const string TenantName = "contoso.onmicrosoft.com";
        public const string TenantId = "guid";
        public const string ClientId = "get-from-Azure-Portal";
        public const string ClientSecret = "get-from-Azure-Portal";
        public const string AuthString = "https://login.microsoftonline.com/" + TenantName;
        public const string ResourceUrl = "https://graph.windows.net";
    }
}
