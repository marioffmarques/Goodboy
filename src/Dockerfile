FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app
EXPOSE 5000


# 1. Copy csproj and restore as distinct layers

COPY *.sln .
COPY Goodboy.Http/*.csproj ./Goodboy.Http/
COPY Goodboy.Http.Logger/*.csproj ./Goodboy.Http.Logger/
COPY Goodboy.Practices/*.csproj ./Goodboy.Practices/
COPY Goodboy.Repository/*.csproj ./Goodboy.Repository/
COPY Goodboy.Tests/*.csproj ./Goodboy.Tests/
COPY TestApi/*.csproj ./TestApi/
RUN dotnet restore


# 2. Copy and build everything else

COPY Goodboy.Http/. ./Goodboy.Http/
COPY Goodboy.Http.Logger/. ./Goodboy.Http.Logger/
COPY Goodboy.Practices/. ./Goodboy.Practices/
COPY Goodboy.Repository/. ./Goodboy.Repository/
COPY Goodboy.Tests/. ./Goodboy.Tests/
COPY TestApi/. ./TestApi/
RUN dotnet build

FROM build AS publish
WORKDIR /app/TestApi
RUN dotnet publish -f netcoreapp2.0 -o out


FROM microsoft/dotnet:2.0-runtime AS runtime
WORKDIR /app
COPY --from=publish /app/TestApi/out ./
ENTRYPOINT ["dotnet", "TestApi.dll"]







#FROM build AS testrunner
#WORKDIR /app/Goodboy.Tests
#ENTRYPOINT ["dotnet", "test","--logger:trx"]

#FROM build AS test
#WORKDIR /app/Goodboy.Practices/tests
#RUN dotnet test

