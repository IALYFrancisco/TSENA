#!/bin/bash

echo "🔍 Récupération des DbContext disponibles..."
CONTEXTS=$(dotnet ef dbcontext list)

# Vérifier s'il y a au moins un contexte
if [ -z "$CONTEXTS" ]; then
    echo "❌ Aucun DbContext trouvé."
    exit 1
fi

echo "✅ Contexte(s) trouvé(s) : $CONTEXTS"
echo "🚀 Mise à jour de la base de données pour chaque contexte..."

# Appliquer les migrations pour chaque contexte
for CONTEXT in $CONTEXTS; do
    echo "🔄 Mise à jour du contexte : $CONTEXT"
    dotnet ef database update --context "$CONTEXT"
done

echo "🎉 Toutes les bases de données ont été mises à jour avec succès !"
