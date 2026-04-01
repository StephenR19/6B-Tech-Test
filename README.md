## Overview
SixBee HealthTech is a healthcare technology company operating in West Yorkshire. Currently, all appointments are managed over the phone and manually entered by a practice receptionist.
SixBee HealthTech would like to automate this process.

## Aims
1. Automate a patient appointment booking system  
2. Create a patient facing form for appointments  
3. Build a simple practice admin area behind a login screen  
4. Display appointments in a table view ordered by date  
5. Provide functionality to edit and approve appointments  

## Technology Requirements
- The system should be built using a tech stack of .NET.   
- The database should be **MySQL**

### Nice to Have
- Tailwind styling
- Containerised setup (e.g. Docker / Docker Compose)
- Unit tests

## Patient Facing Form
Create a simple form that submits and saves appointment data to a database. This data will be used in the admin area.

### Required Fields
- **Name** (required)  
- **Appointment Date and Time** (required)  
- **Desc** (required)  
- **Contact Number** (required)  
- **Email Address** (required)  

## Admin Area
The admin area should be protected behind a login system.

### Authentication
- Login form must include:
  - Email Address  
  - Password  
- A registration flow is **not required**  
- Credentials should be stored in the database  
- You may:
  - reate a seeder to populate a user during setup  

## Admin Features
### Appointments Table
- A simple table listing all appointments  
- Ordered by appointment date  
- Columns should match the patient form fields  

### Actions Column
Each row should include:
- **Approve**
  - Tick icon or button  
  - When approved, the row should change colour  
- **Edit**
  - Opens an edit screen  
  - Form contains the same fields as the patient form  
  - Fields are pre populated  
  - Changes should save and reflect in the table  
- **Delete**
  - Removes the appointment  

### Navigation
- Include a navigation bar  
- Provide a logout option  

## Source Control
- The project must be hosted in a **public GitHub repository** from the start  
- Commit regularly after completing each feature  
- Commit history should clearly show progress  

## Additional Notes
- Use your judgement to ensure the solution is:
  - Functional for users  
  - Maintainable for developers  
  - Easy to extend in future  

- Aim to meet the core requirements rather than over engineering  

---

## Developer Additions
*The following sections were added by the developer and are not part of the original brief.*

## Architecture Decisions

### React
Widely adopted with a large ecosystem, extensive community support, and a strong developer talent pool for future maintenance.

### React Router v7
Provides built-in data loading (loaders/actions) that eliminates the need for a separate state management library. Supports both SPA and SSR modes, offering flexibility to scale. Includes TypeScript-first design and automatic type generation.

### TypeScript
Catches errors at compile time, provides better IDE support and autocompletion, and serves as self-documenting code for the codebase.

### Vite
Lightning-fast dev server with instant HMR and native ESM support, significantly improving developer experience over older bundlers.

### Vitest + Testing Library
Vitest is Vite-native with zero config and near-instant test execution. Testing Library encourages testing behavior over implementation details, leading to more maintainable tests.

### Docker Compose
Ensures consistent environments across dev and production. A single `docker-compose up` command spins up the full stack (MySQL + API + Frontend), simplifying onboarding.

### GitHub Actions CI
Parallel jobs for backend and frontend run format checks, linting, tests, and builds on every push/PR, catching issues early.

### OpenAPI Type Generation
Auto-generates TypeScript types from the backend Swagger spec (`npm run generate:types`), keeping frontend and backend types in sync automatically and eliminating manual type maintenance.

### Co-located Tests
Test files sit next to their source (`*.data.client.test.ts`), making them easy to find and maintain alongside the code they test.

### SPA Architecture
React Router is configured in SPA mode (`ssr: false`), keeping the frontend as a simple static bundle served by Node. SSR could be configured in the future.

### Authentication
For this project, a simple cookie-based auth approach was used for practicality. In a production healthcare environment, this would be replaced with an enterprise identity provider. Keycloak could serve as an identity broker, federating authentication to providers such as OKTA or NHS CIS2.

### Styling
Tailwind CSS was used as desired. The styling is basic and functional — in a production healthcare application, the UI should follow GDS (Government Digital Service) guidelines for accessibility, ensuring proper contrast ratios, keyboard navigation, focus management, and accessible form inputs with clear labels and error messages.

## Considerations & Future Improvements

### Approved Appointment Restrictions
Once an appointment is approved, certain fields or actions should be restricted to prevent accidental modifications. For example, editing an approved appointment could require re-approval, or edits could be blocked entirely.

### Soft Delete
Rather than permanently removing appointments, a soft delete approach (e.g. an `IsDeleted` flag or `DeletedAt` timestamp) should be used. This preserves data integrity and allows deleted records to be recovered if needed.

### Audit Trail
All changes to appointments should be logged, including who made the change, what was changed, and when. This is critical in a healthcare context for accountability and compliance.

### Performance

#### Server-Side Rendering (SSR)
React Router is currently configured in SPA mode (`ssr: false`). Enabling SSR would improve initial load times and SEO by serving pre-rendered HTML on the first request, with client-side hydration taking over after.

#### Database Optimisation
As the dataset grows, query performance should be monitored. Examining SQL query plans would reveal whether additional indexes are needed on frequently queried columns (e.g. `AppointmentDate`, `Status`).

#### PostgreSQL
While MySQL meets the requirements, PostgreSQL offers advantages for healthcare workloads such as stronger ACID compliance, better support for complex queries, and native JSON handling.

#### Message Bus
For a larger-scale deployment, a message bus (e.g. RabbitMQ, Azure Service Bus) could be introduced to decouple services and handle asynchronous operations such as appointment notifications, confirmation emails, or audit logging.
