FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY ./arkopong-build /app

ENV ASPNETCORE_URLS=http://*:5501
EXPOSE 5501

CMD dotnet /app/arkopongBack.dll