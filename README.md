# FluentHttp (🚧 Work in Progress)

A fluent, declarative, and developer-friendly HTTP client for .NET.  
Designed to simplify API consumption with clean syntax, type safety, and robust features.

> 💡 Inspired by FluentValidation, Refit, and MediatR—but built for crafting HTTP clients as readable, composable pipelines.

---

## ✨ Features (Planned & In Progress)

- 🔗 Fluent, declarative API clients.
- 🚀 Quick ad-hoc HTTP requests with fluent chaining.
- 🔧 Compile-time endpoint definitions with automatic DI support.
- 🛡️ Resilience patterns (retries, fallback, circuit breakers).
- 🔑 Automatic token refresh and auth strategies.
- 🌐 Support for REST, WebSockets, GraphQL, and gRPC.
- 📜 Strongly typed responses with JSON, XML, streams, and multipart handling.
- 🧰 Extensible with middleware/interceptors (logging, metrics, caching).
- 🔥 Source generator optimizations for zero-reflection runtime.
- 🌍 Landing page & rich web documentation.

---

## 🗺️ Roadmap

| Stage | Milestone | Status |
|-------|-----------|--------|
| **Phase 1** | 📦 Project Initialization | ✅ Done |
|             | 🏗️ Plan the Basic API Surface | 🔄 In Progress |
|             | 🚀 Implement Basic Fluent Request Pipeline | 🔄 In Progress |
|             | ⚙️ Quick "On Spot" Requests (ad-hoc usage) | 🔜 Planned |
|             | 🔧 Build Core DSL for Declarative Clients | 🔜 Planned |
| **Phase 2** | 🏗️ Compile-Time Endpoint Generation (Source Generator) | 🔜 Planned |
|             | 🔌 DI Integration (Singleton Clients) | 🔜 Planned |
|             | 🏛️ Error Handling, Retry Policies (Polly) | 🔜 Planned |
|             | 🔒 Fault Proofing: Timeouts, Circuit Breakers | 🔜 Planned |
|             | 🔑 Auth Middleware (Token Refresh, OAuth, API Keys) | 🔜 Planned |
| **Phase 3** | 📜 OpenAPI Schema Generation for Clients | 🔜 Planned |
|             | 🔥 WebSockets Support (Client Side) | 🔜 Planned |
|             | 🔗 gRPC Support (Optional Client Abstraction) | 🔜 Planned |
|             | 🔍 GraphQL Fluent Client | 🔜 Planned |
|             | 🗂️ Pagination Helpers (Auto fetch pages) | 🔜 Planned |
| **Phase 4** | 🌍 Landing Page + Live Docs (Docusaurus/Static) | 🔜 Planned |
|             | 📚 API Docs (via DocFX / Typedocs for .NET) | 🔜 Planned |
|             | 🚀 NuGet Release v1.0 (Stable API) | 🔜 Planned |
| **Phase 5** | 🧠 AI-assisted Prompt Clients (Experimental) | 🧠 Idea |
|             | 🏗️ Plugin-based Request Pipelines | 🧠 Idea |
|             | 🛠️ Runtime Mock Server for Testing | 🧠 Idea |

---

## 🔥 Example Usage (Preview Concept)

```csharp
var client = FluentHttpClient
    .BaseUrl("https://api.example.com")
    .WithHeader("Authorization", "Bearer token123")
    .Get("/users")
    .WithQueryParam("page", "1")
    .ExpectJson<User[]>()
    .Build();

var users = await client.ExecuteAsync();