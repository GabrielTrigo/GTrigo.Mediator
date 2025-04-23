#!/bin/bash

exec() {
    local cmd="$1"
    local errorMessage="${2:-Erro ao executar o comando: $cmd}"

    eval "$cmd"
    
    if [ $? -ne 0 ]; then
        echo "Exec: $errorMessage"
        exit 1
    fi
}

artifacts="./artifacts"

if [ -d "$artifacts" ]; then
    rm -rf "$artifacts"
fi

exec "dotnet clean -c Release" "Erro ao executar dotnet clean"
exec "dotnet build -c Release" "Erro ao executar dotnet build"
exec "dotnet test -c Release --no-build -l trx --verbosity=normal" "Erro ao executar dotnet test"
exec "dotnet pack ./GTrigo.Mediator/GTrigo.Mediator.csproj -c Release -o $artifacts --no-build" "Erro ao executar dotnet pack"