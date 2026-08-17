using System.ComponentModel.DataAnnotations;

// This class contains the data needed to update a participant.
public class UpdateParticipantRequest
{
    // The participant name is required.
    [Required]
    public string Name { get; set; }

    // The participant email is required.
    [Required]

    // Check that the value is a valid email address.
    [EmailAddress]
    public string Email { get; set; }
}
