# Aprendiendo .NET mientras creo un Ecommerce

## Levantar el proyecto

1. Instalar paquetes ```dotnet restore``` 
2. Levantar base de datos ```docker compose up -d```
3. Aplicar migraciones ```dotnet ef database update``` (Es opcional, ya que DataExtensions.cs tiene un método que aplica migraciones al correr el proyecto).
4. Compilar y ejecutar ```dotnet watch run``` (Alternativa: usar solo ``dotnet run`` para una ejecució estándar)
