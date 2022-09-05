﻿namespace deploy_sample.Models
{
    /// <summary>
    /// Propriedades que definem um produto
    /// </summary>
    public class Product
    {
        public string IdProduct { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
