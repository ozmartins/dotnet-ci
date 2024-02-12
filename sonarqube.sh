 dotnet sonarscanner begin /k:"dotnet-ci" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_d81f481294ba1bfb65b987fb75a0b99007430d98"
 dotnet clean
 dotnet build
 dotnet sonarscanner end /d:sonar.token="sqp_d81f481294ba1bfb65b987fb75a0b99007430d98"
