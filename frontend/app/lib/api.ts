const API_BASE = "http://localhost:5000/api";

export async function api<T>(path: string, options?: RequestInit): Promise<T> {
  const res = await fetch(`${API_BASE}${path}`, {
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
      ...options?.headers,
    },
    ...options,
  });

  if (!res.ok) {
    const body = await res.json().catch(() => ({}));
    throw new Response(JSON.stringify(body), { status: res.status });
  }

  if (res.status === 204) return undefined as T;
  return res.json();
}
