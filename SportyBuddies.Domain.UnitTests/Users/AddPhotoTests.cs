using FluentAssertions;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class AddPhotoTests
{
    [Fact]
    public void AddPhoto_ShouldSetMainPhoto_WhenPhotoIsMarkedAsMain()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var mainPhoto = UserPhoto.Create(user, "", true);
        
        // Act
        user.AddPhoto(mainPhoto);
        
        // Assert
        user.MainPhoto.Should().Be(mainPhoto);
        user.MainPhotoId.Should().Be(mainPhoto.Id);
        user.Photos.Count.Should().Be(0);
    }
    
    [Fact]
    public void AddPhoto_ShouldAddPhotoToPhotosCollection_WhenPhotoIsNotMarkedAsMain()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var photo = UserPhoto.Create(user, "", false);
        
        // Act
        user.AddPhoto(photo);
        
        // Assert
        user.Photos.Should().Contain(photo);
        user.MainPhoto.Should().BeNull();
        user.MainPhotoId.Should().BeNull();
    }
}