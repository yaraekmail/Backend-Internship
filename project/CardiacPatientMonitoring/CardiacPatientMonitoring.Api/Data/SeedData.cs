using CardiacPatientMonitoring.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoring.Api.Data;

// Seeds the database with sample cardiac patient data.
public static class DbSeeder
{
    public static async Task SeedAsync(CardiacPatientMonitoringDbContext context)
    {
        // Prevent duplicate seed data if patients already exist.
        if (await context.Patients.AnyAsync())
            return;

        // Fixed patient IDs so related records can reference the correct patient.
        var patient1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var patient2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var patient3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var patient4Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var patient5Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

        // Sample patients.
        var patients = new List<Patient>
        {
            new Patient
            {
                Id = patient1Id,
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 4, 12),
                Gender = "Male",
                Phone = "555-0101",
                Email = "john.smith@example.com",
                Address = "12 Main Street",
                City = "New York",
                State = "NY"
            },
            new Patient
            {
                Id = patient2Id,
                FirstName = "Sarah",
                LastName = "Johnson",
                DateOfBirth = new DateTime(1978, 9, 25),
                Gender = "Female",
                Phone = "555-0102",
                Email = "sarah.johnson@example.com",
                Address = "45 Oak Avenue",
                City = "Boston",
                State = "MA"
            },
            new Patient
            {
                Id = patient3Id,
                FirstName = "Michael",
                LastName = "Brown",
                DateOfBirth = new DateTime(1990, 1, 18),
                Gender = "Male",
                Phone = "555-0103",
                Email = "michael.brown@example.com",
                Address = "78 Pine Road",
                City = "Chicago",
                State = "IL"
            },
            new Patient
            {
                Id = patient4Id,
                FirstName = "Emily",
                LastName = "Davis",
                DateOfBirth = new DateTime(1969, 6, 7),
                Gender = "Female",
                Phone = "555-0104",
                Email = "emily.davis@example.com",
                Address = "23 Lake Street",
                City = "Seattle",
                State = "WA"
            },
            new Patient
            {
                Id = patient5Id,
                FirstName = "David",
                LastName = "Wilson",
                DateOfBirth = new DateTime(1958, 11, 30),
                Gender = "Male",
                Phone = "555-0105",
                Email = "david.wilson@example.com",
                Address = "91 Park Avenue",
                City = "Denver",
                State = "CO"
            }
        };

        await context.Patients.AddRangeAsync(patients);

        // Sample vital-sign measurements.
        var vitalSigns = new List<VitalSign>
        {
            // John Smith
            new VitalSign
            {
                PatientId = patient1Id,
                RecordedAt = new DateTime(2026, 8, 20, 9, 0, 0),
                HeartRate = 72,
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                OxygenSaturation = 98.0m,
                Temperature = 36.7m,
                RespiratoryRate = 16
            },
            new VitalSign
            {
                PatientId = patient1Id,
                RecordedAt = new DateTime(2026, 8, 21, 9, 0, 0),
                HeartRate = 75,
                SystolicBloodPressure = 122,
                DiastolicBloodPressure = 81,
                OxygenSaturation = 97.0m,
                Temperature = 36.8m,
                RespiratoryRate = 17
            },
            new VitalSign
            {
                PatientId = patient1Id,
                RecordedAt = new DateTime(2026, 8, 22, 9, 0, 0),
                HeartRate = 78,
                SystolicBloodPressure = 125,
                DiastolicBloodPressure = 82,
                OxygenSaturation = 97.5m,
                Temperature = 36.9m,
                RespiratoryRate = 18
            },

            // Sarah Johnson
            new VitalSign
            {
                PatientId = patient2Id,
                RecordedAt = new DateTime(2026, 8, 20, 10, 0, 0),
                HeartRate = 82,
                SystolicBloodPressure = 130,
                DiastolicBloodPressure = 84,
                OxygenSaturation = 97.0m,
                Temperature = 36.6m,
                RespiratoryRate = 18
            },
            new VitalSign
            {
                PatientId = patient2Id,
                RecordedAt = new DateTime(2026, 8, 21, 10, 0, 0),
                HeartRate = 85,
                SystolicBloodPressure = 134,
                DiastolicBloodPressure = 86,
                OxygenSaturation = 96.5m,
                Temperature = 36.8m,
                RespiratoryRate = 19
            },
            new VitalSign
            {
                PatientId = patient2Id,
                RecordedAt = new DateTime(2026, 8, 22, 10, 0, 0),
                HeartRate = 88,
                SystolicBloodPressure = 138,
                DiastolicBloodPressure = 88,
                OxygenSaturation = 96.0m,
                Temperature = 37.0m,
                RespiratoryRate = 20
            },

            // Michael Brown
            new VitalSign
            {
                PatientId = patient3Id,
                RecordedAt = new DateTime(2026, 8, 20, 11, 0, 0),
                HeartRate = 68,
                SystolicBloodPressure = 118,
                DiastolicBloodPressure = 76,
                OxygenSaturation = 99.0m,
                Temperature = 36.5m,
                RespiratoryRate = 15
            },
            new VitalSign
            {
                PatientId = patient3Id,
                RecordedAt = new DateTime(2026, 8, 21, 11, 0, 0),
                HeartRate = 70,
                SystolicBloodPressure = 119,
                DiastolicBloodPressure = 77,
                OxygenSaturation = 98.5m,
                Temperature = 36.6m,
                RespiratoryRate = 16
            },
            new VitalSign
            {
                PatientId = patient3Id,
                RecordedAt = new DateTime(2026, 8, 22, 11, 0, 0),
                HeartRate = 73,
                SystolicBloodPressure = 121,
                DiastolicBloodPressure = 78,
                OxygenSaturation = 98.0m,
                Temperature = 36.7m,
                RespiratoryRate = 16
            },

            // Emily Davis
            new VitalSign
            {
                PatientId = patient4Id,
                RecordedAt = new DateTime(2026, 8, 20, 12, 0, 0),
                HeartRate = 90,
                SystolicBloodPressure = 140,
                DiastolicBloodPressure = 90,
                OxygenSaturation = 95.0m,
                Temperature = 37.1m,
                RespiratoryRate = 21
            },
            new VitalSign
            {
                PatientId = patient4Id,
                RecordedAt = new DateTime(2026, 8, 21, 12, 0, 0),
                HeartRate = 94,
                SystolicBloodPressure = 145,
                DiastolicBloodPressure = 92,
                OxygenSaturation = 94.5m,
                Temperature = 37.2m,
                RespiratoryRate = 22
            },
            new VitalSign
            {
                PatientId = patient4Id,
                RecordedAt = new DateTime(2026, 8, 22, 12, 0, 0),
                HeartRate = 91,
                SystolicBloodPressure = 142,
                DiastolicBloodPressure = 91,
                OxygenSaturation = 95.0m,
                Temperature = 37.0m,
                RespiratoryRate = 21
            },

            // David Wilson
            new VitalSign
            {
                PatientId = patient5Id,
                RecordedAt = new DateTime(2026, 8, 20, 13, 0, 0),
                HeartRate = 76,
                SystolicBloodPressure = 128,
                DiastolicBloodPressure = 82,
                OxygenSaturation = 97.0m,
                Temperature = 36.8m,
                RespiratoryRate = 17
            },
            new VitalSign
            {
                PatientId = patient5Id,
                RecordedAt = new DateTime(2026, 8, 21, 13, 0, 0),
                HeartRate = 79,
                SystolicBloodPressure = 132,
                DiastolicBloodPressure = 84,
                OxygenSaturation = 96.5m,
                Temperature = 36.9m,
                RespiratoryRate = 18
            },
            new VitalSign
            {
                PatientId = patient5Id,
                RecordedAt = new DateTime(2026, 8, 22, 13, 0, 0),
                HeartRate = 81,
                SystolicBloodPressure = 135,
                DiastolicBloodPressure = 85,
                OxygenSaturation = 96.0m,
                Temperature = 37.0m,
                RespiratoryRate = 18
            }
        };

        // Sample medications.
        var medications = new List<Medication>
        {
            new Medication
            {
                PatientId = patient1Id,
                Name = "Aspirin",
                Dosage = "81 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 1, 10)
            },
            new Medication
            {
                PatientId = patient2Id,
                Name = "Atorvastatin",
                Dosage = "20 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 2, 15)
            },
            new Medication
            {
                PatientId = patient3Id,
                Name = "Lisinopril",
                Dosage = "10 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 3, 5)
            },
            new Medication
            {
                PatientId = patient4Id,
                Name = "Metoprolol",
                Dosage = "25 mg",
                Frequency = "Twice daily",
                StartDate = new DateTime(2026, 1, 20)
            },
            new Medication
            {
                PatientId = patient5Id,
                Name = "Amlodipine",
                Dosage = "5 mg",
                Frequency = "Once daily",
                StartDate = new DateTime(2026, 4, 1)
            }
        };

        // Sample appointments.
        var appointments = new List<Appointment>
        {
            new Appointment
            {
                PatientId = patient1Id,
                AppointmentDate = new DateTime(2026, 9, 5, 10, 0, 0),
                Reason = "Cardiac follow-up",
                Status = "Scheduled",
                Notes = "Routine cardiac monitoring"
            },
            new Appointment
            {
                PatientId = patient2Id,
                AppointmentDate = new DateTime(2026, 9, 7, 11, 0, 0),
                Reason = "Blood pressure check",
                Status = "Scheduled",
                Notes = "Review recent vital signs"
            },
            new Appointment
            {
                PatientId = patient3Id,
                AppointmentDate = new DateTime(2026, 9, 10, 9, 30, 0),
                Reason = "Routine cardiac check",
                Status = "Scheduled",
                Notes = null
            },
            new Appointment
            {
                PatientId = patient4Id,
                AppointmentDate = new DateTime(2026, 9, 12, 14, 0, 0),
                Reason = "Heart rate evaluation",
                Status = "Scheduled",
                Notes = "Review heart rate trend"
            },
            new Appointment
            {
                PatientId = patient5Id,
                AppointmentDate = new DateTime(2026, 9, 15, 10, 30, 0),
                Reason = "Cardiology consultation",
                Status = "Scheduled",
                Notes = "Regular follow-up appointment"
            }
        };

        // Sample medical conditions.
        var medicalConditions = new List<MedicalCondition>
        {
            new MedicalCondition
            {
                PatientId = patient1Id,
                Name = "Hypertension",
                DiagnosedAt = new DateTime(2024, 5, 10),
                Notes = "Requires regular blood pressure monitoring"
            },
            new MedicalCondition
            {
                PatientId = patient2Id,
                Name = "Hyperlipidemia",
                DiagnosedAt = new DateTime(2023, 8, 15),
                Notes = "Managed with medication"
            },
            new MedicalCondition
            {
                PatientId = patient3Id,
                Name = "Asthma",
                DiagnosedAt = new DateTime(2022, 3, 20),
                Notes = "Stable condition"
            },
            new MedicalCondition
            {
                PatientId = patient4Id,
                Name = "Hypertension",
                DiagnosedAt = new DateTime(2021, 11, 5),
                Notes = "Regular monitoring recommended"
            },
            new MedicalCondition
            {
                PatientId = patient5Id,
                Name = "Coronary Artery Disease",
                DiagnosedAt = new DateTime(2020, 7, 12),
                Notes = "Under cardiac follow-up"
            }
        };

        // Sample allergies.
        var allergies = new List<Allergy>
        {
            new Allergy
            {
                PatientId = patient1Id,
                Allergen = "Penicillin",
                Severity = "Moderate",
                Notes = "Skin reaction"
            },
            new Allergy
            {
                PatientId = patient2Id,
                Allergen = "Peanuts",
                Severity = "Severe",
                Notes = "Avoid exposure"
            },
            new Allergy
            {
                PatientId = patient3Id,
                Allergen = "Dust",
                Severity = "Mild",
                Notes = "Seasonal symptoms"
            },
            new Allergy
            {
                PatientId = patient4Id,
                Allergen = "Sulfa drugs",
                Severity = "Moderate",
                Notes = "Medication-related reaction"
            },
            new Allergy
            {
                PatientId = patient5Id,
                Allergen = "Latex",
                Severity = "Mild",
                Notes = null
            }
        };

        // Add all related data to the database.
        await context.VitalSigns.AddRangeAsync(vitalSigns);
        await context.Medications.AddRangeAsync(medications);
        await context.Appointments.AddRangeAsync(appointments);
        await context.MedicalConditions.AddRangeAsync(medicalConditions);
        await context.Allergies.AddRangeAsync(allergies);

        // Save all seeded data.
        await context.SaveChangesAsync();
    }
}