🏥 Doctor Appointment Booking System (3-Tier Architecture)
A structured and secure web application built with ASP.NET Core Razor Pages using the 3-Tier Architecture approach. This system streamlines the doctor-patient appointment process, enabling users to book appointments efficiently, input symptoms and medical history, and manage their accounts securely.

✨ Key Features
🧠 Symptom & Medical History Input: Patients can provide details before visits, aiding in accurate diagnosis.

🔐 Secure Authentication using ASP.NET Identity

🔑 Forgot Password with Email Verification: Token-based reset system ensures account safety.

🩺 Role-Based Authorization: Separate access for Admins, Doctors, and Patients.

📅 Appointment Scheduling: Easy booking and management of appointments between doctors and patients.

🧰 Clean Architecture (3-Tier): Separation of concerns across UI, Business Logic, and Data Access layers.

🗂️ Repository & Unit of Work Patterns: For organized and testable data handling.

🛠️ Technologies Used
Frontend: Razor Pages + Bootstrap

Backend: ASP.NET Core (.NET 6+)

Architecture: 3-Tier (Presentation, Business Logic, Data Access)

Database: SQL Server + Entity Framework Core

Authentication: ASP.NET Identity with Token-Based Email Verification

Dependency Injection

AutoMapper

Role-Based Authorization

📂 Project Layers
Presentation Layer: Razor Pages (UI & user interaction)

Business Logic Layer (BLL): Application logic, services, and validation

Data Access Layer (DAL): Repository + Unit of Work + EF Core for DB operations

📌 Future Improvements
Email notifications for upcoming appointments

Medical report uploads

Real-time chat between patients and doctors

Admin dashboard with analytics

📬 Contact
For contributions, issues, or inquiries, feel free to open a GitHub issue or contact me directly
