using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ProductManagement.Core.Products
{
    public class ProductFacade<T> : IProductFacade<T>
    {
        public List<T> GetImportedProducts()
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "internal", "products.json");
            return LoadJSon(jsonFilePath);
        }

        public List<T> UploadProuctCatalogue(string filePath)
        {
            return LoadJSon(filePath);
        }

        private static List<T> LoadJSon(string filePath)
        {
            var stringData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(stringData);
        }
    }
}
