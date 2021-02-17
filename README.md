# Azure Identity Livestream

This is the repository used during the 425show Twitch livestream at <https://www.twitch.tv/videos/919350158>.

The goal is to have a look at the Azure Identity library, which is part of the Azure SDK.
It allows developers to leverage Azure Active Directory authentication when interacting with Azure services that support it.
This means that by using Azure managed identity, we can build a credential-less application.

To showcase this, we build a simple ASP<span></span>.NET Core app that displays a list of people.
We demonstrate how to use Azure Identity in several ways:

- First, to connect to Azure Blob Storage, using the dedicated client library which is also part of the Azure SDK.
- Then, we look at how we can connect to Azure SQL with Dapper, where we are in control of the creation and disposal of the `SqlConnection` instances.
- Finally, we use what we learnt in the previous step to use AAD auth with Azure SQL while using EF Core.

## Step by step walkthrough

If you can't watch the show, please head over to the [releases page](https://github.com/mderriey/azure-identity-livestream/releases) of this repository where each step we took is explained.

## Interesting links

- List of Azure services that support Azure managed identity for resources: <https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/services-support-managed-identities#azure-services-that-support-managed-identities-for-azure-resources>.
- List of Azure services that support Azure Active Directory authentication: <https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/services-support-managed-identities#azure-services-that-support-azure-ad-authentication>.
- The Azure Identity Git repository: <https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/identity/Azure.Identity>.
- Overview of Azure managed identity: <https://docs.microsoft.com/azure/active-directory/managed-identities-azure-resources/overview>.
- Azure SDK releases: <https://aka.ms/azsdk>.
- Overview of AAD auth for Azure SQL: <https://docs.microsoft.com/azure/azure-sql/database/authentication-aad-overview>.
- The `/.default` scope in AAD: <https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-permissions-and-consent#the-default-scope>.
- A blog post on the Azure SDK blog talking about those concepts: <https://devblogs.microsoft.com/azure-sdk/azure-identity-with-sql-graph-ef/>.
- EF Core interceptors: <https://docs.microsoft.com/ef/core/logging-events-diagnostics/interceptors>.
- The Azurite Git repository: <https://github.com/Azure/Azurite>.
- A blog post explaining how to enable AAD auth when using Azurite: <https://blog.jongallant.com/2020/04/local-azure-storage-development-with-azurite-azuresdks-storage-explorer/>.
