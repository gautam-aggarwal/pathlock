import { useEffect, useState } from "react";
import { api } from "../api";
import TaskItem from "./TaskItem";
import TaskForm from "./TaskForm";

interface Task { id: number; description: string; isCompleted: boolean; }

export default function TaskList() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const load = async () => {
    const res = await api.get("/tasks");
    setTasks(res.data);
  };
  useEffect(() => { load(); }, []);

  return (
    <div className="p-6 max-w-2xl mx-auto">
      <h1 className="text-3xl font-bold mb-4 text-center">Task Manager</h1>
      <div className="bg-white p-4 rounded shadow">
        <TaskForm onAdd={load} />
        <ul className="mt-4 space-y-2">
          {tasks.map(t => (
            <TaskItem key={t.id} task={t} refresh={load} />
          ))}
        </ul>
      </div>
    </div>
  );
}
