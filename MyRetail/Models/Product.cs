﻿namespace MyRetail.Rest.Models
{

    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Price CurrentPrice { get; set; }

    }
}