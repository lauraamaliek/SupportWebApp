using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SupportWebApp.Models;

public class SupportMessage
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Navn er påkrævet")]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email er påkrævet")]
    [EmailAddress(ErrorMessage = "Ugyldig emailadresse")]
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon er påkrævet")]
    [Phone(ErrorMessage = "Ugyldigt telefonnummer")]
    [JsonProperty("phone")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beskrivelse er påkrævet")]
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kategori er påkrævet")]
    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;

    [JsonProperty("dateTime")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}