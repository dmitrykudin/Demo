using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System.Collections.Generic;

namespace DataLayer.MSSQL.Tests
{
    [TestClass]
    public class ProductClassRepositoryTest
    {
        [TestMethod]
        public void ShouldAddProductClass()
        {
            string name = "Неклассифицированный";
            ProductClassRepository repo = new ProductClassRepository();
            repo.CreateProductClass(name, null, null);
        }

        [TestMethod]
        public void ShouldGetAllDescendants()
        {
            string wine = "Вино";
            string redWine = "Красное вино";
            string whiteWine = "Белое вино";
            string redDryWine = "Красное сухое вино";
            string whiteDryWine = "Белое сухое вино";
            string redSemisweetWine = "Красное полусладкое вино";
            string whiteSemisweetWine = "Белое полусладкое вино";
            string redSweetWine = "Красное сладкое вино";
            string whiteSweetWine = "Белое сладкое вино";

            ProductClassRepository repo = new ProductClassRepository();
            //repo.CreateProductClass(wine, null, null);
            ProductClass parent = repo.GetProductClass(wine);
            //repo.CreateProductClass(redWine, null, parent.Id);
            //repo.CreateProductClass(whiteWine, null, parent.Id);
            //parent = repo.GetProductClass(redWine);
            //repo.CreateProductClass(redDryWine, null, parent.Id);
            //repo.CreateProductClass(redSemisweetWine, null, parent.Id);
            //repo.CreateProductClass(redSweetWine, null, parent.Id);
            //parent = repo.GetProductClass(whiteWine);
            //repo.CreateProductClass(whiteDryWine, null, parent.Id);
            //repo.CreateProductClass(whiteSemisweetWine, null, parent.Id);
            //repo.CreateProductClass(whiteSweetWine, null, parent.Id);
            parent = repo.GetProductClass(wine);

            repo.GetProductClassDescendants(parent.Id);
        }

        [TestMethod]
        public void ShouldDeleteProductClass()
        {
            // TODO Реализовать
        }

        [TestMethod]
        public void ShouldGetChildren()
        {
            ProductClassRepository repo = new ProductClassRepository();
            List<ProductClass> children = repo.GetProductClassChildren(2);
        }
    }
}
