# Lost & Found Portal — Fergusson College

A web-based Lost and Found Portal developed for Fergusson College, Pune. 
Students can register, report lost items, register found items, 
search for lost items and mark items as resolved.

## Tech Stack
- ASP.NET Core MVC
- C#
- Entity Framework Core
- MySQL
- Razor Views

## Features
- Student Registration and Login
- Session based authentication
- Report Lost Items with image upload
- Register Found Items with image upload
- Search Lost Items by name or location
- Mark item as resolved (only by the poster)
- Login required popup for protected features
- Responsive UI

## Setup
1. Clone the repository
2. Add your MySQL connection string
3. Run database migrations or use your existing MySQL schema
4. Run the project using Visual Studio or dotnet CLI

## Database
Uses MySQL with three tables — user, lost_items, found_items
