In Visual Studio, you can use the Package Manager Console to apply pending migrations to the database:

PM> Update-Database
Alternatively, you can apply pending migrations from a command prompt at your project directory:

> dotnet ef database update

PreBuildScript
PowerShell -ExecutionPolicy RemoteSigned $(ProjectDir)autoincrement_version.ps1 '$(ProjectDir)appsettings.json'





dotnet restore
#cd possibly
#dir possibly
dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password={password}" Npgsql.EntityFrameworkCore.PostgreSQL -o SimpleAPI.DataAccess


#Identity create migration, then udpate db
dotnet ef migrations add MigrationName --context IdentityContext
##dotnet ef migrations add InitialFootball --context FootballContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj --output-dir .\Football 
dotnet ef migrations add InitialIdentity --context IdentityContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj --output-dir .\Identity 

--force
dotnet ef database update --context IdentityContext

#OR do it using just the ef commands
add-migration "Migration Name" -Context IdentityContext
update-database -Context IdentityContext

dotnet ef database update --context FootballContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj --output-dir .\ -t teams -t players -t games -t stats --force
dotnet ef dbContext scaffold "Host=localhost;Database=postgresNew;Username=postgres;Password=" --provider Npgsql.EntityFrameworkCore.PostgreSQL --context FootballContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj --output-dir .\ -t teams -t players -t games -t stats --force
dotnet ef database update --context FootballContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj 
dotnet ef database update --context IdentityContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj 


#These two work, the other one's are useful in different ways
#Idnentity
dotnet ef migrations add InitialIdentityContenxt --context IdentityContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj --output-dir .\Identity 
dotnet ef database update --context IdentityContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj 

#FootballContext
dotnet ef migrations add firstFootball00000 --context FootballContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj --output-dir .\Football
 dotnet ef database update --context FootballContext --project .\SimpleAPI.DataAccess\SimpleAPI.DataAccess.csproj --startup-project .\SimpleAPI\SimpleAPI.csproj 
