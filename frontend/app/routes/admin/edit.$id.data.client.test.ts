import { describe, it, expect, vi, beforeEach } from "vitest";

vi.mock("~/lib/api", () => ({
  api: vi.fn(),
}));

import { api } from "~/lib/api";
import {
  getMe,
  getAppointment,
  updateAppointment,
} from "./edit.$id.data.client";

const mockedApi = vi.mocked(api);

describe("edit.$id.data.client", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("getMe calls correct endpoint", async () => {
    mockedApi.mockResolvedValueOnce({ email: "admin@test.com" });

    await getMe();

    expect(mockedApi).toHaveBeenCalledWith("/auth/me");
  });

  it("getAppointment calls correct endpoint with id", async () => {
    mockedApi.mockResolvedValueOnce({ id: 1, name: "Test" });

    await getAppointment("1");

    expect(mockedApi).toHaveBeenCalledWith("/appointments/1");
  });

  it("updateAppointment calls correct endpoint with PUT and data", async () => {
    mockedApi.mockResolvedValueOnce({});

    const data = {
      name: "Updated",
      appointmentDateTime: "2026-06-01T10:00",
      description: "Updated desc",
      contactNumber: "456",
      emailAddress: "updated@test.com",
    };

    await updateAppointment("5", data);

    expect(mockedApi).toHaveBeenCalledWith("/appointments/5", {
      method: "PUT",
      body: JSON.stringify(data),
    });
  });
});
