# Utiliser une image officielle de .NET pour la compilation et la publication
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier le fichier projet et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier le reste des fichiers et compiler l’application
COPY . ./
RUN dotnet publish -c Release -o /out

# Définir l’environnement sur Production
ENV ASPNETCORE_ENVIRONMENT=Production
# ENV ASPNETCORE_ENVIRONMENT=Development

# Installer dotnet-ef et appliquer les migrations pour tous les DbContext présents
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Appliquer les migrations pour tous les DbContext, sans refaire celles déjà appliquées
RUN for ctx in $(dotnet ef dbcontext list); do \
    echo "Applying migrations for context: $ctx"; \
    dotnet ef database update --context $ctx || echo "Skipping migration for $ctx (already applied)"; \
done

# Utiliser une image plus légère pour l’exécution de l’application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copier les fichiers de publication de l'image de build
COPY --from=build /out .

# Définir l’environnement sur Production
ENV ASPNETCORE_ENVIRONMENT=Production
# ENV ASPNETCORE_ENVIRONMENT=Development

# Exposer le port 5000 pour les requêtes HTTP
EXPOSE 5000

# Commande pour démarrer l'application
ENTRYPOINT ["dotnet", "TSENA.dll"]
