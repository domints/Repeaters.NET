FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG TARGETARCH

WORKDIR /home/app

COPY out/$TARGETARCH/ .

EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "/home/app/Repeaters.NET.Api.dll", "--urls", "http://0.0.0.0:5000"]