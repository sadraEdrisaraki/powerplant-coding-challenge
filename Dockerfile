FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /EngieCodingChallenge
COPY . .

RUN dotnet restore "./EngieCodingChallenge/EngieCodingChallenge.WebApi.csproj" --disable-parallel
RUN dotnet publish "./EngieCodingChallenge/EngieCodingChallenge.WebApi.csproj" -c Release -o ./publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build ./EngieCodingChallenge ./

EXPOSE 8888
RUN chmod +x ./publish/EngieCodingChallenge.WebApi.dll
ENTRYPOINT ["./publish/EngieCodingChallenge.WebApi.dll"]
