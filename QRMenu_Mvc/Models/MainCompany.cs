﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_Mvc.Models
{
    public class MainCompany
    {
            public int Id { get; set; }

            [StringLength(200, MinimumLength = 1)]
            [Column(TypeName = "nvarchar(200)")]
            public string Name { get; set; } = "";

            [StringLength(5, MinimumLength = 1)]
            [Column(TypeName = "char(5)")]
            [DataType(DataType.PostalCode)]
            public string PostalCode { get; set; } = "";

            [StringLength(200, MinimumLength = 1)]
            [Column(TypeName = "nvarchar(200)")]
            public string Address { get; set; } = "";

            [Phone]
            [StringLength(30)]
            [Column(TypeName = "nvarchar(30)")]
            public string Phone { get; set; } = "";

            [EmailAddress]
            [Column(TypeName = "varchar(100)")]
            public string EMail { get; set; } = "";

            [Column(TypeName = "smalldatetime")]
            public DateTime RegisterDate { get; set; }

            [StringLength(11, MinimumLength = 1)]
            [Column(TypeName = "varchar(11)")]
            public string TaxNumber { get; set; } = "";

            [StringLength(100)]
            [Column(TypeName = "varchar(100)")]
            public string? WebbAddress { get; set; }

         
    }
}
