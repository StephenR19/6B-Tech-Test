import { describe, it, expect, vi, beforeEach } from "vitest";

vi.mock("~/lib/api", () => ({
  api: vi.fn(),
}));

import { api } from "~/lib/api";
import { createAppointment } from "./home.data.client";

const mockedApi = vi.mocked(api);

describe("home.data.client", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("createAppointment calls api with correct data", async () => {
    mockedApi.mockResolvedValueOnce({});

    const data = {
      name: "John",
      appointmentDateTime: "2026-05-01T10:00",
      description: "Checkup",
      contactNumber: "07700900000",
      emailAddress: "john@test.com",
    };

    await createAppointment(data);

    expect(mockedApi).toHaveBeenCalledWith("/appointments", {
      method: "POST",
      body: JSON.stringify(data),
    });
  });

  it("createAppointment throws on api error", async () => {
    mockedApi.mockRejectedValueOnce(new Error("Network error"));

    await expect(
      createAppointment({
        name: "John",
        appointmentDateTime: "2026-05-01T10:00",
        description: "Checkup",
        contactNumber: "07700900000",
        emailAddress: "john@test.com",
      })
    ).rejects.toThrow("Network error");
  });
});
