//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionClinic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Appointement
    {
        public int Ap_ID { get; set; }
        public System.DateTime Date { get; set; }
        public int patient_ID { get; set; }
        public int doctor_ID { get; set; }
        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode =)]
        public System.TimeSpan Time { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
