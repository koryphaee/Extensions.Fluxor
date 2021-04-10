#!/bin/bash

dotnet.exe build -c Release Source/
dotnet.exe pack --output Packages Source/

key=$(cmd.exe /c echo %NuGetApiKey% | tr -d '\r')

for file in Packages/*.nupkg
do
	dotnet.exe nuget push "$file" --skip-duplicate --api-key "$key" --source "https://api.nuget.org/v3/index.json"
done

rm -rf Packages/
