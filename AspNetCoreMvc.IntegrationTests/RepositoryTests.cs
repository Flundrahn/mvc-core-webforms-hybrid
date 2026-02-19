using Data.Configuration;
using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using NHibernate;

namespace AspNetCoreMvc.IntegrationTests;

[Collection("NHibernate Collection")]
public class RepositoryTests : IDisposable
{
    private readonly ISession _session;
    private readonly ProductRepository _repository;
    private readonly List<Product> _testProducts = new();

    public RepositoryTests(NHibernateFixture fixture)
    {
        // Fixture ensures NHibernate is initialized
        // Initialize NHibernate session for testing
        var sessionFactory = NHibernateHelper.SessionFactory;
        _session = sessionFactory.OpenSession();
        _repository = new ProductRepository(_session);
    }

    [Fact]
    public void Repository_Save_InsertsData()
    {
        // Arrange
        var product = new Product
        {
            Name = $"Test Product {Guid.NewGuid()}",
            Category = "Test Category",
            Discontinued = false
        };

        // Act
        _repository.Save(product);
        _testProducts.Add(product);

        // Assert
        product.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Repository_Get_RetrievesData()
    {
        // Arrange - Create a product first
        var product = new Product
        {
            Name = $"Test Product {Guid.NewGuid()}",
            Category = "Test Category",
            Discontinued = false
        };

        _repository.Save(product);
        _testProducts.Add(product);

        // Clear session to ensure fresh read
        _session.Clear();

        // Act
        var retrievedProduct = _repository.Get(product.Id);

        // Assert
        retrievedProduct.Should().NotBeNull();
        retrievedProduct!.Name.Should().Be(product.Name);
        retrievedProduct.Category.Should().Be(product.Category);
    }

    [Fact]
    public void Repository_SaveOrUpdate_ModifiesData()
    {
        // Arrange - Create a product first
        var product = new Product
        {
            Name = $"Original Name {Guid.NewGuid()}",
            Category = "Original Category",
            Discontinued = false
        };

        _repository.Save(product);
        _testProducts.Add(product);

        var updatedName = $"Updated Name {Guid.NewGuid()}";
        var updatedCategory = "Updated Category";

        // Act
        product.Name = updatedName;
        product.Category = updatedCategory;
        _repository.Save(product);

        _session.Clear();
        var retrievedProduct = _repository.Get(product.Id);

        // Assert
        retrievedProduct.Should().NotBeNull();
        retrievedProduct!.Name.Should().Be(updatedName);
        retrievedProduct.Category.Should().Be(updatedCategory);
    }

    [Fact]
    public void Repository_Delete_RemovesData()
    {
        // Arrange - Create a product first
        var product = new Product
        {
            Name = $"To Be Deleted {Guid.NewGuid()}",
            Category = "Delete Category",
            Discontinued = false
        };

        _repository.Save(product);

        var productId = product.Id;

        // Act
        _repository.Delete(product);

        _session.Clear();
        var retrievedProduct = _repository.Get(productId);

        // Assert
        retrievedProduct.Should().BeNull();
    }

    [Fact]
    public void Repository_GetAll_ReturnsMultipleProducts()
    {
        // Arrange - Create multiple products
        var products = new[]
        {
            new Product { Name = $"Product A {Guid.NewGuid()}", Category = "Category A", Discontinued = false },
            new Product { Name = $"Product B {Guid.NewGuid()}", Category = "Category B", Discontinued = false }
        };

        foreach (var product in products)
        {
            _repository.Save(product);
            _testProducts.Add(product);
        }

        // Act
        var allProducts = _repository.GetAll();

        // Assert
        allProducts.Should().NotBeNull();
        allProducts.Should().HaveCountGreaterOrEqualTo(2);
    }

    public void Dispose()
    {
        // Cleanup: Delete test products
        if (_testProducts.Any())
        {
            try
            {
                foreach (var product in _testProducts)
                {
                    var existingProduct = _session.Get<Product>(product.Id);
                    if (existingProduct != null)
                    {
                        _repository.Delete(existingProduct);
                    }
                }
            }
            catch
            {
                // Ignore cleanup errors
            }
        }

        _session?.Dispose();
    }
}
