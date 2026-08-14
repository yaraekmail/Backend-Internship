
# 📅 Week 1 - Day 5 | Git & GitHub Workflow

> This day focused on Git and GitHub workflow practices and served as the Week 1 close-out and synthesis.

---

## 📖 Overview

Day 5 focused on understanding and practicing **Git and GitHub workflow fundamentals** used for managing source code in a professional development environment.

The day also served as the **Week 1 synthesis**, bringing together the C# fundamentals, Object-Oriented Programming, Collections, LINQ, and asynchronous programming concepts covered throughout the week.

---

## 📚 Topics Covered

### 🔹 Git Fundamentals

Git is used to track changes in a codebase through commits.

The main Git workflow includes:

```text
git add
     ↓
git commit
     ↓
git push
````

These commands are used to stage changes, create commits, and synchronize the local repository with GitHub.

---

### 🔀 Feature Branch Workflow

The feature-branch workflow keeps development work separated from the `main` branch.

Example workflow:

```bash
git checkout -b feature/week1-oop-domain
git add .
git commit -m "Add domain model with User and Order classes"
git push -u origin feature/week1-oop-domain
```

The purpose of using feature branches is to keep the `main` branch stable while development work is being completed separately.

---

### 📝 Good Commit Messages

Commit messages should clearly describe what was changed.

A good commit message should:

* Be specific.
* Describe the actual change.
* Use an imperative style such as `Add`, `Fix`, or `Refactor`.
* Make the project history easier to understand.

For example:

```text
Add domain model with User and Order classes
```

is more informative than:

```text
fix stuff
```

---

### 🔍 Pull Requests

A Pull Request (PR) is used to propose merging changes from a feature branch into `main`.

A good Pull Request description should explain:

* What was changed.
* Why the changes were made.
* Any questions or issues that need review.

The goal is to make the work easy for a mentor or teammate to review.

---

## 🧪 Hands-On Lab

The Day 5 hands-on lab focused on the Git workflow and closing Week 1.

### Task 1 — Git Setup

The Week 1 project was initialized and the GitHub remote was verified.

### Task 2 — Feature Branch

A feature branch was created for the Week 1 work.

The Day 2–4 work was committed using descriptive commit messages and pushed to GitHub.

### Task 3 — Pull Request

A Pull Request was opened from the feature branch into the `main` branch.

The Pull Request included a description summarizing the Week 1 work.

### Task 4 — Mentor Review

The workflow included requesting the mentor as a reviewer for the Pull Request.

### Task 5 — Week 1 Synthesis

The Week 1 work brings together the following topics:

* C# Fundamentals
* Types and Variables
* Control Flow
* Object-Oriented Programming
* Collections
* LINQ
* Async/Await
* Exception Handling
* Git and GitHub Workflow

---

## 📂 Project Structure

```text
Day5/
│
├── Program.cs
└── Day5.csproj
```

---

## 💻 Program

The Day 5 project contains the basic console application created for the day's work.

[Open Program.cs](./Program.cs)

---

## 🛠 Technologies and Tools

* C#
* .NET
* Git
* GitHub
* Visual Studio Code

---

## 🎯 Learning Outcomes

By completing Day 5, I practiced:

* Using Git to track changes.
* Staging changes with `git add`.
* Creating commits with `git commit`.
* Pushing changes to GitHub.
* Working with feature branches.
* Writing clear commit messages.
* Creating Pull Requests.
* Preparing work for mentor review.
* Reviewing and synthesizing the concepts covered during Week 1.

---

## 🔗 Quick Links

| Resource      | Link                              |
| ------------- | --------------------------------- |
| Program.cs    | [Open Program.cs](./Program.cs)   |
| Day 5 Project | [Open Day5.csproj](./Day5.csproj) |

---

## 📌 Project Information

| Item                | Details               |
| ------------------- | --------------------- |
| Week                | 1                     |
| Day                 | 5                     |
| Main Topic          | Git & GitHub Workflow |
| Additional Focus    | Week 1 Synthesis      |
| Language            | C#                    |
| Framework           | .NET                  |
| Version Control     | Git                   |
| Repository Platform | GitHub                |

