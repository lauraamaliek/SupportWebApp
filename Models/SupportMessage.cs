using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SupportWebApp.Models;

public class SupportMessage
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Navn er påkrævet")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email er påkrævet")]
    [EmailAddress(ErrorMessage = "Ugyldig emailadresse")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon er påkrævet")]
    [Phone(ErrorMessage = "Ugyldigt telefonnummer")]
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beskrivelse er påkrævet")]
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kategori er påkrævet")]
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("dateTime")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}