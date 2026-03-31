# Development Timeline

| Timestamp | Commit | Description |
|-----------|--------|-------------|
| 2026-03-30 23:01:56 | `28df256` | Initial commit — README with project requirements |
| 2026-03-31 00:12:23 | `938d4f0` | Full project scaffold — backend, frontend, Docker Compose |
| — | — | Fix `CURRENT_TIMESTAMP(6)` for MySQL datetime precision |
| — | — | Convert SSR routes to SPA (`clientLoader`/`clientAction`) |
| — | — | Add `serve` for SPA static hosting, fix `start` script |
| — | — | Fix CORS with named policy (`SetIsOriginAllowed`) |
| — | — | Add DB startup retry loop for MySQL readiness |
| — | — | Add `HydrateFallback` component to root |
| 2026-03-31 17:35 | - | Start work on BE auth, unit tests(NUnit), liniting, formatting |
| 2026-03-31 18:28 | - | Fe loaders action split out. add FE and BE unit tests. Stopping work here|
