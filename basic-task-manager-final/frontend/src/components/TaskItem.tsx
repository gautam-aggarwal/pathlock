import { api } from "../api";

export default function TaskItem({ task, refresh }: any) {
  const toggle = async () => {
    await api.patch(`/tasks/${task.id}/toggle`);
    refresh();
  };
  const del = async () => {
    await api.delete(`/tasks/${task.id}`);
    refresh();
  };
  return (
    <li className="flex justify-between items-center border p-2 text-black rounded bg-white">
      <span
        onClick={toggle}
        className={task.isCompleted ? "line-through cursor-pointer" : "cursor-pointer"}
      >
        {task.description}
      </span>
      <button onClick={del} className="bg-purple-600 hover:bg-purple-700 text-black px-4 py-2 rounded">Delete</button>
    </li>
  );
}
