import { api } from "~/lib/api";
import type { components } from "~/lib/api-types";

type Appointment = components["schemas"]["Appointment"];

export async function getMe() {
  return api<{ email: string }>("/auth/me");
}

export async function getAppointments() {
  return api<Appointment[]>("/appointments");
}

export async function approveAppointment(id: FormDataEntryValue | null) {
  return api(`/appointments/${id}/approve`, { method: "PATCH" });
}

export async function deleteAppointment(id: FormDataEntryValue | null) {
  return api(`/appointments/${id}`, { method: "DELETE" });
}

export async function logout() {
  return api("/auth/logout", { method: "POST" });
}
