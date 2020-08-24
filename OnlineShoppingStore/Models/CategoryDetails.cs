using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnlineShoppingStore.Models
{
    public class CategoryDetails
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category Name required")]
        [StringLength(100,ErrorMessage ="Minimum 5 and maximum 100 characters",MinimumLength =3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }

    }

    public class ProductDetails
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Product Name required")]
        [StringLength(100, ErrorMessage = "Minimum 5 and maximum 100 characters", MinimumLength = 3)]
        public string productName { get; set; }
        [Required]
        [Range(1,50)]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required(ErrorMessage ="Description is required")]
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        [Required]
        [Range(typeof(int),"1","500",ErrorMessage ="Invalid quantity")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(decimal),"1","200000",ErrorMessage ="Invalid Price")]
        
        public Nullable<decimal> Price { get; set; }
        public SelectList Categories { get; set; }
    }
}