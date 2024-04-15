using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }     
        [DisplayName("Tên sản phẩm")]
        [Required]
        public string ProductName { get; set; }      
        [DisplayName("Trọng lượng")]
        public double? Weight { get; set; }
        [DisplayName("Hình thức đóng gói")]
        public string? TypeClosure {  get; set; }
        [DisplayName("Hạn sử dụng")]
        public DateTime EXP {  get; set; }
        [DisplayName("Ngày sản xuất")]
        public DateTime MFG { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        [DisplayName("Giá Bán")]
        public double Priced { get; set; }
        [DisplayName("Giá Nhập")]
        public double? ListedPriced { get; set; }
        [DisplayName("Giảm giá")]
        public int? SaleId { get; set; }
   
        public virtual Sale? Sale { get; set; }
        [DisplayName("Trạng thái")]
        public byte Status { get; set; } = 1;
        [DisplayName("Mô tả")]
        public string? ProductDescription { get; set; }

        [DisplayName("Ảnh")]
        public string? ImageUrl { get; set; }

        [DisplayName("Loại sản phẩm")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual CartItem? CartItem { get; set; }

        public virtual InvoiceDetail? InvoiceDetail { get; set; }


    }
}

