import { createAppointment } from "./home.data.client";
import type { Route } from "./+types/home";

export async function clientAction({ request }: Route.ClientActionArgs) {
  const formData = await request.formData();
  const data = {
    name: formData.get("name") as string,
    appointmentDateTime: formData.get("appointmentDateTime") as string,
    description: formData.get("description") as string,
    contactNumber: formData.get("contactNumber") as string,
    emailAddress: formData.get("emailAddress") as string,
  };

  try {
    await createAppointment(data);
    return { success: true };
  } catch (error: unknown) {
    if (error instanceof Response) {
      const body = await error.json().catch(() => ({}));
      return { success: false, errors: body };
    }
    return {
      success: false,
      errors: { message: "Failed to book appointment" },
    };
  }
}
