# Migrations

# Migration scripts for the database schema

Add-Migration -Name InitialCreate -OutputDir Data/Migrations -StartupProject FLP.AzureFunctions -Project FLP.Infra -Context AppDbContext

Update-Database -StartupProject FLP.AzureFunctions -Project FLP.Infra -Context AppDbContext