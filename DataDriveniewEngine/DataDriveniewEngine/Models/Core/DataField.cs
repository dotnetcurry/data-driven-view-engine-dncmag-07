using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DataDrivenViewEngine.Models.Core
{
    public class DataField
    {
        public int Id { get; set; }
        [Required]
        public string FieldName { get; set; }
        [Required]
        public string DisplayLabel { get; set; }
        [Required]
        public FieldType DisplayType { get; set; }
        [Required]
        public bool IsMandatory { get; set; }
        
        public int FormId { get; set; }
        [ForeignKey("FormId")]
        [ScriptIgnore]
        public DataForm ParentForm { get; set; }
    }
}