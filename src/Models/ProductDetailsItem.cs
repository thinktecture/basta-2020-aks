﻿using System;

namespace Thinktecture.AKS.Sample.Models
{
    public class ProductDetailsItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public Meta Meta => new Meta();
    }
}
