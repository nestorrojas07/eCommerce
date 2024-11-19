## generating migrations with dotnet ef-tools
dotnet ef migrations add InitialMigration -p "src/Catalog.Infraestructure" -s "src/Catalog.API" -o "Persistence/Migrations"

