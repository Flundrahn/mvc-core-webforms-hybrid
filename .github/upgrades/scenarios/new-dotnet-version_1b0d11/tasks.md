# AspNetCoreMvc .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the AspNetCoreMvc project upgrade from .NET 6.0 to .NET 10.0. Integration tests will be established on .NET 6.0 first to provide automated regression detection, then both AspNetCoreMvc and the test project will be upgraded together in a single atomic operation.

**Progress**: 3/4 tasks complete (75%) ![0%](https://progress-bar.xyz/75)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-02-19 14:36)*
**References**: Plan §Phase 1, Plan §Step 1

- [✓] (1) Verify .NET 10 SDK installed per Plan §Step 1
- [✓] (2) .NET 10 SDK present in installed SDKs list (**Verify**)

---

### [✓] TASK-002: Create integration tests on .NET 6.0 *(Completed: 2026-02-19 15:35)*
**References**: Plan §Phase 0, Plan §Step 0

- [✓] (1) Create AspNetCoreMvc.IntegrationTests project targeting net6.0 per Plan §Step 0
- [✓] (2) Add project references to AspNetCoreMvc and Data, add required packages per Plan §Step 0
- [✓] (3) Implement integration tests per Plan §Step 0 test specifications (application initialization, repository methods, NHibernate integration)
- [✓] (4) Configure test database connection per Plan §Step 0
- [✓] (5) Run tests on .NET 6.0
- [✓] (6) All tests pass with 0 failures (**Verify**)
- [✓] (7) Commit changes with message: "Add integration tests for AspNetCoreMvc on .NET 6.0"

---

### [✓] TASK-003: Atomic framework and package upgrade *(Completed: 2026-02-19 15:36)*
**References**: Plan §Phase 2, Plan §Step 2, Plan §Package Update Reference

- [✓] (1) Update TargetFramework to net10.0 in both AspNetCoreMvc.csproj and AspNetCoreMvc.IntegrationTests.csproj per Plan §Step 2
- [✓] (2) Remove System.Net.Http and System.Text.RegularExpressions package references from AspNetCoreMvc.csproj per Plan §Step 2
- [✓] (3) Restore NuGet packages per Plan §Step 3
- [✓] (4) All packages restore successfully (**Verify**)
- [✓] (5) Build solution and fix all compilation errors per Plan §Step 4 and Plan §Breaking Changes Catalog
- [✓] (6) Solution builds with 0 errors (**Verify**)

---

### [▶] TASK-004: Run integration tests and finalize
**References**: Plan §Phase 3, Plan §Step 6

- [✓] (1) Run integration tests in AspNetCoreMvc.IntegrationTests project per Plan §Step 6
- [✓] (2) Fix any test failures (reference Plan §Breaking Changes Catalog if needed)
- [✓] (3) Re-run tests after fixes
- [✓] (4) All tests pass with 0 failures (**Verify**)
- [▶] (5) Commit changes with message: "Upgrade AspNetCoreMvc from .NET 6.0 to .NET 10.0"

---












