# Instalation

## Prerequisites

1. Install [dotnet](https://dotnet.microsoft.com/en-us/download)
2. Install dotnet-ef
```dotnet tool install --global dotnet-ef```
3. Install docker and docker-compose

## How to run

1. Build and run application with dependencies
```
docker-compose up
```
2. Setup database
```
cd scripts
./updateDb.sh
```
