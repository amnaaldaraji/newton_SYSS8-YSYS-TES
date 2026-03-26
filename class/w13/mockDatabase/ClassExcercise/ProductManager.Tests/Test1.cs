namespace ProductManager.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[TestCategory("Integration")]
public class IntegrationTestProductRepository
{
    [TestMethod]
    public void GetProductsByCategory_ReturnsProductsFromDatabase()
    {
        // Arrange
        var repo = new ProductRepository();

        // Act
        var result = repo.GetProductsByCategory("Tech");

        // Assert
        Assert.IsTrue(result.Count > 0);
        foreach (var p in result)
        {
            Assert.AreEqual("Tech", p.Category);
        }
    }
}
