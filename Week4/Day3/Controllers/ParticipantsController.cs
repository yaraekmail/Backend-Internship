using Microsoft.AspNetCore.Authorization; // NEW
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// This controller handles requests related to participants.
[ApiController]
[Route("api/[controller]")]
[Authorize] // NEW
public class ParticipantsController : ControllerBase
{
    // DbContext used to access the database.
    private readonly TrainingManagementDbContext _context;

    // ASP.NET Core provides the DbContext using Dependency Injection.
    public ParticipantsController(TrainingManagementDbContext context)
    {
        _context = context;
    }

    // POST: api/participants
    // This endpoint creates a new participant.
    [HttpPost]
    public async Task<IActionResult> Create(CreateParticipantRequest request)
    {
        // Create a new participant using the data sent by the client.
        var participant = new Participant
        {
            Name = request.Name,
            Email = request.Email
        };

        // Add the new participant to the DbContext.
        // It is not saved to the database yet.
        _context.Participants.Add(participant);

        // Save the new participant to the database.
        await _context.SaveChangesAsync();

        // Return 201 Created with the created participant.
        return Created(
            $"/api/participants/{participant.Id}",
            participant
        );
    }

    // GET: api/participants
    // This endpoint gets all participants from the database.
    [Authorize(Policy = "AdminWithEmailPolicy")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get all participants from the Participants table.
        // ToListAsync() runs the database query without blocking the application.
        var participants = await _context.Participants.ToListAsync();

        // Return 200 OK with the list of participants.
        return Ok(participants);
    }

    // GET: api/participants/1
    // This endpoint gets one participant by its ID.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Find the participant with the given ID.
        // FirstOrDefaultAsync() returns the participant if it exists,
        // or null if no participant has this ID.
        var participant = await _context.Participants
            .FirstOrDefaultAsync(p => p.Id == id);

        // If the participant does not exist, return 404 Not Found.
        if (participant == null)
        {
            return NotFound();
        }

        // If the participant exists, return 200 OK with the participant.
        return Ok(participant);
    }

    // PUT: api/participants/1
    // This endpoint updates an existing participant.
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateParticipantRequest request)
    {
        // Find the participant by its ID.
        // EF Core tracks the participant after loading it.
        var participant = await _context.Participants
            .FirstOrDefaultAsync(p => p.Id == id);

        // If the participant does not exist, return 404 Not Found.
        if (participant == null)
        {
            return NotFound();
        }

        // Update the participant's properties with the new values.
        participant.Name = request.Name;
        participant.Email = request.Email;

        // Save the changes to the database.
        // EF Core detects which properties were changed
        // and sends an UPDATE command to SQL Server.
        await _context.SaveChangesAsync();

        // Return 200 OK with the updated participant.
        return Ok(participant);
    }

    // DELETE: api/participants/1
    // This endpoint deletes an existing participant.
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Find the participant by its ID.
        // EF Core tracks the participant after loading it.
        var participant = await _context.Participants
            .FirstOrDefaultAsync(p => p.Id == id);

        // If the participant does not exist, return 404 Not Found.
        if (participant == null)
        {
            return NotFound();
        }

        // Mark the participant for deletion.
        _context.Participants.Remove(participant);

        // Save the deletion to the database.
        await _context.SaveChangesAsync();

        // Return 204 No Content because the deletion was successful.
        return NoContent();
    }
}