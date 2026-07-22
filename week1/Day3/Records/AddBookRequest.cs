//task2 Add at least one record type for an immutable data transfer object in your domain

public record AddBookRequest(string Title, int Pages, int AuthorId);