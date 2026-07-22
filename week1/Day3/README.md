# Day 3 - C# Fundamentals II: Object-Oriented Programming

## Overview
This project demonstrates the main Object-Oriented Programming concepts in C#:
- Classes, Records, and Structs
- Encapsulation and Access Modifiers
- Inheritance and Interfaces
- Polymorphism

---

## Domain Model
### Library Management System

The domain contains:

### Classes
#### Book
Represents a book entity with:
- BookId
- Title
- Author

Implements `IPrint` interface to display book details.

#### Member
Represents a library member with:
- MemberId
- Name
- Email

Implements `IPrint` interface to display member details.

---

## Encapsulation
Encapsulation is applied by:
- Using private fields.
- Exposing data through public properties.
- Using constructors to initialize valid object states.

Example:
```csharp
public class Book
{
    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }
}