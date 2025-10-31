import React, { ReactElement, ReactNode } from "react";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./routes/Login";
import Register from "./routes/Register";
import Dashboard from "./routes/Dashboard";
import ProjectDetail from "./routes/ProjectDetail";

interface RequireAuthProps {
  children?: ReactNode; // ✅ make optional
}

function RequireAuth({ children }: RequireAuthProps): ReactElement {
  const token = localStorage.getItem("token");
  return token ? <>{children}</> : <Navigate to="/" replace />;
}

export default function App(): ReactElement {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route
          path="/dashboard"
          element={
            <RequireAuth>
              <Dashboard />
            </RequireAuth>
          }
        />
        <Route
          path="/project/:id"
          element={
            <RequireAuth>
              <ProjectDetail />
            </RequireAuth>
          }
        />
      </Routes>
    </BrowserRouter>
  );
}
