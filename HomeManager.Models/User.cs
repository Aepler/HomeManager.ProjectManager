﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public bool Darkmode { get; set; }

        public byte[] Picture { get; set; }

        public string DataType { get; set; }

        public double StartBalance { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Payment_Template> Payment_Templates { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Type> types { get; set; }
        public ICollection<Status> statuses { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
