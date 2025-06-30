# RequestsClient

A fluent, declarative, and developer-friendly HTTP client for .NET.  
Designed to simplify API consumption with clean syntax, type safety, and robust features.

> 💡 A Fluent API for Http requests.

RequestsCliente doesn't reinvent the wheel and provides a clear API with abstracted away optimizations and configurations like
resilence and caching.

---

## ✨ Features (Planned & In Progress)

- 🔗 Fluent, declarative API clients.
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
| **Phase 2** | 🗂️ Pagination Helpers (Auto fetch pages) | 🔜 Planned |
|             | 🔌 DI Integration (Singleton Clients) | 🔜 Planned |
|             | 🏛️ Error Handling, Retry Policies (Polly) | 🔜 Planned |
|             | 🔒 Fault Proofing: Timeouts, Circuit Breakers | 🔜 Planned |
|             | 🔑 Auth Middleware (Token Refresh, OAuth, API Keys) | 🔜 Planned |
| **Phase 3** | 📜 OpenAPI Schema Generation for Clients | 🔜 Planned |
|             | 🏗️ Compile-Time Endpoint Generation (Source Generator) | 🔜 Planned |
|             | 🔥 WebSockets Support (Client Side) | 🔜 Planned |
|             | 🔗 gRPC Support (Optional Client Abstraction) | 🔜 Planned |
|             | 🔍 GraphQL Fluent Client | 🔜 Planned |
| **Phase 4** | 🌍 Landing Page + Live Docs (Docusaurus/Static) | 🔜 Planned |
|             | 📚 API Docs (via DocFX / Typedocs for .NET) | 🔜 Planned |
|             | 🚀 NuGet Release v1.0 (Stable API) | 🔜 Planned |
| **Phase 5** | 🧠 AI-assisted Prompt Clients (Experimental) | 🧠 Idea |
|             | 🏗️ Plugin-based Request Pipelines | 🧠 Idea |
|             | 🛠️ Runtime Mock Server for Testing | 🧠 Idea |

---

## 🔥 Example Usage (Preview Concept)

```csharp
var response = await RequestsClient
    .GetAsync<IEnumerable<ProductDTO>>("https://api.example.com/products")
    .WithAuth(new Requests.BasicAuthHeader("example@email.com", "super-secret-password"))
    .WithQueryPagination(page: 1, pageSize: 20)
    .WithQueryParam("status", "active")
    .Send();

IEnumerable<ProductDTO> products = response.Result;
while (response.Next()) // returns true if the current page had overflown the page size (e.g. Count == pageSize)
{
    products.AddRange(response.Result);
}
