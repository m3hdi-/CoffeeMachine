using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace CoffeeMachine.Models.Models
{
    public partial class Dose
    {

        [JsonIgnore]
        public int Id { get; set; }
        
        public string Type { get; set; }
        
        public bool IsMug { get; set; }
        
        public bool? IsBadge { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
       
        public string User { get; set; }
    }
}
