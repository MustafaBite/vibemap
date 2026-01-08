# Project Structure

## Root Directory Organization
```
VibeMap/
├── Forms/           # Windows Forms UI components
├── Services/        # Business logic and recommendation engine
├── Models/          # Data models and DTOs
├── Utils/           # Utility classes (DB connection, theme management)
├── DataAccess/      # Database initialization and data access
├── Database/        # SQLite database files and assets
├── Data/            # Static data files (JSON configurations)
├── Properties/      # Assembly info, resources, settings
├── Entities/        # Domain entities (currently empty)
└── packages/        # NuGet package dependencies
```

## Architecture Patterns

### Forms Structure
- Each form has `.cs` (code-behind), `.Designer.cs` (UI design), and `.resx` (resources)
- Form naming convention: `Frm[Purpose]` (e.g., `FrmLogin`, `FrmHome`, `FrmMood`)
- Forms are located in `Forms/` directory

### Service Layer
- `RecommendationService` - Core recommendation logic with database queries
- `SmartRecommendationEngine` - Mood analysis and content matching
- Services use static methods for stateless operations

### Data Access
- `DbConnection` utility provides connection factory methods
- Separate connection methods for user data (`GetUserConnection()`) and catalog (`GetCatalogConnection()`)
- Database initialization handled in `DatabaseInitializer`

### Models
- `MoodAnalysisResult` - Represents analyzed mood data
- `Recommendation` - Content recommendation data structure
- Models follow simple POCO pattern

## Naming Conventions
- Classes: PascalCase
- Methods: PascalCase
- Properties: PascalCase
- Private fields: camelCase with underscore prefix (`_fieldName`)
- Constants: PascalCase
- Database files: PascalCase with .db extension

## File Organization Rules
- UI forms grouped in `Forms/` directory
- Business logic in `Services/` directory
- Database utilities in `Utils/` directory
- Keep related files together (form + designer + resources)
- Static data files in `Data/` directory