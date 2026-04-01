import { describe, it, expect, vi, beforeEach } from "vitest";

vi.mock("~/lib/api", () => ({
  api: vi.fn(),
}));

import { api } from "~/lib/api";
import {
  getMe,
  getAppointments,
  approveAppointment,
  deleteAppointment,
  logout,
} from "./dashboard.data.client";

const mockedApi = vi.mocked(api);

describe("dashboard.data.client", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("getMe calls correct endpoint", async () => {
    mockedApi.mockResolvedValueOnce({ email: "admin@test.com" });

    await getMe();

    expect(mockedApi).toHaveBeenCalledWith("/auth/me");
  });

  it("getAppointments calls correct endpoint", async () => {
    mockedApi.mockResolvedValueOnce([]);

    await getAppointments();

    expect(mockedApi).toHaveBeenCalledWith("/appointments");
  });

  it("approveAppointment calls correct endpoint with PATCH", async () => {
    mockedApi.mockResolvedValueOnce({});

    await approveAppointment("1");

    expect(mockedApi).toHaveBeenCalledWith("/appointments/1/approve", {
      method: "PATCH",
    });
  });

  it("deleteAppointment calls correct endpoint with DELETE", async () => {
    mockedApi.mockResolvedValueOnce(undefined);

    await deleteAppointment("1");

    expect(mockedApi).toHaveBeenCalledWith("/appointments/1", {
      method: "DELETE",
    });
  });

  it("logout calls correct endpoint with POST", async () => {
    mockedApi.mockResolvedValueOnce({});

    await logout();

    expect(mockedApi).toHaveBeenCalledWith("/auth/logout", {
      method: "POST",
    });
  });
});
