# Étape 1 : Construction et compilation de l'application
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

# Installer dotnet-ef dans l'image de construction
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool restore

# Étape 2 : Préparation de l'image runtime plus légère
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copier les fichiers de l'application depuis l'étape build
COPY --from=build /out .

# Copier le script de mise à jour de la base de données
COPY --from=build /app/updatedb.sh .
RUN chmod +x updatedb.sh  # S'assurer que le script est exécutable

# Installer dotnet-ef dans l'image runtime
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Définir l’environnement sur Production
ENV ASPNETCORE_ENVIRONMENT=Production
# ENV ASPNETCORE_ENVIRONMENT=Development

# Exposer le port 5000 pour les requêtes HTTP
EXPOSE 5000

# Exécuter la mise à jour de la base de données puis lancer l’application
ENTRYPOINT ["bash", "-c", "./updatedb.sh && dotnet TSENA.dll"]
