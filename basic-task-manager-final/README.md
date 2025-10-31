# Basic Task Manager (Assignment 1)

A simple full-stack web app using .NET 8 and React + TypeScript.

## Features
- Add, list, toggle, and delete tasks
- In-memory backend (no database)
- React frontend with Axios API calls
- TailwindCSS (via CDN)

## How to Run

### Backend
```bash
cd backend/TaskManager.API
dotnet run
```
Backend runs at `http://localhost:5000`

### Frontend
```bash
cd frontend
npm install
npm start
```
Frontend runs at `http://localhost:3000`

## Notes
- Make sure .NET 8 SDK is installed.
- Tailwind is included via CDN in `public/index.html` for simplicity.
