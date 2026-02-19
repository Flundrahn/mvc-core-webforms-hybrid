# .NET 10.0 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Implementation Timeline](#implementation-timeline)
- [Detailed Execution Steps](#detailed-execution-steps)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Migration Plans](#project-by-project-migration-plans)
  - [AspNetCoreMvc](#aspnetcoremvcaspnetcoremvccsproj)
  - [AspNetCoreMvc.IntegrationTests (New)](#aspnetcoremvcintegrationtests-new-project)
  - [Data](#datadatacsproj)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Risk Management](#risk-management)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description

Upgrade the **AspNetCoreMvc** project from **.NET 6.0** to **.NET 10.0 LTS** (Long Term Support). This upgrade focuses exclusively on the ASP.NET Core MVC application while maintaining compatibility with the shared Data library (.NET Standard 2.0).

### Scope

**Projects in Scope:**
- **AspNetCoreMvc** (AspNetCoreMvc\AspNetCoreMvc.csproj) - .NET 6.0 → .NET 10.0
- **AspNetCoreMvc.IntegrationTests** (New) - Integration test project to be created

**Projects Out of Scope:**
- **AspNetWebForms** - Remains on .NET Framework 4.8 (requires separate major migration)
- **Data** - Remains on .NET Standard 2.0 (already compatible)

### Current State

- **Total Projects**: 3 (1 in scope for upgrade + 1 new test project)
- **AspNetCoreMvc**: .NET 6.0, 19 files, 267 LOC, 6 NuGet packages
- **Issue Count**: 2 (both minor - framework-included functionality)
- **Security Vulnerabilities**: None
- **Dependency Depth**: 1 (AspNetCoreMvc → Data)
- **Test Projects**: 0 (will create AspNetCoreMvc.IntegrationTests)

### Target State

- **AspNetCoreMvc**: .NET 10.0 with updated framework-included packages removed
- **AspNetCoreMvc.IntegrationTests**: .NET 10.0 with comprehensive integration tests
- **Expected Outcome**: Simplified project with fewer explicit package references, leveraging .NET 10 built-in functionality, validated by integration tests

### Complexity Assessment

**Classification: Simple**

**Metrics Supporting Simple Classification:**
- ✅ Single project upgrade
- ✅ Low dependency depth (1 level)
- ✅ Small codebase (267 LOC)
- ✅ Few package changes (2 packages to remove)
- ✅ No security vulnerabilities
- ✅ No API compatibility issues (0 breaking changes)
- ✅ Already on modern .NET (6.0 → 10.0)
- ✅ SDK-style project

**Critical Issues**: None

### Selected Strategy

**All-At-Once Strategy** - Single atomic upgrade operation.

**Rationale:**
- Only 1 project in scope (plus new test project)
- Minimal changes required (TargetFramework + remove 2 packages)
- No intermediate states needed
- Entire upgrade can complete quickly after tests established
- Low risk profile with safety net of integration tests

### Iteration Strategy

Given the simple classification with test establishment, this plan will use **enhanced fast batch approach**:
- Phase 0 (Test Establishment) documented with detailed test specifications
- Phases 1-4 completed in 2-3 detail iterations

**Test-First Approach:** Integration tests established on .NET 6.0 before migration, providing automated regression detection on .NET 10.0.

---

## Migration Strategy

### Approach Selection

**Selected Approach: All-At-Once Strategy**

**Justification:**
- **Single Project Scope**: Only AspNetCoreMvc requires upgrade
- **Minimal Complexity**: 2 issues (both package removals), 0 API breaking changes
- **Small Codebase**: 267 lines of code
- **No Risk Factors**: No security vulnerabilities, no deprecated APIs, no breaking changes
- **Clean Dependencies**: Single dependency on Data library which requires no changes
- **Already Modern**: Starting from .NET 6.0 (not legacy .NET Framework)

### All-At-Once Strategy Rationale

This scenario perfectly fits All-At-Once criteria:
- ✅ Only 1 project in scope (plus test project)
- ✅ Simple, clear dependency structure
- ✅ Small codebase
- ✅ Minimal changes required
- ✅ Low risk profile
- ✅ Can complete in single operation
- ✅ **Enhanced with integration tests** - provides automated regression detection

**Test-First Enhancement:**
- Integration tests establish .NET 6.0 baseline before migration
- Same tests validate .NET 10.0 after migration
- Automated regression detection reduces manual testing burden
- Increases confidence in migration success

**Execution Approach:**
1. **Phase 0: Create integration tests on .NET 6.0, verify they pass**
2. Update AspNetCoreMvc and test project files (TargetFramework)
3. Remove packages now included in framework
4. Restore dependencies
5. Build and verify
6. **Run integration tests on .NET 10.0, verify they still pass**
7. Additional manual validation

**Test-Driven Migration:** Integration tests provide safety net, catching regressions immediately.

**No intermediate states needed** - the upgrade completes in one atomic operation.

---

## Implementation Timeline

### Phase 0: Test Establishment (Pre-Migration)

**Purpose:** Create integration tests to validate current functionality before migration, establishing a safety net.

**Operations:**

1. Create new test project: `AspNetCoreMvc.IntegrationTests`
2. Target framework: net6.0 (matching current AspNetCoreMvc)
3. Add integration test infrastructure
4. Implement tests covering:
   - Application initialization (startup, dependency injection, configuration)
   - Repository methods against test database
   - NHibernate session management
   - Data CRUD operations
5. Create test database (user-provided)
6. Run tests and verify all pass on .NET 6.0

**Deliverables:** 
- Integration test project created
- All tests pass on .NET 6.0 (baseline established)
- Test database configured

**Success Criteria:**
- ✅ Test project builds successfully
- ✅ All integration tests pass (100% pass rate)
- ✅ Tests cover initialization and repository operations
- ✅ Test database connection works

---

### Phase 1: Preparation

**Verify Prerequisites:**
- ✅ .NET 10 SDK installed
- ✅ On `upgrade-to-NET10` branch (already created)
- ✅ No pending changes
- ✅ Integration tests pass on .NET 6.0 (from Phase 0)

**Deliverables:** Environment ready for upgrade

---

### Phase 2: Atomic Upgrade

**Operations** (performed as single coordinated operation):

1. Update AspNetCoreMvc.csproj TargetFramework to net10.0
2. Update AspNetCoreMvc.IntegrationTests.csproj TargetFramework to net10.0
3. Remove System.Net.Http package reference from AspNetCoreMvc
4. Remove System.Text.RegularExpressions package reference from AspNetCoreMvc
5. Restore NuGet packages
6. Build solution
7. Fix any compilation errors (none expected)
8. Verify build success

**Deliverables:** AspNetCoreMvc and tests build successfully on .NET 10.0 with 0 errors/warnings

---

### Phase 3: Validation

**Operations:**

1. Run integration tests on .NET 10.0
2. Verify all tests still pass (regression detection)
3. Start application and verify startup
4. Execute additional smoke tests (YARP routing, database, basic functionality)
5. Perform integration testing
6. Document any observations

**Deliverables:** 
- Integration tests pass on .NET 10.0 (same pass rate as .NET 6.0)
- Application runs successfully on .NET 10.0
- All manual tests pass

---

### Phase 4: Finalization

**Operations:**

1. Commit changes to upgrade-to-NET10 branch
2. Create pull request
3. Review and approve
4. Merge to main branch
5. Update documentation (README.md)
6. Clean up upgrade branch

**Deliverables:** Upgrade complete, merged, and documented

---

## Detailed Execution Steps

### Step 0: Create Integration Tests (Pre-Migration)

**Create Test Project:**

```powershell
dotnet new xunit -n AspNetCoreMvc.IntegrationTests -f net6.0
dotnet sln add AspNetCoreMvc.IntegrationTests/AspNetCoreMvc.IntegrationTests.csproj
```

**Add Project Reference:**

In `AspNetCoreMvc.IntegrationTests.csproj`, add:
```xml
<ItemGroup>
  <ProjectReference Include="..\AspNetCoreMvc\AspNetCoreMvc.csproj" />
  <ProjectReference Include="..\Data\Data.csproj" />
</ItemGroup>
```

**Add Required Packages:**

```powershell
cd AspNetCoreMvc.IntegrationTests
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Microsoft.EntityFrameworkCore.InMemory # or SQL Server for real DB tests
dotnet add package FluentAssertions # optional, for better assertions
```

**Test Coverage Requirements:**

1. **Application Initialization Tests:**
   - Verify application starts without exceptions
   - Verify dependency injection container configured correctly
   - Verify configuration loaded (appsettings.json)
   - Verify NHibernate session factory initialized

2. **Repository Method Tests:**
   - Test Create operations (insert data)
   - Test Read operations (query data)
   - Test Update operations (modify data)
   - Test Delete operations (remove data)
   - Test query methods (filters, ordering)

3. **Database Integration Tests:**
   - Use test database provided by user
   - Verify schema creation/updates
   - Test transaction handling
   - Test concurrent access (if applicable)

**Example Test Structure:**

```csharp
public class RepositoryIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RepositoryIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Application_Initializes_Successfully()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public void Repository_Create_InsertsData()
    {
        // Test create operation against test DB
    }

    [Fact]
    public void Repository_Read_RetrievesData()
    {
        // Test read operation against test DB
    }

    // ... more repository tests
}
```

**Test Database Setup:**

User will provide test database. Update test configuration to use it:

```json
// appsettings.Testing.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TestDb;..."
  }
}
```

**Verify Tests Pass on .NET 6.0:**

```powershell
dotnet test AspNetCoreMvc.IntegrationTests/AspNetCoreMvc.IntegrationTests.csproj
```

**Expected Outcome:** All tests pass (establish baseline)

---

### Step 1: Verify .NET 10 SDK

Verify .NET 10 SDK is installed:
```powershell
dotnet --list-sdks
```

Expected: .NET 10.x.x SDK present in list.

If missing, download from: https://dotnet.microsoft.com/download/dotnet/10.0

---

### Step 2: Update Project Files

**File 1:** `AspNetCoreMvc\AspNetCoreMvc.csproj`

**Change 1: Update TargetFramework**

Locate:
```xml
<TargetFramework>net6.0</TargetFramework>
```

Replace with:
```xml
<TargetFramework>net10.0</TargetFramework>
```

**Change 2: Remove Framework-Included Packages**

Remove these package references:
```xml
<PackageReference Include="System.Net.Http" Version="4.3.4" />
<PackageReference Include="System.Text.RegularExpressions" Version="4.3.1" />
```

**Rationale:** These packages are now included in the .NET 10 framework and cause redundant references.

---

**File 2:** `AspNetCoreMvc.IntegrationTests\AspNetCoreMvc.IntegrationTests.csproj`

**Change: Update TargetFramework**

Locate:
```xml
<TargetFramework>net6.0</TargetFramework>
```

Replace with:
```xml
<TargetFramework>net10.0</TargetFramework>
```

---

### Step 3: Restore Dependencies

Restore NuGet packages for all projects:
```powershell
dotnet restore mvc-core-webforms-hybrid.sln
```

**Expected Outcome:** All packages restore successfully, no errors or warnings.

---

### Step 4: Build Solution

Build the entire solution:
```powershell
dotnet build mvc-core-webforms-hybrid.sln --configuration Release
```

**Expected Outcome:** 
- Build succeeds
- 0 errors
- 0 warnings
- All projects build successfully

**If Build Fails:** Review error messages, consult Breaking Changes Catalog, investigate .NET 10 migration documentation.

---

### Step 5: Validate Application

**Start Application:**
```powershell
dotnet run --project AspNetCoreMvc\AspNetCoreMvc.csproj
```

**Validation Checklist:**
- [ ] Application starts without exceptions
- [ ] No errors in console output
- [ ] Listens on expected port (check console output)
- [ ] Home page accessible via browser
- [ ] YARP routes to WebForms app function
- [ ] Database connection succeeds
- [ ] NHibernate schema export completes (if triggered)

---

### Step 6: Execute Integration Tests

**Run Integration Tests on .NET 10.0:**

```powershell
dotnet test AspNetCoreMvc.IntegrationTests\AspNetCoreMvc.IntegrationTests.csproj --configuration Release
```

**Expected Outcome:**
- ✅ All tests pass (100% pass rate)
- ✅ Same tests that passed on .NET 6.0 still pass on .NET 10.0
- ✅ No regressions detected
- ✅ Repository operations work correctly
- ✅ Application initialization succeeds

**If Tests Fail:**
- Review test output for specific failures
- Check for .NET 10 behavioral changes affecting tested areas
- Verify test database connectivity
- Compare behavior with .NET 6.0 baseline

---

### Step 6a: Execute Additional Manual Tests

**Manual Testing (supplementary validation):**

1. **Basic Navigation**
   - Access MVC home page
   - Navigate through MVC controllers
   - Verify views render correctly

2. **YARP Integration**
   - Navigate to URLs proxied to WebForms
   - Verify seamless transition
   - Check that both apps can access shared data

3. **Data Operations**
   - Perform create, read, update operations via UI
   - Verify data persists correctly
   - Confirm NHibernate functionality

**Expected Outcome:** All manual tests pass, no functional regressions detected.

---

### Step 7: Commit Changes

After successful validation (tests pass on .NET 10.0), commit to `upgrade-to-NET10` branch:

```powershell
git add .
git commit -m "Upgrade AspNetCoreMvc to .NET 10.0 with integration tests

- Updated AspNetCoreMvc TargetFramework to net10.0
- Updated AspNetCoreMvc.IntegrationTests TargetFramework to net10.0
- Removed System.Net.Http (now in framework)
- Removed System.Text.RegularExpressions (now in framework)
- All packages compatible and verified
- Build successful with 0 errors/warnings
- All integration tests pass on .NET 10.0"
```

---

### Step 8: Create Pull Request

Create PR from `upgrade-to-NET10` → `main`

Use Pull Request template from Source Control Strategy section.

---

### Step 9: Merge and Finalize

After PR approval:

1. Merge to main
2. Update README.md (mark ToDo item complete, update requirements)
3. Delete upgrade branch
4. Notify team

---

## Detailed Dependency Analysis

### Dependency Graph Summary

The solution has a clean, simple dependency structure with added test project:

```
AspNetCoreMvc (net6.0 → net10.0)
    └── Data (netstandard2.0) ✅ Compatible

AspNetCoreMvc.IntegrationTests (net6.0 → net10.0) [New]
    ├── AspNetCoreMvc (test target)
    └── Data (direct reference for repository testing)

AspNetWebForms (net48) [Out of Scope]
    └── Data (netstandard2.0) ✅ Compatible
```

**Key Characteristics:**
- **Dependency Depth**: 1 level (AspNetCoreMvc and test project depend only on Data)
- **Circular Dependencies**: None
- **Shared Dependencies**: Data library is shared by all projects
- **Critical Path**: Data → AspNetCoreMvc → AspNetCoreMvc.IntegrationTests
- **Test Dependencies**: Test project references both AspNetCoreMvc and Data

### Project Groupings

**Migration Phase: Single Atomic Operation with Test-First Approach**

Since only one application project requires upgrade, the migration follows test-first pattern:

**Phase 0: Test Establishment (Pre-Migration)**
- Create AspNetCoreMvc.IntegrationTests on .NET 6.0
- Establish passing test baseline

**Phase 2: Atomic Upgrade**
- AspNetCoreMvc (net6.0 → net10.0)
- AspNetCoreMvc.IntegrationTests (net6.0 → net10.0)
- Upgraded together in single operation

**Phase 3: Test Validation**
- Verify integration tests still pass on .NET 10.0
- Confirm no regressions

### Dependency Considerations

**Data Library (.NET Standard 2.0)**
- ✅ Remains unchanged
- ✅ .NET Standard 2.0 is compatible with .NET 10.0
- ✅ Shared by both AspNetCoreMvc and AspNetWebForms
- ✅ No package updates required
- **Rationale for keeping netstandard2.0**: Maintains compatibility with both the upgraded AspNetCoreMvc (.NET 10) and the legacy AspNetWebForms (.NET Framework 4.8)

**AspNetWebForms (.NET Framework 4.8)**
- Out of scope for this upgrade
- Remains on .NET Framework 4.8
- Will continue to use Data library via .NET Standard 2.0 compatibility
- Future migration would be a separate, complex project

### Migration Order

**No ordering required** - single project upgrade with no dependencies on other projects being upgraded.

---

## Project-by-Project Migration Plans

### AspNetCoreMvc (AspNetCoreMvc\AspNetCoreMvc.csproj)

**Current State:**
- Target Framework: net6.0
- Project Type: ASP.NET Core MVC
- SDK-style: True
- Dependencies: 1 (Data library)
- Files: 19
- Lines of Code: 267
- Risk Level: 🟢 Low

**Current NuGet Packages:**
- FluentNHibernate 3.2.0
- Microsoft.AspNetCore.SystemWebAdapters.CoreServices 1.1.0
- NHibernate 5.4.9
- System.Data.SqlClient 4.8.6
- System.Net.Http 4.3.4 ⚠️ (functionality now in framework)
- System.Text.RegularExpressions 4.3.1 ⚠️ (functionality now in framework)
- Yarp.ReverseProxy 2.0.1

**Target State:**
- Target Framework: net10.0
- Cleaner package references (2 packages removed as now included in framework)

---

#### Migration Steps

##### 1. Prerequisites

**Verify .NET 10 SDK Installation:**
- Ensure .NET 10 SDK is installed on development machine
- Verify via: `dotnet --list-sdks`

**Dependencies:**
- ✅ Data library (netstandard2.0) - already compatible, no changes needed

##### 2. Framework Update

**Update AspNetCoreMvc.csproj:**

Change the `TargetFramework` property:
```xml
<TargetFramework>net10.0</TargetFramework>
```

**Previous value:** `net6.0`

##### 3. Package Updates

**Packages to Remove:**

The following packages are now included in the .NET 10 framework and should be removed:

| Package | Current Version | Action | Reason |
| :--- | :---: | :--- | :--- |
| System.Net.Http | 4.3.4 | ❌ Remove | Functionality included with framework reference (NuGet.0003) |
| System.Text.RegularExpressions | 4.3.1 | ❌ Remove | Functionality included with framework reference (NuGet.0003) |

**Remove from AspNetCoreMvc.csproj:**
```xml
<PackageReference Include="System.Net.Http" Version="4.3.4" />
<PackageReference Include="System.Text.RegularExpressions" Version="4.3.1" />
```

**Packages Remaining (No Changes):**

These packages remain compatible and require no version updates:

| Package | Version | Status |
| :--- | :---: | :--- |
| FluentNHibernate | 3.2.0 | ✅ Compatible |
| Microsoft.AspNetCore.SystemWebAdapters.CoreServices | 1.1.0 | ✅ Compatible |
| NHibernate | 5.4.9 | ✅ Compatible |
| System.Data.SqlClient | 4.8.6 | ✅ Compatible |
| Yarp.ReverseProxy | 2.0.1 | ✅ Compatible |

##### 4. Expected Breaking Changes

**None Expected**

Assessment analysis shows:
- 🟢 0 Binary Incompatible APIs
- 🟢 0 Source Incompatible APIs
- 🟢 0 Behavioral Changes

**All 1,660 APIs analyzed are compatible with .NET 10.0**

##### 5. Code Modifications

**No code changes expected**

Based on assessment:
- No deprecated API usage
- No obsolete patterns
- No configuration changes required
- No namespace changes required

**Areas to Monitor (proactive checks):**
- Ensure YARP reverse proxy configuration still functions correctly
- Verify SystemWebAdapters compatibility (used for WebForms integration)
- Confirm NHibernate initialization works as expected

##### 6. Testing Strategy

**Build Validation:**
- ✅ Restore NuGet packages successfully
- ✅ Build succeeds with 0 errors
- ✅ Build succeeds with 0 warnings

**Functional Testing:**
- ✅ Application starts without errors
- ✅ YARP reverse proxy routes correctly to WebForms app
- ✅ Database connectivity via NHibernate works
- ✅ NHibernate schema export completes successfully
- ✅ MVC controllers and views render correctly
- ✅ SystemWebAdapters integration with WebForms functions

**Integration Testing:**
- ✅ MVC app can communicate with WebForms app via YARP
- ✅ Shared Data library works correctly from MVC app
- ✅ SQL Server connection and data operations succeed

**Manual Smoke Tests:**
- Start application and verify home page loads
- Navigate between MVC and WebForms sections via proxy
- Perform basic CRUD operations to verify data layer

##### 7. Validation Checklist

**Pre-Upgrade:**
- [ ] .NET 10 SDK installed and verified
- [ ] On `upgrade-to-NET10` branch
- [ ] No pending uncommitted changes

**Post-Upgrade:**
- [ ] TargetFramework updated to net10.0
- [ ] System.Net.Http package removed
- [ ] System.Text.RegularExpressions package removed
- [ ] Build succeeds with 0 errors
- [ ] Build succeeds with 0 warnings
- [ ] No NuGet package restore errors
- [ ] Application starts successfully
- [ ] YARP routing functions correctly
- [ ] Database operations work
- [ ] No security vulnerabilities introduced
- [ ] All functional tests pass

---

### AspNetCoreMvc.IntegrationTests (New Project)

**Purpose:** Integration test project to validate AspNetCoreMvc functionality before and after upgrade.

**Target State:**
- Target Framework: net6.0 initially, then net10.0 (upgraded in Phase 2)
- Project Type: xUnit Test Project
- Dependencies: AspNetCoreMvc, Data

**Test Coverage:**

#### 1. Application Initialization Tests

**File:** `ApplicationInitializationTests.cs`

```csharp
public class ApplicationInitializationTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Application_Starts_Successfully()
    {
        // Verify application starts without exceptions
    }

    [Fact]
    public void DependencyInjection_Container_ConfiguredCorrectly()
    {
        // Verify services registered in DI container
    }

    [Fact]
    public void Configuration_LoadedCorrectly()
    {
        // Verify appsettings.json loaded
        // Verify connection strings available
    }

    [Fact]
    public void NHibernate_SessionFactory_Initialized()
    {
        // Verify NHibernate session factory created
        // Verify can open session
    }
}
```

#### 2. Repository Method Tests

**File:** `RepositoryMethodTests.cs`

Tests for each repository method against test database:

```csharp
public class RepositoryMethodTests : IDisposable
{
    private readonly ISession _session;

    public RepositoryMethodTests()
    {
        // Initialize test database connection
        // _session = ... (user will provide test DB details)
    }

    [Fact]
    public void Repository_Create_InsertsData()
    {
        // Test: Insert new entity
        // Verify: Entity saved to test DB
    }

    [Fact]
    public void Repository_Read_RetrievesData()
    {
        // Test: Query entity by ID
        // Verify: Correct entity returned
    }

    [Fact]
    public void Repository_Update_ModifiesData()
    {
        // Test: Update existing entity
        // Verify: Changes persisted to test DB
    }

    [Fact]
    public void Repository_Delete_RemovesData()
    {
        // Test: Delete entity
        // Verify: Entity removed from test DB
    }

    [Fact]
    public void Repository_Query_WithFilters_ReturnsFilteredResults()
    {
        // Test: Query with WHERE clause
        // Verify: Only matching records returned
    }

    [Fact]
    public void Repository_Query_WithOrdering_ReturnsSortedResults()
    {
        // Test: Query with ORDER BY
        // Verify: Results in correct order
    }

    public void Dispose()
    {
        _session?.Dispose();
    }
}
```

#### 3. NHibernate Integration Tests

**File:** `NHibernateIntegrationTests.cs`

```csharp
public class NHibernateIntegrationTests
{
    [Fact]
    public void NHibernate_SchemaExport_Succeeds()
    {
        // Test: Schema export/update
        // Verify: Tables created/updated correctly
    }

    [Fact]
    public void NHibernate_Transaction_CommitsSuccessfully()
    {
        // Test: Transaction commit
        // Verify: Changes persisted
    }

    [Fact]
    public void NHibernate_Transaction_RollbackSuccessfully()
    {
        // Test: Transaction rollback
        // Verify: Changes not persisted
    }
}
```

#### Required NuGet Packages

Add to `AspNetCoreMvc.IntegrationTests.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.0" />
  <PackageReference Include="xunit" Version="2.9.0" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
  <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="6.0.36" />
  <PackageReference Include="FluentAssertions" Version="6.12.1" />
</ItemGroup>

<ItemGroup>
  <ProjectReference Include="..\AspNetCoreMvc\AspNetCoreMvc.csproj" />
  <ProjectReference Include="..\Data\Data.csproj" />
</ItemGroup>
```

**Note:** Package versions shown for .NET 6.0. During Phase 2, these will be updated to .NET 10 compatible versions if needed.

#### Test Database Configuration

User will provide test database connection string. Add to test project:

**File:** `appsettings.Testing.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MvcHybridTestDb;Integrated Security=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

**Test Setup:**
- User creates test database: `MvcHybridTestDb`
- Tests run against this test database
- Schema created/updated by NHibernate during test initialization

#### Validation Checklist (Phase 0)

**Before proceeding to migration:**
- [ ] Test project created and added to solution
- [ ] All test files implemented
- [ ] Test database created by user
- [ ] Test configuration updated with test DB connection string
- [ ] `dotnet test` runs successfully
- [ ] All tests pass on .NET 6.0 (baseline established)
- [ ] Test coverage includes initialization and repository methods

**Success Gate:** Cannot proceed to Phase 2 (migration) until all Phase 0 tests pass.

---

### Data (Data\Data.csproj)

**Current State:**
- Target Framework: netstandard2.0
- Project Type: Class Library
- SDK-style: True
- Dependants: 2 (AspNetCoreMvc, AspNetWebForms)
- Files: 9
- Lines of Code: 219

**Target State:**
- ✅ **No changes required** - .NET Standard 2.0 remains compatible with .NET 10.0

**Rationale:**
- .NET Standard 2.0 is supported by both .NET 10.0 and .NET Framework 4.8
- Keeping netstandard2.0 maintains compatibility with AspNetWebForms
- No package updates needed (0 issues found)
- No API compatibility issues

---

## Package Update Reference

### Packages Requiring Action

| Package | Current Version | Target Version | Action | Projects Affected | Reason |
| :--- | :---: | :---: | :--- | :--- | :--- |
| System.Net.Http | 4.3.4 | - | ❌ Remove | AspNetCoreMvc | Functionality included with .NET 10 framework |
| System.Text.RegularExpressions | 4.3.1 | - | ❌ Remove | AspNetCoreMvc | Functionality included with .NET 10 framework |

### Packages Remaining Unchanged

These packages are compatible with .NET 10.0 and require no updates:

| Package | Version | Projects | Status |
| :--- | :---: | :--- | :--- |
| FluentNHibernate | 3.2.0 | AspNetCoreMvc, Data | ✅ Compatible |
| Microsoft.AspNetCore.SystemWebAdapters.CoreServices | 1.1.0 | AspNetCoreMvc | ✅ Compatible |
| NHibernate | 5.4.9 | AspNetCoreMvc, Data | ✅ Compatible |
| System.Data.SqlClient | 4.8.6 | AspNetCoreMvc, Data | ✅ Compatible |
| Yarp.ReverseProxy | 2.0.1 | AspNetCoreMvc | ✅ Compatible |

### Data Library Packages

Data library (netstandard2.0) packages remain unchanged and compatible:

| Package | Version | Status |
| :--- | :---: | :--- |
| FluentNHibernate | 3.2.0 | ✅ Compatible |
| NETStandard.Library | 2.0.3 | ✅ Compatible |
| NHibernate | 5.4.9 | ✅ Compatible |
| System.Data.SqlClient | 4.8.6 | ✅ Compatible |

---

## Breaking Changes Catalog

### Expected Breaking Changes

**None identified for AspNetCoreMvc**

The assessment analysis confirms:
- ✅ **0 Binary Incompatible APIs** - No APIs require code changes
- ✅ **0 Source Incompatible APIs** - No compilation issues expected
- ✅ **0 Behavioral Changes** - No runtime behavior changes identified
- ✅ **1,660 Compatible APIs** - All analyzed APIs work with .NET 10.0

### Framework-Level Changes (.NET 6 → .NET 10)

While no specific breaking changes affect this project, be aware of general .NET 10 improvements:

**Performance Enhancements:**
- JIT compiler improvements
- GC optimizations
- Runtime performance gains

**New Language Features:**
- C# 13 language features (if using)
- Enhanced pattern matching
- Collection expressions improvements

**ASP.NET Core Changes:**
- Enhanced minimal APIs
- Improved performance middleware
- New authentication options

**None of these require code changes** - they are additive improvements.

### Areas Requiring Attention

**None Expected**

However, as a precaution during testing, validate:

1. **YARP Reverse Proxy Configuration**
   - File: `appsettings.json` and/or `Program.cs`
   - Verify routing rules still apply correctly
   - Test proxying to AspNetWebForms application

2. **SystemWebAdapters Integration**
   - Verify adapter functionality for WebForms integration
   - Check session sharing still works (if configured)

3. **NHibernate Data Layer**
   - Verify database connectivity
   - Confirm schema export functionality
   - Test data operations

**Impact**: These are validation checks, not required code changes.

---

## Testing & Validation Strategy

### Multi-Level Testing Approach

#### Level 1: Build Validation (Immediate)

**After project file updates:**

1. **Restore Packages**
   ```powershell
   dotnet restore AspNetCoreMvc\AspNetCoreMvc.csproj
   ```
   - ✅ All packages restore successfully
   - ✅ No dependency conflicts
   - ✅ No missing packages

2. **Build Project**
   ```powershell
   dotnet build AspNetCoreMvc\AspNetCoreMvc.csproj --configuration Release
   ```
   - ✅ Build succeeds with 0 errors
   - ✅ Build succeeds with 0 warnings
   - ✅ No deprecated API warnings

3. **Build Solution**
   ```powershell
   dotnet build mvc-core-webforms-hybrid.sln --configuration Release
   ```
   - ✅ Entire solution builds successfully
   - ✅ Data library builds
   - ✅ No cross-project compatibility issues

#### Level 2: Application Validation (Smoke Testing)

**After successful build:**

1. **Application Startup**
   - ✅ Application starts without exceptions
   - ✅ No runtime errors in console
   - ✅ Listens on expected port

2. **Basic Functionality**
   - ✅ Home page loads successfully
   - ✅ MVC controllers respond correctly
   - ✅ Views render without errors
   - ✅ Static files serve correctly

3. **Database Layer**
   - ✅ NHibernate initializes successfully
   - ✅ Database connection established
   - ✅ Schema export completes (if applicable)
   - ✅ Basic data operations work (read/write)

4. **YARP Reverse Proxy**
   - ✅ Proxy configuration loads
   - ✅ Routes to AspNetWebForms work correctly
   - ✅ Request forwarding functions
   - ✅ Response handling works

5. **SystemWebAdapters**
   - ✅ Adapter services initialize
   - ✅ Integration with WebForms functions (if tested)

#### Level 3: Integration Testing (Comprehensive)

**End-to-end validation:**

1. **Cross-Application Routing**
   - Test navigation from MVC app to WebForms app via YARP
   - Verify seamless user experience between applications
   - Confirm URL routing works as expected

2. **Shared Data Layer**
   - Execute CRUD operations from AspNetCoreMvc
   - Verify data consistency with AspNetWebForms operations
   - Test concurrent access patterns

3. **Configuration Validation**
   - Verify appsettings.json loaded correctly
   - Check connection strings resolved properly
   - Confirm environment-specific settings work

4. **Performance Baseline**
   - Compare startup time with .NET 6 version
   - Verify no performance regressions
   - Note any improvements

#### Level 4: Regression Testing

**Critical user flows:**
- [ ] User can access all MVC pages
- [ ] User can navigate to WebForms pages via proxy
- [ ] Data operations persist correctly
- [ ] Application handles errors gracefully
- [ ] Logging and diagnostics function

### Test Execution Order

1. ✅ **Build Validation** (automated) - Must pass before proceeding
2. ✅ **Application Startup** (automated) - Must succeed before functional tests
3. ✅ **Smoke Tests** (semi-automated) - Quick validation of core features
4. ✅ **Integration Tests** (manual/automated) - Comprehensive validation
5. ✅ **Regression Tests** (manual) - Ensure no existing functionality broken

### Success Criteria per Level

| Level | Criteria | Blocker if Failed? |
| :--- | :--- | :---: |
| Build Validation | 0 errors, 0 warnings | ✅ Yes |
| Integration Tests | All tests pass | ✅ Yes |
| Application Startup | Starts without exceptions | ✅ Yes |
| Smoke Tests | Core features work | ✅ Yes |
| Integration Tests (manual) | All scenarios pass | ⚠️ Investigate |
| Regression Tests | No broken features | ⚠️ Investigate |

**Critical Gate:** Integration tests must pass both before (Phase 0) and after (Phase 3) migration.

### Testing Automation

**Automated (Integration Tests):**
- Run via: `dotnet test AspNetCoreMvc.IntegrationTests/AspNetCoreMvc.IntegrationTests.csproj`
- **Before Migration**: Establish baseline (all tests pass on .NET 6.0)
- **After Migration**: Regression detection (all tests still pass on .NET 10.0)

**Test Coverage:**
- Application initialization and startup
- Repository CRUD operations
- NHibernate session management
- Database connectivity and transactions
- Configuration loading

**Manual Testing (Supplementary):**
- YARP routing validation
- Cross-application navigation
- UI rendering verification

**Recommendation:** Integration tests provide automated regression detection, significantly reducing manual testing burden.

---

## Risk Management

### Risk Assessment

| Project | Risk Level | Description | Mitigation |
| :--- | :---: | :--- | :--- |
| AspNetCoreMvc | 🟢 Low | Simple framework upgrade with no breaking changes | Standard testing, quick rollback available |

**Overall Risk: Low**

### Detailed Risk Analysis

**AspNetCoreMvc - Low Risk Factors:**
- ✅ Small codebase (267 LOC)
- ✅ Only 2 package changes (removals, not upgrades)
- ✅ 0 API breaking changes identified
- ✅ 0 security vulnerabilities
- ✅ Already on modern .NET (6.0)
- ✅ SDK-style project
- ✅ Clear upgrade path documented

**No High-Risk Items Identified**

### Security Vulnerabilities

**None found** - All packages are up-to-date and secure.

### Contingency Plans

**Scenario 1: Unexpected Build Errors**
- **Likelihood**: Low
- **Impact**: Low
- **Response**: 
  - Review error messages against .NET 10 breaking changes documentation
  - Check for implicit dependencies or dynamic code
  - Consult .NET 10 migration guide for edge cases
- **Rollback**: Simple branch revert

**Scenario 2: Runtime Behavioral Changes**
- **Likelihood**: Very Low
- **Impact**: Medium
- **Response**:
  - Review .NET 10 behavioral changes documentation
  - Test critical user flows
  - Compare behavior with .NET 6 version
- **Rollback**: Deploy previous version

**Scenario 3: YARP Compatibility Issues**
- **Likelihood**: Very Low (Yarp.ReverseProxy 2.0.1 is compatible)
- **Impact**: Medium (affects integration with WebForms)
- **Response**:
  - Verify YARP configuration still works
  - Check for .NET 10-specific YARP updates
  - Test proxy routing between applications
- **Rollback**: Simple branch revert

### Rollback Strategy

**Simple Rollback:**
1. Abort current upgrade work
2. Switch back to `main` branch
3. Delete `upgrade-to-NET10` branch if desired

**Time to Rollback**: < 1 minute (no complex dependencies or data migrations)

---

## Complexity & Effort Assessment

### Per-Project Complexity

| Project | Complexity | Dependencies | Risk | Issues | LOC | Notes |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| AspNetCoreMvc | 🟢 Low | 1 | 🟢 Low | 2 | 267 | Simple framework upgrade |
| AspNetCoreMvc.IntegrationTests | 🟡 Medium | 2 | 🟢 Low | 0 | ~300-500 | New project, requires test implementation |
| Data | ✅ None | 0 | ✅ None | 0 | 219 | No changes needed |

### Phase Complexity

**Phase 0: Test Establishment**
- **Complexity**: 🟡 Medium
- **Projects**: AspNetCoreMvc.IntegrationTests (new)
- **Key Activities**: Create project, implement tests, verify against test DB
- **Expected Challenges**: Test database setup, NHibernate test configuration

**Phase 1: Preparation**
- **Complexity**: 🟢 Low
- **Key Activities**: Verify .NET 10 SDK
- **Expected Challenges**: None

**Phase 2: Atomic Upgrade**
- **Complexity**: 🟢 Low
- **Projects**: AspNetCoreMvc, AspNetCoreMvc.IntegrationTests
- **Key Activities**: Update TargetFramework, remove 2 packages, build, verify
- **Expected Challenges**: None

**Phase 3: Validation**
- **Complexity**: 🟢 Low
- **Key Activities**: Run integration tests, verify no regressions
- **Expected Challenges**: None (tests already proven on .NET 6.0)

### Resource Requirements

**Skills Required:**
- Basic understanding of .NET project files
- Familiarity with NuGet package management
- Ability to interpret build errors (if any occur)

**Parallel Capacity:**
- Not applicable (single project + test project upgraded together)

**Relative Effort:**
- **Phase 0 (Test Creation)**: Medium - Requires writing test code and configuring test database
- **Phase 2 (Migration)**: Low - Straightforward framework version update with minimal changes
- **Phase 3 (Validation)**: Low - Automated tests provide quick validation

---

## Source Control Strategy

### Branching Strategy

**Source Branch:** `main`
- Starting point for upgrade
- Represents current stable .NET 6.0 version

**Upgrade Branch:** `upgrade-to-NET10` ✅ (already created)
- All upgrade changes committed here
- Isolated from main branch during work
- Allows easy rollback if needed

**Merge Target:** `main`
- After successful validation, merge upgrade-to-NET10 back to main

### Commit Strategy

### Commit Strategy

**Recommended: Two Atomic Commits**

Given the addition of integration tests, use two commits to separate test establishment from migration:

**Commit 1: Test Establishment (Phase 0)**

```
git add AspNetCoreMvc.IntegrationTests/
git add mvc-core-webforms-hybrid.sln
git commit -m "Add integration tests for AspNetCoreMvc on .NET 6.0

- Created AspNetCoreMvc.IntegrationTests project
- Added application initialization tests
- Added repository method tests against test database
- Added NHibernate integration tests
- All tests pass on .NET 6.0 baseline
- Test database: MvcHybridTestDb"
```

**Commit 2: Framework Upgrade (Phase 2)**

```
git add AspNetCoreMvc/AspNetCoreMvc.csproj
git add AspNetCoreMvc.IntegrationTests/AspNetCoreMvc.IntegrationTests.csproj
git commit -m "Upgrade AspNetCoreMvc from .NET 6.0 to .NET 10.0

- Updated TargetFramework to net10.0 (AspNetCoreMvc + tests)
- Removed System.Net.Http (now in framework)
- Removed System.Text.RegularExpressions (now in framework)
- All packages compatible and verified
- Build successful with 0 errors/warnings
- All integration tests pass on .NET 10.0"
```

**Rationale:**
- Separates test creation from migration
- Establishes working test baseline on .NET 6.0
- Shows clear before/after progression
- Each commit is independently reviewable
- Easy to bisect if issues arise

**Alternative: Single Commit**

If preferred, combine both phases:

```
git add .
git commit -m "Add integration tests and upgrade AspNetCoreMvc to .NET 10.0

## Test Establishment
- Created AspNetCoreMvc.IntegrationTests project
- Added comprehensive integration tests
- All tests pass on .NET 6.0 baseline

## Migration
- Updated TargetFramework to net10.0
- Removed System.Net.Http (now in framework)
- Removed System.Text.RegularExpressions (now in framework)
- All integration tests still pass on .NET 10.0
- Build successful with 0 errors/warnings"
```

### Review and Merge Process

#### Pull Request Requirements

**Title:** `Upgrade AspNetCoreMvc to .NET 10.0`

**Description Template:**
```markdown
## Summary
Adds integration tests and upgrades AspNetCoreMvc project from .NET 6.0 to .NET 10.0 LTS.

## Changes
### Test Establishment
- Created AspNetCoreMvc.IntegrationTests project
- Added application initialization tests
- Added repository method tests (CRUD operations)
- Added NHibernate integration tests
- Configured test database: MvcHybridTestDb
- All tests pass on .NET 6.0 baseline

### Migration
- Updated TargetFramework to net10.0 (AspNetCoreMvc + tests)
- Removed 2 packages now included in framework
- No code changes required

## Testing
- [x] Integration tests pass on .NET 6.0 (baseline)
- [x] Build succeeds with 0 errors
- [x] Build succeeds with 0 warnings
- [x] Integration tests pass on .NET 10.0 (no regressions)
- [x] Application starts successfully
- [x] YARP routing to WebForms works
- [x] Database operations function correctly

## Breaking Changes
None - all APIs compatible.

## Risk Assessment
Low - minimal changes with comprehensive integration test coverage.
```

#### Review Checklist

**Reviewer should verify:**
- [ ] AspNetCoreMvc.IntegrationTests project added to solution
- [ ] Integration tests implemented and comprehensive
- [ ] Tests passed on .NET 6.0 before migration
- [ ] Only AspNetCoreMvc.csproj and AspNetCoreMvc.IntegrationTests.csproj modified
- [ ] TargetFramework is net10.0 in both projects
- [ ] System.Net.Http package removed from AspNetCoreMvc
- [ ] System.Text.RegularExpressions package removed from AspNetCoreMvc
- [ ] No other unintended changes
- [ ] Build logs show success
- [ ] Integration tests pass on .NET 10.0
- [ ] Testing evidence provided

#### Merge Criteria

**Required before merge:**
- ✅ Pull request approved by reviewer(s)
- ✅ All validation checkpoints passed
- ✅ Build succeeds
- ✅ No new warnings introduced
- ✅ Application tested and functional
- ✅ No conflicts with main branch

**Merge Method:** Squash or Regular merge (team preference)

### Post-Merge Actions

1. **Update README.md**
   - Update "ToDo" section - mark "migrate mvc app to .net 10" as complete
   - Update requirements section to reflect .NET 10
   - Document integration test project and how to run tests

2. **Tag Release** (optional)
   ```
   git tag -a v2.0-net10 -m "AspNetCoreMvc upgraded to .NET 10.0 with integration tests"
   git push origin v2.0-net10
   ```

3. **Notify Team**
   - Inform team of upgrade completion
   - Share integration test documentation
   - Update development environment requirements (test database setup)

### Branch Cleanup

After successful merge to main:
```powershell
git checkout main
git pull origin main
git branch -d upgrade-to-NET10  # Delete local branch
git push origin --delete upgrade-to-NET10  # Delete remote branch (if pushed)
```

---

## Success Criteria

### Technical Criteria

**All of the following must be true:**

#### Project Upgrade Completion
- ✅ AspNetCoreMvc targets net10.0
- ✅ AspNetCoreMvc.IntegrationTests targets net10.0
- ✅ System.Net.Http package removed from AspNetCoreMvc
- ✅ System.Text.RegularExpressions package removed from AspNetCoreMvc
- ✅ All remaining packages compatible and verified
- ✅ Data library remains on netstandard2.0 (unchanged)

#### Build Success
- ✅ `dotnet restore` completes with 0 errors
- ✅ `dotnet build` completes with 0 errors  
- ✅ `dotnet build` completes with 0 warnings
- ✅ Solution build succeeds (all projects including tests)
- ✅ No package dependency conflicts

#### Test Success (Critical)
- ✅ Integration tests created and implemented
- ✅ All integration tests pass on .NET 6.0 (pre-migration baseline)
- ✅ All integration tests pass on .NET 10.0 (post-migration validation)
- ✅ Test pass rate: 100% on both frameworks
- ✅ No test regressions detected

#### Runtime Validation
- ✅ AspNetCoreMvc application starts successfully
- ✅ No runtime exceptions during startup
- ✅ Application responds to HTTP requests
- ✅ YARP reverse proxy routes to AspNetWebForms correctly
- ✅ NHibernate initializes and connects to database
- ✅ Data operations complete successfully

#### Security & Quality
- ✅ No security vulnerabilities present
- ✅ No new compiler warnings introduced
- ✅ No deprecated API usage warnings

### Quality Criteria

**Code Quality:**
- ✅ Project file follows SDK-style conventions
- ✅ Package references minimal and necessary
- ✅ No redundant dependencies
- ✅ Code remains unchanged (no refactoring required)

**Documentation:**
- ✅ README.md updated to reflect .NET 10 requirement
- ✅ Commit messages describe changes clearly
- ✅ Pull request documents validation performed

**Testing:**
- ✅ Integration tests established on .NET 6.0 baseline
- ✅ All integration tests pass on .NET 10.0
- ✅ Repository operations validated via automated tests
- ✅ Application initialization validated via automated tests
- ✅ Manual smoke tests passed
- ✅ Integration with WebForms validated
- ✅ Database operations tested
- ✅ No functional regressions identified

### Process Criteria

**Strategy Adherence:**
- ✅ All-At-Once strategy followed (single atomic operation)
- ✅ All assessment findings addressed
- ✅ All package removals completed

**Source Control:**
- ✅ Changes committed to `upgrade-to-NET10` branch
- ✅ Commit history clear and meaningful
- ✅ Pull request created and reviewed
- ✅ Merged to `main` branch

**Risk Management:**
- ✅ Rollback plan documented and available
- ✅ No high-risk changes introduced
- ✅ Contingency plans understood

### Definition of Done

**The upgrade is complete when:**

1. ✅ Integration tests created and pass on .NET 6.0 (baseline)
2. ✅ AspNetCoreMvc runs on .NET 10.0
3. ✅ Integration tests pass on .NET 10.0 (no regressions)
4. ✅ All builds succeed without errors or warnings
5. ✅ Application functions correctly (manual validation passed)
6. ✅ YARP integration with WebForms works
7. ✅ Data layer operations function correctly
8. ✅ Changes merged to main branch
9. ✅ Documentation updated
10. ✅ Team notified and upgrade validated

**Completion Checkpoint:** When all items above are ✅, the migration is considered successful and complete.
