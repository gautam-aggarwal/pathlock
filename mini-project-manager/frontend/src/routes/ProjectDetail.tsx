import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../api";

export default function ProjectDetail() {
  const { id } = useParams();
  const [tasks, setTasks] = useState<any[]>([]);
  const [title, setTitle] = useState("");
  const load = async () => {
    const res = await api.get(`/projects`);
    const p = res.data.find((x:any)=>x.id.toString()===id);
    if(p) setTasks(p.tasks || []);
  };
  useEffect(()=>{ load(); }, [id]);

  const add = async () => {
    if(!title.trim()) return;
    await api.post(`/projects/${id}/tasks`, { title });
    setTitle(""); load();
  };
  const toggle = async (tid:number) => { await api.patch(`/projects/${id}/tasks/${tid}/toggle`); load(); };
  const del = async (tid:number) => { await api.delete(`/projects/${id}/tasks/${tid}`); load(); };

  return (
    <div className="max-w-2xl mx-auto p-6">
      <h1 className="text-2xl font-bold mb-4">Project Tasks</h1>
      <div className="bg-white p-4 rounded shadow mb-4">
        <input className="w-full p-2 border rounded mb-2" placeholder="Task title" value={title} onChange={e=>setTitle(e.target.value)} />
        <div className="flex gap-2">
          <button className="bg-blue-600 text-white px-4 py-2 rounded" onClick={add}>Add Task</button>
        </div>
      </div>
      <ul className="space-y-2">
        {tasks.map(t=> (
          <li key={t.id} className="bg-white p-3 rounded shadow flex justify-between items-center">
            <div onClick={()=>toggle(t.id)} className={t.isCompleted ? "line-through cursor-pointer" : "cursor-pointer"}>{t.title}</div>
            <div className="flex gap-2">
              <button onClick={()=>del(t.id)} className="text-red-600">Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
