using Data.Configuration;
using Data.Entities;
using FluentAssertions;
using NHibernate;

namespace AspNetCoreMvc.IntegrationTests;

[Collection("NHibernate Collection")]
public class NHibernateHelperTests
{
    public NHibernateHelperTests(NHibernateFixture fixture)
    {
        // Fixture ensures NHibernate is initialized
    }

    [Fact]
    public void NHibernate_SessionFactory_Initialized()
    {
        // Act
        var sessionFactory = NHibernateHelper.SessionFactory;

        // Assert
        sessionFactory.Should().NotBeNull();
        sessionFactory.IsClosed.Should().BeFalse();
    }

    [Fact]
    public void NHibernate_CanOpenSession()
    {
        // Arrange
        var sessionFactory = NHibernateHelper.SessionFactory;

        // Act
        using var session = sessionFactory.OpenSession();

        // Assert
        session.Should().NotBeNull();
        session.IsOpen.Should().BeTrue();
    }

    [Fact]
    public void NHibernate_Transaction_CommitsSuccessfully()
    {
        // Arrange
        using var session = NHibernateHelper.SessionFactory.OpenSession();
        var product = new Product
        {
            Name = $"Transaction Test Product {Guid.NewGuid()}",
            Category = "Transaction Test Category",
            Discontinued = false
        };

        // Act
        using (var transaction = session.BeginTransaction())
        {
            session.Save(product);
            transaction.Commit();
        }

        // Assert
        product.Id.Should().BeGreaterThan(0);

        // Cleanup
        using (var transaction = session.BeginTransaction())
        {
            session.Delete(product);
            transaction.Commit();
        }
    }

    [Fact]
    public void NHibernate_Transaction_RollbackSuccessfully()
    {
        // Arrange
        using var session = NHibernateHelper.SessionFactory.OpenSession();
        var product = new Product
        {
            Name = $"Rollback Test Product {Guid.NewGuid()}",
            Category = "Rollback Test Category",
            Discontinued = false
        };

        int productId;

        // Act
        using (var transaction = session.BeginTransaction())
        {
            session.Save(product);
            productId = product.Id;
            transaction.Rollback();
        }

        // Clear session to ensure fresh read
        session.Clear();

        // Try to retrieve the product
        var retrievedProduct = session.Get<Product>(productId);

        // Assert
        retrievedProduct.Should().BeNull("transaction was rolled back");
    }

    [Fact]
    public void NHibernate_CanQueryDatabase()
    {
        // Arrange
        using var session = NHibernateHelper.SessionFactory.OpenSession();

        // Act
        var query = session.CreateQuery("from Product");
        var products = query.List<Product>();

        // Assert
        products.Should().NotBeNull();
    }
}
