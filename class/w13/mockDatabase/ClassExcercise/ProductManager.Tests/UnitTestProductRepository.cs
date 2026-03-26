namespace ProductManager.Tests;

using Moq;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[TestCategory("UnitTest")]
public class UnitTestProductRepository
{
    [TestMethod]
    public void GetProductsByCategory_ReturnsMockedProducts()
    {
        // Arrange
        var expectedProducts = new List<Product>
        {
            new Product { Name = "iPhone 17 pro", Category = "Tech", Price = "13000" }
        };

        var mockConnection = new Mock<IDbConnection>();
        var mockCommand = new Mock<IDbCommand>();
        var mockReader = new Mock<IDataReader>();

        var readCallCount = 0;
        mockReader.Setup(r => r.Read()).Returns(() => readCallCount++ == 0);
        mockReader.Setup(r => r.GetString(0)).Returns("iPhone 17 pro");
        mockReader.Setup(r => r.GetString(1)).Returns("Tech");
        mockReader.Setup(r => r.GetString(2)).Returns("13000");
        mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);
        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

        var repo = new ProductRepository(mockConnection.Object);

        // Act
        var result = repo.GetProductsByCategory("Tech");

        // Assert
        Assert.AreEqual(expectedProducts.Count, result.Count);
        Assert.AreEqual(expectedProducts[0].Name, result[0].Name);
        Assert.AreEqual(expectedProducts[0].Category, result[0].Category);
        Assert.AreEqual(expectedProducts[0].Price, result[0].Price);
    }
}
