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

        [Required(ErrorMessage = "\"{0}\" ist erforderlich."), Display(Name = "Name*")]
        public string CustomerName { get; set; }

        [Display(Name = "Hersteller")]
        public string Vehicle { get; set; }

        [Display(Name = "Modell")]
        public string VehicleModel { get; set; }

        /// <summary>
        /// Kennzeichen
        /// </summary>
        [Required(ErrorMessage = "\"{0}\" ist erforderlich.")]
        [Display(Name = "Kennzeichen*")]
        public string Registration { get; set; }

        /// <summary>
        /// Work Order Number
        /// </summary>
        [Display(Name = "Reihenfolge")]
        public string WIP { get; set; }

        [Required(ErrorMessage = "\"{0}\" ist erforderlich.")]
        [Display(Name = "Datum/Uhrzeit*")]
        public string EVHCDateTimeIn { get; set; }

        /// <summary>
        /// Fahrgestellnummer
        /// </summary>
        [Display(Name = "Fahrgestell-Nr (VIN)")]
        public string VIN { get; set; }

        [Display(Name = "Telefon")]
        public string HomePhone { get; set; }

        [Display(Name = "Telefon (Mobil)")]
        public string Mobile { get; set; }

        [Display(Name = "Telefon (Arbeit)")]
        public string WorkPhone { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Verkaufsberater")]
        public string SalesAdvisor { get; set; }

        [Display(Name = "Kilometerstand")]
        public int Mileage { get; set; }

        [Display(Name = "Erstzulassung")]
        public string DateVehicleFirstRegistered { get; set; }

        [Display(Name = "Telefon (heute)")]
        public string ContactPhone { get; set; }

        [Display(Name = "Fertigstellung bis")]
        public string CarDateTimeDueOut { get; set; }

        [Display(Name = "Kunde wartet")]
        public int CustomerWaiting { get; set; }

        /// <summary>
        /// Kunden ID
        /// </summary>
        [Required(ErrorMessage ="\"{0}\" ist erforderlich."), Display(Name = "Benutzer-ID*")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "\"{0}\" ist erforderlich."), Display(Name = "FTP Benutzername*")]
        public string UserName { get; set; }

        [Display(Name = "FTP Passwort")]
        public string UserPassword { get; set; }

        public bool CreateNewFTPCredentials { get; set; }

        public bool ShowFTPLoginData { get; set; }

    }
}