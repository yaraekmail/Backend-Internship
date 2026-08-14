# Week 3 - Day 2

## SQL Server Schema Design & Normalization

### 📚 Learning Objectives

* Understand why database normalization is important.
* Apply First, Second, and Third Normal Form (1NF, 2NF, 3NF).
* Define primary keys and foreign keys correctly.
* Model relationships between database tables.
* Choose appropriate SQL Server data types.

---

## 🛠️ Project

**Student Personal Development & Task Management**

This task focused on designing a normalized relational database schema based on the REST resources defined in Day 1.

---

## 📄 Documentation

[📄 View Day 2 Documentation](./Week3_Day2_Task.docx)

---

## 🗄️ Database Design

The schema was designed around the main resources and relationships required by the project.

### Main Entities

* Participants
* Trainings
* TrainingParticipants
* TrainingDays
* Trainers
* Companies
* Courses
* Instructors
* Universities
* Semesters
* IslamicGoals
* Tasks
* Skills
* CourseSkills
* TrainingSkills

---

## 🔑 Keys & Relationships

Primary keys were defined to uniquely identify records in each table.

Foreign keys were used to connect related entities and maintain referential integrity.

Many-to-many relationships were handled using junction tables such as:

* `TrainingParticipants`
* `CourseSkills`
* `TrainingSkills`

---

## 📐 Normalization

### 1NF — First Normal Form

The database design follows 1NF by keeping values atomic and avoiding multiple values or comma-separated lists inside a single column.

### 2NF — Second Normal Form

The design ensures that non-key attributes depend on the complete primary key, which is particularly important for tables involving composite relationships.

### 3NF — Third Normal Form

The design separates attributes that depend on other non-key attributes, ensuring that each fact is stored in the appropriate table and reducing update anomalies.

---

## 🔗 Relationships

The database includes:

* One-to-many relationships.
* Many-to-many relationships through junction tables.
* Foreign-key relationships between related entities.

The schema was planned before implementing it in the backend to ensure that the database structure was normalized and consistent.

---

## 🧱 Data Types

Appropriate SQL Server data types were considered for each attribute.

Examples include:

* `int` for numeric identifiers.
* `nvarchar` for text values.
* `decimal` for monetary values where applicable.
* `datetime` / `datetime2` for date and time values.

The goal was to choose data types that accurately represent the stored data without unnecessary storage overhead.

---

## 📊 ERD

The database relationships were represented using an Entity Relationship Diagram (ERD) to visualize the tables, keys, and relationships before implementation.

---

## 🧰 Tools Used

* SQL Server
* SQL Server Management Studio / database tools
* dbdiagram.io
* VS Code
* .NET

---

## 🚀 Skills Gained

* Database schema design.
* SQL Server relational database concepts.
* Database normalization.
* Applying 1NF, 2NF, and 3NF.
* Defining primary and foreign keys.
* Modeling one-to-many and many-to-many relationships.
* Choosing appropriate SQL Server data types.
* Designing an ERD before backend implementation.

---

## 📁 Project Structure

```text
Day2/
├── README.md
└── Week3_Day2_Task.docx
```

[📄 Open the complete Day 2 documentation](./Week3_Day2_Task.docx)
