import { api } from "~/lib/api";

export async function login(credentials: { email: string; password: string }) {
  return api("/auth/login", {
    method: "POST",
    body: JSON.stringify(credentials),
  });
}
