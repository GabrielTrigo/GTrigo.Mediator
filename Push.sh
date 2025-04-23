#!/bin/bash

scriptName=$(basename "$0")

artifacts="./artifacts"

if [ -z "$NUGET_API_KEY" ]; then
    echo "${scriptName}: NUGET_API_KEY está vazia ou não definida. Publicação de pacote(s) ignorada."
else
    for package in "$artifacts"/*.nupkg; do
        if [ -f "$package" ]; then
            echo "${scriptName}: Publicando $(basename "$package")"
            dotnet nuget push "$package" --source "$NUGET_URL" --api-key "$NUGET_API_KEY"
            
            if [ $? -ne 0 ]; then
                echo "Exec: Erro ao publicar o pacote $package"
                exit 1
            fi
        fi
    done
fi