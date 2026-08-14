# Week 3 - Day 5

## Testing & Documenting the API with Postman

## Overview

Day 5 focused on testing and documenting the **Training Management API** using Postman.

The practical work included creating an organized Postman Collection, testing successful and error scenarios, adding automated status-code checks, creating a Postman Environment with a `baseUrl` variable, and exporting the completed collection as JSON.

---

## API Tested

The API tested during Day 5 is the **Participants API**.

### Main Endpoints

| Method | Endpoint                 | Success | Error |
| ------ | ------------------------ | ------: | ----: |
| GET    | `/api/participants`      |     200 |     — |
| GET    | `/api/participants/{id}` |     200 |   404 |
| POST   | `/api/participants`      |     201 |   400 |
| PUT    | `/api/participants/{id}` |     200 |   404 |
| DELETE | `/api/participants/{id}` |     204 |   404 |

---

## Postman Collection

A Postman Collection was created to organize all Participant requests.

```text
Week 3 - Day 5 - Training Management API
└── Participants
    ├── Get All Participants - Success
    ├── Get Participant By ID - Success
    ├── Get Participant By ID - Not Found
    ├── Create Participant - Success
    ├── Create Participant - Error
    ├── Update Participant - Success
    ├── Update Participant - Not Found
    ├── Delete Participant - Success
    └── Delete Participant - Not Found
```

The collection covers both successful and error scenarios for the main CRUD operations.

[📦 Open the exported Postman Collection](./Postman/Week3_Day5_TrainingManagement_Postman.json)

---

## Success & Error Testing

### Success Paths

* `GET /api/participants` → `200 OK`
* `GET /api/participants/{id}` → `200 OK`
* `POST /api/participants` → `201 Created`
* `PUT /api/participants/{id}` → `200 OK`
* `DELETE /api/participants/{id}` → `204 No Content`

### Error Paths

* Non-existing participant → `404 Not Found`
* Invalid create request → `400 Bad Request`
* Update non-existing participant → `404 Not Found`
* Delete non-existing participant → `404 Not Found`

---

## Postman Test Scripts

Basic automated status-code tests were added using Postman's **Post-response** scripts.

### GET

```javascript
pm.test("Status code is 200", () => {
    pm.response.to.have.status(200);
});
```

### POST

```javascript
pm.test("Status code is 201", () => {
    pm.response.to.have.status(201);
});
```

### DELETE

```javascript
pm.test("Status code is 204", () => {
    pm.response.to.have.status(204);
});
```

These tests automatically verify that the API returns the expected status code.

---

## Environment & Variables

A local Postman Environment was created with:

```text
baseUrl = http://localhost:5042
```

The requests were updated to use:

```text
{{baseUrl}}/api/participants
```

instead of repeating the complete local URL.

This keeps the collection easier to reuse and maintain.

---

## Project Files

### API Code

* [Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Program.cs)
* [ParticipantsController.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Controllers/ParticipantsController.cs)
* [TrainingManagementDbContext.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Data/TrainingManagementDbContext.cs)

### Request Models

* [CreateParticipantRequest.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Models/CreateParticipantRequest.cs)
* [UpdateParticipantRequest.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Models/UpdateParticipantRequest.cs)
* [Participant.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Models/Participant.cs)

### Postman

* [Exported Postman Collection](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Postman/Week3_Day5_TrainingManagement_Postman.json)

### Documentation

* [Day 5 Documentation](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Documentation/Week_3-Day5-Documentation.docx)

---

## Skills Practiced

* Postman Collections
* API Request Organization
* Success Path Testing
* Error Path Testing
* HTTP Status Code Validation
* Postman Test Scripts
* Postman Environments
* Variables and `{{baseUrl}}`
* API Testing Documentation
* Exporting Postman Collections

---

## Day 5 Outcome

By the end of Day 5, the **Participants API** was tested systematically through an organized Postman Collection covering its main CRUD operations.

Both success and error scenarios were tested, automated status-code checks were added, a reusable local environment was configured, and the complete Postman Collection was exported as JSON.

### 🔗 Quick Links

[📦 Postman Collection](./Postman/Week3_Day5_TrainingManagement_Postman.json)
[📄 Day 5 Documentation](./Documentation/Week_3-Day5-Documentation.docx)
[💻 Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Program.cs)
[🎮 ParticipantsController.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Controllers/ParticipantsController.cs)
[🗄️ TrainingManagementDbContext.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week3/Day5/Data/TrainingManagementDbContext.cs)
