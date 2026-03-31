import { api } from "~/lib/api";

export async function createAppointment(data: {
  name: string;
  appointmentDateTime: string;
  description: string;
  contactNumber: string;
  emailAddress: string;
}) {
  return api("/appointments", {
    method: "POST",
    body: JSON.stringify(data),
  });
}
