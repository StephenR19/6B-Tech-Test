import { redirect } from "react-router";
import { updateAppointment } from "./edit.$id.data.client";
import type { Route } from "./+types/edit.$id";

export async function clientAction({ request, params }: Route.ClientActionArgs) {
  const formData = await request.formData();
  const data = {
    name: formData.get("name") as string,
    appointmentDateTime: formData.get("appointmentDateTime") as string,
    description: formData.get("description") as string,
    contactNumber: formData.get("contactNumber") as string,
    emailAddress: formData.get("emailAddress") as string,
  };

  try {
    await updateAppointment(params.id, data);
    return redirect("/admin");
  } catch {
    return { error: "Failed to update appointment" };
  }
}
