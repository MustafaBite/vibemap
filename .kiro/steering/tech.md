# Technology Stack

## Framework & Runtime
- .NET Framework 4.7.2
- C# 7.3 language version
- Windows Forms for desktop UI
- DevExpress v24.1 UI components (XtraEditors, XtraGrid, XtraLayout)

## Database & Data Access
- SQLite 1.0.119.0 for local data storage
- Dapper 2.1.66 for lightweight ORM
- Separate databases: UserStore.db and CatalogStore.db
- WAL journal mode for better concurrency

## Key Dependencies
- System.Data.SQLite.Core - SQLite database engine
- Microsoft.Bcl.AsyncInterfaces - Async programming support
- System.Threading.Tasks.Extensions - Task extensions
- System.Runtime.CompilerServices.Unsafe - Performance optimizations

## Build & Development

### Build Commands
```bash
# Build Debug (AnyCPU)
msbuild VibeMap.sln /p:Configuration=Debug

# Build Release (AnyCPU)
msbuild VibeMap.sln /p:Configuration=Release

# Build x64 Debug
msbuild VibeMap.sln /p:Configuration=Debug /p:Platform=x64

# Build x64 Release
msbuild VibeMap.sln /p:Configuration=Release /p:Platform=x64
```

### Package Management
- NuGet packages managed via packages.config
- Restore packages: `nuget restore VibeMap.sln`

### Output Directories
- Debug: `bin\Debug\` or `bin\x64\Debug\`
- Release: `bin\Release\` or `bin\x64\Release\`

## Database Configuration
- Connection string uses WAL journal mode and 5-second busy timeout
- Database files stored in `Database\` folder relative to executable
- Automatic database initialization on startup