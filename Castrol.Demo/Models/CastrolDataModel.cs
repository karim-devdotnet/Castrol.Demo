using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Castrol.Demo.Models
{
    public class CastrolDataModel
    {

        [Required(ErrorMessage ="This field is required!"), Display(Name = "Selected Customer Name*")]
        public string CustomerName { get; set; }

        [Display(Name = "Vehicle")]
        public string Vehicle { get; set; }

        [Display(Name = "Model")]
        public string VehicleModel { get; set; }

        /// <summary>
        /// Kennzeichen
        /// </summary>
        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Registration*")]
        public string Registration { get; set; }

        /// <summary>
        /// Work Order Number
        /// </summary>
        [Display(Name = "Work Order Number")]
        public string WIP { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "EVHC Date/Time In*")]
        public string EVHCDateTimeIn { get; set; }

        /// <summary>
        /// Fahrgestellnummer
        /// </summary>
        [Display(Name = "Vin Number")]
        public string VIN { get; set; }

        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Work Phone")]
        public string WorkPhone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Sales Advisor (if allocated)")]
        public string SalesAdvisor { get; set; }

        [Display(Name = "Mileage")]
        public int Mileage { get; set; }

        [Display(Name = "Date Vehicle 1st Registered")]
        public string DateVehicleFirstRegistered { get; set; }

        [Display(Name = "Contact Phone (on the day)")]
        public string ContactPhone { get; set; }

        [Display(Name = "Car Date/Time Due Out")]
        public string CarDateTimeDueOut { get; set; }

        [Display(Name = "Customer Waiting")]
        public int CustomerWaiting { get; set; }

        /// <summary>
        /// Kunden ID
        /// </summary>
        public string UserID { get; set; }

    }
}