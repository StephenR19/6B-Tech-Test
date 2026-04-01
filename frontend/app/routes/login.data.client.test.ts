import { describe, it, expect, vi, beforeEach } from "vitest";

vi.mock("~/lib/api", () => ({
  api: vi.fn(),
}));

import { api } from "~/lib/api";
import { login } from "./login.data.client";

const mockedApi = vi.mocked(api);

describe("login.data.client", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("login calls api with correct credentials", async () => {
    mockedApi.mockResolvedValueOnce({});

    await login({ email: "admin@test.com", password: "password123" });

    expect(mockedApi).toHaveBeenCalledWith("/auth/login", {
      method: "POST",
      body: JSON.stringify({
        email: "admin@test.com",
        password: "password123",
      }),
    });
  });

  it("login throws on invalid credentials", async () => {
    mockedApi.mockRejectedValueOnce(new Error("Unauthorized"));

    await expect(
      login({ email: "wrong@test.com", password: "wrong" }),
    ).rejects.toThrow("Unauthorized");
  });
});
