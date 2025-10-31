import React, { useState } from "react";
import { api } from "../api";

export default function TaskForm({ onAdd }: { onAdd: () => void }) {
  const [desc, setDesc] = useState("");
  const submit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!desc.trim()) return;
    await api.post("/tasks", { description: desc });
    setDesc("");
    onAdd();
  };
  return (
    <form onSubmit={submit} className="flex gap-2">
      <input
        className="border p-2 text-black flex-grow rounded"
        placeholder="New task..."
        value={desc}
        onChange={(e) => setDesc(e.target.value)}
      />
      <button className="bg-blue-600 text-black px-4 py-2 rounded">Add</button>
    </form>
  );
}
