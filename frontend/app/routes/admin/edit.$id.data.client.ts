import { api } from "~/lib/api";
import type { Appointment } from "~/lib/types";

export async function getMe() {
  return api<{ email: string }>("/auth/me");
}

export async function getAppointment(id: string) {
  return api<Appointment>(`/appointments/${id}`);
}

export async function updateAppointment(
  id: string,
  data: {
    name: string;
    appointmentDateTime: string;
    description: string;
    contactNumber: string;
    emailAddress: string;
  },
) {
  return api(`/appointments/${id}`, {
    method: "PUT",
    body: JSON.stringify(data),
  });
}
