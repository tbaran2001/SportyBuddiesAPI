namespace SportyBuddies.Api.IntegrationTests.Common;

[CollectionDefinition(CollectionName)]
public class SportyBuddiesApiCollection : ICollectionFixture<SportyBuddiesApiFactory>
{
    public const string CollectionName = "SportyBuddiesApiFactoryCollection";
}