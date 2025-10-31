import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../api";

export default function Dashboard() {
  const [projects, setProjects] = useState<any[]>([]);
  const [title, setTitle] = useState("");
  const [desc, setDesc] = useState("");
  const navigate = useNavigate();

  const load = async () => {
    try {
      const res = await api.get("/projects");
      setProjects(res.data);
    } catch { /* ignore */ }
  };

  useEffect(()=>{ load(); }, []);

  const add = async () => {
    if(!title.trim()) return;
    await api.post("/projects", { title, description: desc });
    setTitle(""); setDesc("");
    load();
  };

  const del = async (id:number) => {
    await api.delete(`/projects/${id}`);
    load();
  };

  const logout = () => { localStorage.removeItem("token"); navigate("/"); };

  return (
    <div className="max-w-3xl mx-auto p-6">
      <div className="flex justify-between items-center mb-4">
        <h1 className="text-2xl font-bold">Dashboard</h1>
        <button onClick={logout} className="px-3 py-2 border rounded">Logout</button>
      </div>
      <div className="bg-white p-4 rounded shadow mb-4">
        <input className="w-full p-2 border rounded mb-2" placeholder="Project title" value={title} onChange={e=>setTitle(e.target.value)} />
        <textarea className="w-full p-2 border rounded mb-2" placeholder="Description" value={desc} onChange={e=>setDesc(e.target.value)} />
        <div className="flex gap-2"><button className="bg-blue-600 text-white px-4 py-2 rounded" onClick={add}>Create</button></div>
      </div>
      <div className="space-y-2">
        {projects.map(p=> (
          <div key={p.id} className="bg-white p-3 rounded shadow flex justify-between items-center">
            <div>
              <Link to={`/project/${p.id}`} className="font-semibold">{p.title}</Link>
              <div className="text-sm text-gray-600">{p.description}</div>
            </div>
            <div className="flex gap-2">
              <button onClick={()=>del(p.id)} className="text-red-600">Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
