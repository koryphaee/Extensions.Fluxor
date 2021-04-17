#!/bin/bash

if [ $# -ne 1 ]
then
	echo "missing version X.Y.Z"
	exit
fi 

dotnet.exe pack --output Packages/ Source/ /p:Version=$1

key=$(cmd.exe /c echo %NuGetApiKey% | tr -d '\r')

for file in Packages/*.nupkg
do
	dotnet.exe nuget push "$file" --skip-duplicate --api-key "$key" --source "https://api.nuget.org/v3/index.json"
done

rm -rf Packages/
